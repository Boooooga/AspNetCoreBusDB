using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL;
using Common.Enums;
using Common.Search;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Admin.Models;
using UI.Areas.Admin.Models.ViewModels;
using UI.Areas.Admin.Models.ViewModels.FilterModels;
using UI.Other;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UsersController : Controller
	{
		public IActionResult Login(string returnUrl)
		{
			return View(new LogOnModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LogOnModel model)
		{
			var user = UserModel.FromEntity(await new UsersBL().VerifyPasswordAsync(model.Login, model.Password));
			if (user != null)
			{
				if (user.IsBlocked)
				{
					TempData[OperationResultType.Error.ToString()] = "Пользователь заблокирован";
				}
				else
				{
					var identity = new CustomUserIdentity(user);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
						new AuthenticationProperties
						{
							IsPersistent = model.Remember
						});
					return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? "~/" : model.ReturnUrl);
				}
			}
			else
			{
				TempData[OperationResultType.Error.ToString()] = "Указаны неверные данные для входа";
			}
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = nameof(UserRole.Admin))]
		public async Task<IActionResult> Index(UsersFilterModel filterModel, int page = 1)
		{
			const int objectsPerPage = 10;
			var roles = typeof(UserRole).GetEnumValues().Cast<UserRole>();
			var searchResult = await new UsersBL().GetAsync(new UsersSearchParams
			{
				SearchQuery = filterModel.SearchQuery,
				StartIndex = (page - 1) * objectsPerPage,
				ObjectsCount = objectsPerPage,
				Roles = User.IsInRole(nameof(UserRole.Developer)) ? roles 
					: roles.Where(role => role != UserRole.Developer),
			});
			var viewModel = new SearchResultViewModel<UserModel, UsersFilterModel>(
				UserModel.FromEntitiesList(searchResult.Objects), filterModel,
				searchResult.Total, searchResult.RequestedStartIndex, searchResult.RequestedObjectsCount, 5);
			return View(viewModel);
		}

		[Authorize(Roles = nameof(UserRole.Admin))]
		public async Task<IActionResult> Update(int? id)
		{
			var model = new UserModel();
			if (id != null)
			{
				model = UserModel.FromEntity(await new UsersBL().GetAsync(id.Value));
				if (model == null)
				{
					return NotFound();
				}
				if (!User.IsInRole(nameof(UserRole.Developer)) && model.Role == UserRole.Developer)
				{
					return Forbid();
				}
			}
			model.Password = null;
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = nameof(UserRole.Admin))]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(UserModel model)
		{
			var oldUser = UserModel.FromEntity(await new UsersBL().GetAsync(model.Login));
			if (oldUser != null && oldUser.Id != model.Id)
				ModelState.AddModelError(nameof(model.Login), "Пользователь с таким логином уже существует");
			if (!User.IsInRole(nameof(UserRole.Developer)) && (model.Role == UserRole.Developer || oldUser?.Role == UserRole.Developer))
				return Forbid();
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			await new UsersBL().AddOrUpdateAsync(UserModel.ToEntity(model));
			TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
			return RedirectToAction("Index");
		}
	}
}
