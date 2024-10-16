using MahasDemo.DAL.Data.Model;
using MahasDemo.PL.Models.AccountViews;
using MahasDemo.PL.Models.helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MahasDemo.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
								SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region SignUp
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(UserViewModel userVM)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					Fname = userVM.Fname,
					Lname = userVM.Lname,
					UserName = userVM.Email.Split("@")[0],
					Email = userVM.Email,
					IsAgree = userVM.IsAgree
				};
				var result = await _userManager.CreateAsync(user, userVM.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				foreach (var item in result.Errors)
					ModelState.AddModelError("", item.Description);
			}
			return View(userVM);
		}
		#endregion

		#region SignIn
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel userVM)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(userVM.Email);
				if (user != null)
				{
					bool flag = await _userManager.CheckPasswordAsync(user, userVM.Password);
					if (flag)
					{
						var result =await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.RememberMe, false);
						if(result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}
				ModelState.AddModelError("", "Invalid");
			}
			return View(userVM);
		}
		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn");
		}
		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(viewModel.Email);
				if (user != null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var resetPasswordUrl = Url.Action("ResetPassword","Account",new {email=user.Email,token}, Request.Scheme);
					var email = new Email
					{
						To= viewModel.Email,
						Body= resetPasswordUrl,
						Subject="Reset Your Password"
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction("CheckInbox");
				}
			}
			return View(viewModel);
		}

		public IActionResult CheckInbox()
		{
			return View();
		}

		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"]as string;
				var token = TempData["token"]as string;
				var user = await _userManager.FindByEmailAsync(email);
				var result = await _userManager.ResetPasswordAsync(user, token,viewModel.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				foreach (var item in result.Errors) 
					ModelState.AddModelError("",item.Description);
			}
			return View(viewModel);
		}
        #endregion
    }
}