﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private ICourseGroupServices _courseGroupServices;
        public HomeController(IUserService userService, ICourseGroupServices courseGroupServices)
        {
            _userService = userService;
            _courseGroupServices = courseGroupServices;

        }
        public IActionResult Index()
        {
            var popular = _courseGroupServices.GetPopularCourse();
            ViewBag.PopularCourse = popular;
            return View(_courseGroupServices.GetCourse().Item1);
        }
        [Authorize]
      [Route("OnlinePayment/{id}")]
      public IActionResult OnlinePayment(int id)
       {
            if(HttpContext.Request.Query["status"]!=""
                && HttpContext.Request.Query["status"]
                .ToString().ToLower()=="ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWalletByWalletid(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }

            }
            return View();
       }
        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };

            list.AddRange(_courseGroupServices.GetSubGroupForManageCourse(id))  ;
            return Json(new SelectList(list, "Value", "Text"));
        }
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }
            var url = $"{"/MyImages/"}{fileName}";
            return Json(new { uploaded = true, url });
        }

    }
}
