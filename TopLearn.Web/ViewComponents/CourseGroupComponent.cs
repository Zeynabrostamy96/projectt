using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.ViewComponents
{
    public class CourseGroupComponent: ViewComponent
    {
        private ICourseGroupServices _courseGroupServices;
        public CourseGroupComponent(ICourseGroupServices courseGroupServices)
        {
            _courseGroupServices = courseGroupServices;
        }    
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            return  await Task.FromResult((IViewComponentResult)View("CourseGroup",_courseGroupServices.GettGroup()));
        }
    }
}
