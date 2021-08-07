using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
//using TapLearn.Core.Course;
using TapLearn.Core.DTOs.Course;
//using TapLearn.Layer.Entites.CoursGroup;
using TapLearn.Layer.Entites.Course;
using TapLearn.Layer.Entites.Orders;

namespace TapLearn.Core.Services.interfaces
{
    public interface ICourseGroupServices
    {
        #region Group
        List<CourseGroup> GettGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int id);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevel();
        List<SelectListItem> GetStatuse();
        Tuple<List<ShowCourseListItemViewModel>,int>   GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
        
        int AddCourse(IFormFile ImagCourseUP, IFormFile demoUP,Curse course);
        List<ShowCourseForAdminViewModel> GetShowCourseforAdminviewmodels();
        Curse GetCourseById(int id);
        void UpdateCourse(IFormFile ImagCourseUP, IFormFile demoUP, Curse course);
        Curse GetCurseForShow(int courseid);
        List<ShowCourseListItemViewModel> GetPopularCourse();
        
        

        #endregion
        #region EpisodeCourse
        List<CourseEpisod> GetListEpisodeCourse(int courseid);
        bool CheckExistFile(string formFile);
        int AddEpisode(CourseEpisod Episod,IFormFile formFile);
        CourseEpisod GetCourseEpisodById(int id);
        void EditEpisode(CourseEpisod Episod, IFormFile formFile);

        #endregion
        #region comment
        void AddComment(Note comment);
        Tuple<List<Note>, int> GetCourseComment(int Courseid, int PageId = 1);
        #endregion
        #region CourseGroup
        List<CourseGroup> GetAllCourseGroup();
        void AddGroup(CourseGroup group);
        CourseGroup GetGroupByid(int GroupId);
        void UpdateCourseGroup(CourseGroup @courseGroup);
        bool IsFree(int corseid);
        #endregion
        #region CourseVote
        void AddVote(int useerid, int courseid, bool vote);
        Tuple<int, int> GetCourseVote(int courseid);
        #endregion
    }
}
