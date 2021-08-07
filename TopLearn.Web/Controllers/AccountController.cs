using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using NHibernate.Hql;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TapLearn.Core.Convertors;
using TapLearn.Core.DTOs;
using TapLearn.Core.Generator;
using TapLearn.Core.Security;
using TapLearn.Core.Sender;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.User;
namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userservices;
        private IViewRenderService _viewRenderService;
        public AccountController(IUserService userservices, IViewRenderService viewRenderService )
        {
            _userservices = userservices;
            _viewRenderService = viewRenderService;

        }
        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
      
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userservices.IsExisistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد.");
                return View(register);
            }
            if (_userservices.IsExisistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد ");
            }

            User user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixEmail(register.Email),
                ActiveCode = NameGenerater.GenerateUniqCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "default.jpg",

            };
            _userservices.adduser(user);
            #region  Send Activation Code
            string body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email,"فعال سازی ",body);
            #endregion


            return View("SuccessRegister",user);
        }
        #endregion
        #region login
        [Route("Login")]
        public IActionResult Login (bool EditProfile= false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userservices.LoginUser(login);
            if(user != null)
            {
               if (user.IsActive)
               {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Userid.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.Issucces = true;
                    if(ReturnUrl != "/")
                    {
                        return Redirect(ReturnUrl);
                    }
                    return View(login);
               }
               else
               {
                ModelState.AddModelError("email","حساب کاربری شما فعال نمی باشد.");

               }


            }
            ModelState.AddModelError("email","کاربری با مشخصات وارد شده یافت نشد.");
            return View(login);
        }
        #endregion
        #region Active Account
        //string id استفاده کردیم تا url بشه account/activeAccount/کدمورد نظر
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userservices.ActiveAccount(id);

            return View();
        }
        #endregion
        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login");
        }
        #endregion
        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult Forgotpassword()
        {

            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult Forgotpassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);
            string fixemail = FixedText.FixEmail(forgot.Email);
            User user= _userservices.GetuserByEmail(fixemail);
            if (user == null)
            {
                ModelState.AddModelError("email", "کاربری با این ایمیل یافت نشد.");
                return View(forgot);
            }
            string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه ی عبور", bodyEmail);
            ViewBag.IsSuccess = true;

            return View();
        }
        #endregion

        #region ResetPassword
    
        public IActionResult ResetPassword(string id)
        {

            return View(new RestartPasswordViewModel()
            {
                ActiveCode = id

            }); ;
        }
       
        [HttpPost]
        
        public IActionResult ResetPassword(RestartPasswordViewModel restart)
        {
            if (!ModelState.IsValid)
                return View(restart);
            User user = _userservices.GetuserByActiveCode(restart.ActiveCode);
            if (user==null)
            {
                return NotFound();

            }
            string hashNewPassword = PasswordHelper.EncodePasswordMd5(restart.Password);
            user.Password = hashNewPassword;
            _userservices.UpdateUser(user);
            return Redirect("/Login");

      
            
        }
        #endregion
        
    }
}

