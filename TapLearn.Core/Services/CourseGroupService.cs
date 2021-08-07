using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TapLearn.Core.Convertors;
//using TapLearn.Core.Course;
using TapLearn.Core.DTOs.Course;
using TapLearn.Core.Generator;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Context;
//using TapLearn.Layer.Entites.CoursGroup;
using TapLearn.Core.Security;
using TapLearn.Layer.Entites.Course;
using TapLearn.Layer.Entites.Orders;
using System.Data.SqlClient;

namespace TapLearn.Core.Services
{
    public class CourseGroupService : ICourseGroupServices
    {
        private TopLearnContext _context;
        public CourseGroupService(TopLearnContext context)
        {
            _context = context;
        }

        public void AddComment(Note comment)
        {
      
                _context.notes.Add(comment);
                _context.SaveChanges();
       
           
        }
        public int AddCourse(IFormFile ImagCourseUP, IFormFile demoUP, Curse course)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            //TODO Check Image
            if (ImagCourseUP != null && ImagCourseUP.IsImage())
            {
               
                string imagepath = "";


                course.CourseImageName = NameGenerater.GenerateUniqCode() + Path.GetExtension(ImagCourseUP.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                   ImagCourseUP.CopyTo(stream);
                }
                //TODO Image Resize
                ImageConvertor Image_resize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);
                Image_resize.Image_resize(imagepath, thumbPath,150);
            }

            if (demoUP != null)
            {
                string demopath = "";
                course.DemoFileName = NameGenerater.GenerateUniqCode() + Path.GetExtension(demoUP.FileName);
                demopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demoes", course.DemoFileName);
                using (var stream = new FileStream(demopath, FileMode.Create))
                {
                    demoUP.CopyTo(stream);
                }
            }

            _context.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

       

        public int AddEpisode(CourseEpisod Episod, IFormFile formFile)
        {

            Episod.EpisodeFileName = formFile.FileName;
            string filePath = "";
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseFile", Episod.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            _context.courseEpisods.Add(Episod);
            _context.SaveChanges();
            return Episod.EpisodeId;
        }

        public bool CheckExistFile(string formFile)
        {
            string filePath = "";
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseFile", formFile);
            return File.Exists(filePath);


        }

        public void EditEpisode(CourseEpisod Episod, IFormFile formFile)
        {
            if(formFile != null)
            {
                string DeleteFilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseFile", Episod.EpisodeFileName);
                if (File.Exists(DeleteFilepath))
                {
                    File.Delete(DeleteFilepath);
                }
                Episod.EpisodeFileName = formFile.FileName;
                string filePath = "";
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseFile", Episod.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
              

            }
            _context.Update(Episod);
            _context.SaveChanges();


        }
    
        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            #region comment
            //if (take == 0)
            //    take = 8;
            //IQueryable<Curse> result = _context.curses;
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    result = result.Where(c => c.CourseTitel.Contains(filter));
            //}
            //switch (getType)
            //{
            //    case "all":
            //        break;
            //    case "buy":
            //        {
            //            result = result.Where(c => c.CoursePrice != 0);
            //            break;
            //        }
            //    case "free":
            //        {
            //            result = result.Where(c => c.CoursePrice == 0);
            //        }
            //        break;
            //}
            //switch (orderByType)
            //{
            //    case "date":
            //        {
            //            result = result.OrderByDescending(c => c.CreateDate);
            //            break;
            //        }
            //    case "updatedate":
            //        {
            //            result = result.OrderByDescending(c => c.UpdateData);
            //            break;
            //        }
            //}
            //if (startPrice > 0)
            //{
            //    result = result.Where(c => c.CoursePrice > startPrice);
            //}
            //if (endPrice > 0)
            //{
            //    result = result.Where(c => c.CoursePrice < endPrice);
            //}
            //if (selectedGroups != null && selectedGroups.Any())
            //{
            //    foreach (int groupid in selectedGroups)
            //    {
            //        result = result.Where(c => c.GroupId == groupid || c.SubGroup == groupid);
            //    }
            //}
            //int skip = (pageId - 1) * take; ;

