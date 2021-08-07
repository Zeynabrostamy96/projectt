using System;
using System.Collections.Generic;
using System.Text;
//using TapLearn.Layer.Entites.CoursGroup;

namespace TapLearn.Core.DTOs.Course
{
    public class ShowCourseListItemViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        //public List<CourseEpisod> courseEpisods { get; set; }
        public List<TimeSpan> TotalTime { get; set; }

    }
}
