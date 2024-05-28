using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Converters;
using UI.Areas.Admin.Models.FileManager;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class FileManagerController : Controller
	{
		private readonly FileManagerOptions settings;
		private string rootDirectoryPath;

		public FileManagerController(IOptions<FileManagerOptions> options, IWebHostEnvironment env)
		{
			settings = options.Value;
			rootDirectoryPath = Path.GetFullPath(Path.Combine(settings.RootDirectoryRelativePath.TrimStart('/').Split('/')),
				env.WebRootPath);
			Directory.CreateDirectory(rootDirectoryPath);
		}

		public IActionResult Index(FileManagerViewModel model)
		{
			return View(model);
		}

		public IActionResult GetSubdirectories(string directory = "/")
		{
			if (!IsUrlValid(directory))
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var directoryInfo = new DirectoryInfo(GetAbsolutePathFromUrl(directory));
			if (!directoryInfo.Exists)
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			return Json(directoryInfo.GetDirectories("*", new EnumerationOptions
			{
				RecurseSubdirectories = true
			}).Select(item => new DirectoryModel
				{
					Name = item.Name,
					RelativeUrl = GetRelativeUrlFromPath(item.FullName)
				}));
		}

		public IActionResult GetFiles(string directory = "/")
		{
			if (!IsUrlValid(directory))
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			return Json(new DirectoryInfo(GetAbsolutePathFromUrl(directory)).GetFiles().Select(item =>
				new FileModel
				{
					Name = item.Name,
					Size = item.Length,
					LastModified = item.LastWriteTimeUtc,
					RelativeUrl = directory.TrimEnd('/') + '/' + item.Name,
				}));
		}

		[HttpPost]
		public async Task<IActionResult> UploadFile(string directory, IFormFile file)
		{
			if (!IsUrlValid(directory))
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			if (string.IsNullOrEmpty(file.FileName) || file.FileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				return new ContentResult
				{
					Content = UploadError.InvalidName.ToString(),
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			if (file.Length > settings.MaxFileSize * 1024L)
			{
				return new ContentResult
				{
					Content = UploadError.TooBig.ToString(),
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			if (settings.OnFileTypeValidate?.Invoke(file.FileName, file.ContentType) == false)
			{
				return new ContentResult
				{
					Content = UploadError.InvalidType.ToString(),
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var fileInfo = new FileInfo(Path.Combine(GetAbsolutePathFromUrl(directory), file.FileName));
			if (fileInfo.Exists)
			{
				return new ContentResult
				{
					Content = UploadError.AlreadyExists.ToString(),
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			await using var fileStream = new FileStream(fileInfo.FullName, FileMode.CreateNew, FileAccess.Write);
			await file.CopyToAsync(fileStream);
			return Content("");
		}

		public IActionResult RenameFile(string file, string newName)
		{
			if (!IsUrlValid(file))
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			if (string.IsNullOrEmpty(newName) || newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				return new ContentResult
				{
					Content = RenameError.InvalidName.ToString(), 
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var fileInfo = new FileInfo(GetAbsolutePathFromUrl(file));
			if (!fileInfo.Exists)
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var newPath = Path.Combine(fileInfo.DirectoryName, newName);
			if (StringComparer.Ordinal.Compare(fileInfo.FullName, newPath) != 0)
			{
				if (System.IO.File.Exists(newPath))
				{
					return new ContentResult
					{
						Content = RenameError.AlreadyExists.ToString(),
						StatusCode = StatusCodes.Status400BadRequest
					};
				}
				fileInfo.MoveTo(newPath);
			}
			return Content("");
		}

		public IActionResult DeleteFile(string file)
		{
			if (!IsUrlValid(file))
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			System.IO.File.Delete(GetAbsolutePathFromUrl(file));
			return Content("");
		}

		public IActionResult CreateDirectory(string directory)
		{
			if (!IsUrlValid(directory) || directory == "/")
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			Directory.CreateDirectory(GetAbsolutePathFromUrl(directory));
			return Content("");
		}

		public IActionResult RenameDirectory(string directory, string newName)
		{
			if (!IsUrlValid(directory) || directory == "/")
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			if (string.IsNullOrEmpty(newName) || newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				return new ContentResult
				{
					Content = RenameError.InvalidName.ToString(),
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var directoryInfo = new DirectoryInfo(GetAbsolutePathFromUrl(directory));
			if (!directoryInfo.Exists)
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			var newPath = Path.Combine(directoryInfo.Parent.FullName, newName);
			if (StringComparer.Ordinal.Compare(directoryInfo.FullName, newPath) != 0)
			{
				if (Directory.Exists(newPath))
				{
					return new ContentResult
					{
						Content = RenameError.AlreadyExists.ToString(),
						StatusCode = StatusCodes.Status400BadRequest
					};
				}
				directoryInfo.MoveTo(newPath);
			}
			return Content("");
		}

		public IActionResult DeleteDirectory(string directory)
		{
			if (!IsUrlValid(directory) || directory == "/")
			{
				return new ContentResult
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			Directory.Delete(GetAbsolutePathFromUrl(directory), true);
			return Content("");
		}

		private string GetAbsolutePathFromUrl(string relativeUrl)
		{
			return Path.GetFullPath(relativeUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar), rootDirectoryPath);
		}

		private string GetRelativeUrlFromPath(string absolutePath)
		{
			var result = absolutePath.Substring(rootDirectoryPath.Length);
			if (!result.StartsWith(Path.DirectorySeparatorChar))
				result = Path.DirectorySeparatorChar + result;
			return result.Replace(Path.DirectorySeparatorChar, '/');
		}

		private bool IsUrlValid(string relativeUrl)
		{
			return GetAbsolutePathFromUrl(relativeUrl).StartsWith(rootDirectoryPath, StringComparison.Ordinal);
		}
	}
}