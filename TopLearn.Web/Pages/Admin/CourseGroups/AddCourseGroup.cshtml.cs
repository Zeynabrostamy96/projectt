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
    public class AddCourseGroupModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public AddCourseGroupModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public CourseGroup courseGroup { get; set; }
        public void OnGet(int? id )
        {
            courseGroup = new CourseGroup()
            {
                ParentId = id,
            };

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            _courseGroupServices.AddGroup(courseGroup);

            return RedirectToPage("Index");
        }
    }
}
