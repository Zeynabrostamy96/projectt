using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Course;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    public class EditCourseGroupModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public EditCourseGroupModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public CourseGroup courseGroup { get; set; }
        public void OnGet(int id)
        {
            courseGroup = _courseGroupServices.GetGroupByid(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            _courseGroupServices.UpdateCourseGroup(courseGroup);
            return RedirectToPage("Index");
        }
    }
}
