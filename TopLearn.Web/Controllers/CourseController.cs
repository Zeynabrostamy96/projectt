using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Context;
using TapLearn.Layer.Entites.Course;
using TapLearn.Layer.Entites.Orders;
//using TapLearn.Layer.Entites.CoursGroup;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        private TopLearnContext _context;
        private ICourseGroupServices _CourseGroupServices;
        private IOrderService _orderService;
        private IUserService _userService;
        public CourseController(ICourseGroupServices CourseGroupServices, TopLearnContext context, IOrderService orderService, IUserService userService)
        {

            _CourseGroupServices = CourseGroupServices;
            _context = context;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Course(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.pageid = pageId;
            ViewBag.SelectedGroup = selectedGroups;
            ViewBag.Groups = _CourseGroupServices.GettGroup();
            return View(_CourseGroupServices.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9));
        }
        [Route("ShowCourse/{id}")]
        [Authorize]
        public IActionResult ShowCourse(int id)
        {

            List<TimeSpan> TotalTime = _context.courseEpisods.Where(e => e.CId == id).Select(e => e.EpisodeTime).ToList();
            var duration = new TimeSpan(TotalTime.Sum(e => e.Ticks));
            var th = (int)duration.TotalHours;
            var tm = (int)duration.TotalMinutes - (th * 60);
            var ts = "00";
            ViewData["DateTime"] = ($"{th}:{tm}:{ts}");
            var course = _CourseGroupServices.GetCurseForShow(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/UserPanel/MyOrder/ShowOrder/" + id);
        }
        [Route("DownloadFile/{episodeid}")]
        public IActionResult DownloadFile(int episodeid)
        {
            var episode = _CourseGroupServices.GetCourseEpisodById(episodeid);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseFile", episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);

            }
            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserInCourse(User.Identity.Name, episode.CId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }
            return Forbid();
        }
        [HttpPost]
        public IActionResult CreateComment(Note comment)
        {
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.IsAdminRead = false;
            comment.Userid = _userService.GetUserIdByUserName(User.Identity.Name);

            _CourseGroupServices.AddComment(comment);


            return View("ShowComment", _CourseGroupServices.GetCourseComment(comment.CourseId));
        }
        public IActionResult ShowComment(int id,int pageid=1)
        {
            return View(_CourseGroupServices.GetCourseComment(id,pageid));
        }
        public IActionResult CourseVote( int id)
        {
            if (!_CourseGroupServices.IsFree(id))
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name, id))
                {
                    ViewBag.NotAccess = true;

                };
            }
            return PartialView(_CourseGroupServices.GetCourseVote(id));
        }
        [Authorize]
        
        public IActionResult AddVote(int id,bool vote)
        {
            _CourseGroupServices.AddVote(_userService.GetUserIdByUserName(User.Identity.Name), id, vote);
            return PartialView("CourseVote", _CourseGroupServices.GetCourseVote(id));

        }
    }
  
}