            //return _context.curses
            //.Include(e => e.courseEpisods)
            //.Select(c => new ShowCourseListItemViewModel()
            //{
            //    CourseId = c.CourseId,
            //    Title = c.CourseTitel,
            //    ImageName = c.CourseImageName,
            //    Price = c.CoursePrice,
            //    TotalTime = _context.courseEpisods.Where(e => e.CourseId == c.CourseId).Select(e => e.EpisodeTime).ToList()
            //}).Skip(skip).Take(take).ToList();
            #endregion
            if (take == 0)
                take = 9;

            IQueryable<Curse> result = _context.curses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitel.Contains(filter)||c.Tags.Contains(filter));
                
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }

            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateData);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < startPrice);
            }


            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int groupid in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupid || c.SubGroup == groupid);
                }
            }

           int skip = (pageId - 1) * take;
           int pageCount=  result.Include(c => c.courseEpisods).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitel,
                
            }).Count()/take;

            var query= result.Include(c => c.courseEpisods).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitel,
                TotalTime = _context.courseEpisods.Where(e => e.CId == c.CourseId).Select(e => e.EpisodeTime).ToList()
            }).Skip(skip).Take(take).ToList();
            return Tuple.Create(query, pageCount);
        }

        public Tuple<List<Note>, int> GetCourseComment(int Courseid, int PageId = 1)
        {
            //یعنی در هرصفحه چندتانمایش بده
            int take = 5;
            //پرش
            int skip = (PageId - 1) * take;
          
            //چندتاصفحه داریم
            int pagecount = _context.notes.Where(c => c.CourseId==Courseid && !c.IsDelete).Count() / take;
            if((pagecount%2) != 0)
            {
                pagecount += 1;
            }
            return Tuple.Create(_context.notes.Where(c => !c.IsDelete && c.CourseId==Courseid).Skip(skip).Take(take).Include(c => c.user).OrderByDescending(c=>c.CreateDate).ToList(), pagecount);
        }

        public Curse GetCourseById(int id)
        {
            return _context.curses.Find(id);
        }
        public CourseEpisod GetCourseEpisodById(int id)
        {
            return _context.courseEpisods.Find(id);
        }

        public Curse GetCurseForShow(int courseid)
        {
            return _context.curses
                .Include(c =>c.CourseStatus).Include(c=>c.user)
                .Include(c=>c.courseEpisods)
                .Include(c=>c.CourseLevel)
                .Include(c=>c.UseCourses)
                .FirstOrDefault(c=>c.CourseId==courseid);
        }
        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.courseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitel,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        

        public List<SelectListItem> GetLevel()
        {
            return _context.courseLevels
                .Select(l => new SelectListItem
                {
                    Value=l.LevelId.ToString(),
                    Text=l.LevelTitle

                }).ToList();
        }

        public List<CourseEpisod> GetListEpisodeCourse(int courseid)
        {
            return _context.courseEpisods.Where(e => e.CId == courseid).ToList();
        }

        public List<ShowCourseForAdminViewModel> GetShowCourseforAdminviewmodels()
        {
            return _context.curses.Include(c=>c.courseEpisods)
                .Select(c => new ShowCourseForAdminViewModel
                {
                Courseid=c.CourseId,
                Title=c.CourseTitel,
                EpisodeCount=c.courseEpisods.Count(),
                ImageName=c.CourseImageName,
                


                }).ToList();
        }

        public List<SelectListItem> GetStatuse()
        {
            return _context.courseStatuses
                .Select(s => new SelectListItem()
                {
                    Value = s.StatusId.ToString(),
                    Text = s.StatusTitel
                }).ToList();

        }

        public List<SelectListItem> GetSubGroupForManageCourse(int id)
        {
            return _context.courseGroups.Where(g =>g.ParentId==id )
         .Select(g => new SelectListItem()
         {
             Value = g.GroupId.ToString(),
             Text = g.GroupTitel,
            
         }).ToList();
        }

      

        public List<SelectListItem> GetTeachers()
        {
            return _context.userRoles.Where(t => t.Roleid == 1006).Include(t=>t.User)
                .Select(t => new SelectListItem()
                {
                    Value =t.Userid.ToString(),
                    Text = t.User.UserName
                }).ToList();
        }

        public List<CourseGroup> GettGroup()
        {
            return _context.courseGroups.ToList();
        }

        public void UpdateCourse(IFormFile ImagCourseUP, IFormFile demoUP, Curse course)
        {
            course.CreateDate = DateTime.Now;
            if (ImagCourseUP != null && ImagCourseUP.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
     
                    string Deleteimagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);
                    if (File.Exists(Deleteimagepath))
                    {
                         File.Delete(Deleteimagepath);
                     }
                    string Deletethumbpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);
                    if (File.Exists(Deletethumbpath))
                    {
                        File.Delete(Deletethumbpath);
                    }


                }

                string imagepath = "";


                course.CourseImageName = NameGenerater.GenerateUniqCode() + Path.GetExtension(ImagCourseUP.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/image", course.CourseImageName);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    ImagCourseUP.CopyTo(stream);
                }
                //TODO Image Resize
                ImageConvertor Image_resize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thumb", course.CourseImageName);
                Image_resize.Image_resize(imagepath, thumbPath, 150);
            }

            if (demoUP != null)
            {
                if(course.DemoFileName != null)
                {
                    string Deletedemopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demoes", course.DemoFileName);
                    if (File.Exists(Deletedemopath))
                    {
                        File.Delete(Deletedemopath);
                    }
                }
               
                string demopath = "";
                course.DemoFileName = NameGenerater.GenerateUniqCode() + Path.GetExtension(demoUP.FileName);
                demopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/demoes", course.DemoFileName);
                using (var stream = new FileStream(demopath, FileMode.Create))
                {
                    demoUP.CopyTo(stream);
                }
            }
            _context.Update(course);
            _context.SaveChanges();

        }

        public List<ShowCourseListItemViewModel> GetPopularCourse()
        {
            return _context.curses.Include(c => c.OrderDetails)
                .Where(c=>c.OrderDetails.Any())
                .OrderByDescending(d => d.OrderDetails.Count)
                .Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitel,
                    TotalTime = _context.courseEpisods.Where(e => e.CId == c.CourseId).Select(e => e.EpisodeTime).ToList()

                }).ToList();



        }

        public List<CourseGroup> GetAllCourseGroup()
        {
            return _context.courseGroups.Include(C=>C.CourseGroups).ToList();
        }

        public void AddGroup(CourseGroup group)
        {
            _context.courseGroups.Add(group);
            _context.SaveChanges();
        }

        public CourseGroup GetGroupByid(int GroupId)
        {
            return _context.courseGroups.Find(GroupId);
        }

        public void UpdateCourseGroup(CourseGroup courseGroup)
        {
            _context.courseGroups.Update(courseGroup);
            _context.SaveChanges();
        }

        public void AddVote(int useerid, int courseid, bool vote)
        {
           
            var uservote = _context.courseVoltes.Include(c=>c.Course).FirstOrDefault(c => c.userId == useerid && c.CourseId == courseid);
            if(uservote != null)
            {
                uservote.Volte = vote;

            }
            else
            {
                uservote = new CourseVolte()
                {
                   CourseId=courseid,
                    userId=useerid,
                    Volte=vote

                };
                _context.Add(uservote);
            }
            _context.SaveChanges();
        }

        public Tuple<int, int> GetCourseVote(int courseid)
        {
            var votes = _context.courseVoltes.Where(c => c.CourseId == courseid).Select(c=>c.Volte).ToList();
            return Tuple.Create(votes.Count(c => c = true), votes.Count(c => !c));
        }

        public bool IsFree(int corseid)
        {
            return _context.curses.Where(c => c.CourseId == corseid).Select(c=>c.CoursePrice).First()==0;
        }
    }
}
