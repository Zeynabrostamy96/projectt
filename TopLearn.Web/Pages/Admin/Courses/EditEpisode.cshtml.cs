using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class EditEpisodeModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public EditEpisodeModel(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public CourseEpisod courseEpisod { get; set; }
        public void OnGet(int id)
        {
            courseEpisod = _courseGroupServices.GetCourseEpisodById(id);
            
        }
        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
                return Page();
            if (fileEpisode != null)
            {
                if (_courseGroupServices.CheckExistFile(fileEpisode.FileName))
                {
                    ViewData["IsExistFile"] = true;
                    return Page();
                }
            }
            _courseGroupServices.EditEpisode(courseEpisod, fileEpisode);
            return Redirect("/Admin/Courses/IndexEpisode"+"?id="+courseEpisod.CId);
        }
    }
}
