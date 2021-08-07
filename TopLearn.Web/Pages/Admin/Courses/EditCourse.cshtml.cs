using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TapLearn.Layer.Entites.Course;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public EditCourseModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public Curse course { get; set; }
        public void OnGet(int id)
        {
            course = _courseGroupServices.GetCourseById(id);
            var groups = _courseGroupServices.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text",course.GroupId);
            var subgroups = _courseGroupServices.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["Subgroups"] = new SelectList(subgroups, "Value", "Text", course.SubGroup??0);
            var techers = _courseGroupServices.GetTeachers();
            ViewData["Techers"] = new SelectList(techers, "Value", "Text",course.TeacherId);
            var statues = _courseGroupServices.GetStatuse();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text",course.SId);
            var levels = _courseGroupServices.GetLevel();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text",course.Lid);
        }
        public IActionResult OnPost(IFormFile ImagCourseUP, IFormFile demoUP)
        {
            if (!ModelState.IsValid)
                return Page();
            _courseGroupServices.UpdateCourse(ImagCourseUP, demoUP,course);
            return RedirectToPage("Index");
    
        }
    }
}
