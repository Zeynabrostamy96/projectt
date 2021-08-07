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
    public class CreateEpisodeModel : PageModel
    {
        private ICourseGroupServices _courseGroupServices;
        public CreateEpisodeModel(ICourseGroupServices courseGroupServices )
        {
            _courseGroupServices = courseGroupServices;
        }
        [BindProperty]
        public CourseEpisod courseEpisod { get; set; }
        public void OnGet(int id )
        {
            courseEpisod  = new CourseEpisod();
            courseEpisod.CId = id;
        }
        public IActionResult OnPost( IFormFile fileEpisode)
        {
            if (!ModelState.IsValid || fileEpisode == null)
                return Page();
            if (_courseGroupServices.CheckExistFile(fileEpisode.FileName))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }
            _courseGroupServices.AddEpisode(courseEpisod, fileEpisode);
            return Redirect("/Admin/Courses/IndexEpisode" + "?id=" + courseEpisod.CId);
        }
    }
}
