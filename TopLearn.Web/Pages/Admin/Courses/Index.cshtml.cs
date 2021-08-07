using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.DTOs.Course;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
       
        public IndexModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;

        }
        public List<ShowCourseForAdminViewModel> showCourse { get; set; }
        public void OnGet()
        {
           
            showCourse = _courseGroupServices.GetShowCourseforAdminviewmodels();
        }
    }
}
