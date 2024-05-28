using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL;
using Common;
using Common.Configuration;
using Common.Configuration.Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using UI.Areas.Admin.Models;
using UI.Areas.Admin.Models.FileManager;
using UI.Extensions.Middleware;

namespace UI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.LoginPath = new PathString("/Admin/Users/Login");
				options.AccessDeniedPath = new PathString("/Admin/Users/Login");
				options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
				options.Events.OnValidatePrincipal += async context =>
				{
					if (!context.Principal.Identity.IsAuthenticated)
					{
						return;
					}
					var user = UserModel.FromEntity(await new UsersBL().GetAsync(context.Principal.Identity.Name));
					if (user == null || user.IsBlocked)
					{
						context.RejectPrincipal();
						await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
						return;
					}
					context.ReplacePrincipal(new ClaimsPrincipal(new CustomUserIdentity(user)));
				};
			});
			services.AddAuthorization();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddOptions<FileManagerOptions>().Bind(Configuration.GetSection("FileManager")).Configure(options =>
			{
				options.OnFileTypeValidate = (fileName, contentType) =>
				{
					return new[] { "exe", "dll", "bat" }.Aggregate(true, (result, extension) =>
						result & !fileName.EndsWith(extension));
				};
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			SharedConfiguration.UpdateSharedConfiguration(Configuration.GetConnectionString("DefaultConnectionString"),
				Configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>(), loggerFactory);

			app.UseStatusCodePagesWithReExecute(context =>
			{
				var area = context.HttpContext.Request.RouteValues["area"];
				var statusCode = context.HttpContext.Response.StatusCode;
				return StringComparer.InvariantCulture.Compare(area, "Admin") == 0
					? $"/Admin/Error/{statusCode}"
					: $"/Error/{statusCode}";
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(builder =>
				{
					builder.Run(async context =>
					{
						context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					});
				});
				app.UseHttpsRedirection();
			}

			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = context =>
				{
					context.Context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=31622400");
				}
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "AdminDefaultRoute",
					pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
					new { area = "Admin" });

				endpoints.MapControllerRoute(
					name: "AdminErrorRoute",
					pattern: "Admin/{controller=Error}/{code:int}",
					defaults: new { area = "Admin", action = "Index" });

				endpoints.MapControllerRoute(
					name: "PublicDefaultRoute",
					pattern: "{controller=Home}/{action=Index}/{id?}", 
					new { area = "Public" });

				endpoints.MapControllerRoute(
					name: "PublicErrorRoute",
					pattern: "{controller=Error}/{code:int}",
					defaults: new { area = "Public", action = "Index" });
			});
		}
	}
}
