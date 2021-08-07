using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class IndexEpisodeModel : PageModel
    {

        private ICourseGroupServices _courseGroupServices;

        public IndexEpisodeModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;

        }
        public List<CourseEpisod> courseEpisod { get; set; }
        public void OnGet(int id )
        {
            ViewData["CourseId"] = id;
            courseEpisod = _courseGroupServices.GetListEpisodeCourse(id);
        }
       
      
    }
}
