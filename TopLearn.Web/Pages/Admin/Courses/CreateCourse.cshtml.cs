using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Course;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public CreateCourseModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public Curse course { get; set; }
        public void OnGet()
        {
            var groups= _courseGroupServices.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value","Text");
            var subgroups = _courseGroupServices.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["Subgroups"] = new SelectList(subgroups, "Value", "Text");
            var techers = _courseGroupServices.GetTeachers();
            ViewData["Techers"] = new SelectList(techers, "Value", "Text");
            var statues = _courseGroupServices.GetStatuse();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");
            var levels = _courseGroupServices.GetLevel();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

        }
        public  IActionResult OnPost( IFormFile ImagCourseUP, IFormFile demoUP)
        {
            if (!ModelState.IsValid)
                return Page();
            _courseGroupServices.AddCourse(ImagCourseUP, demoUP, course);
           return  RedirectToPage("index");
        }
    }
}
