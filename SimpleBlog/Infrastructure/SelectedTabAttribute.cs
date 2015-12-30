using System;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _selectedtab;

        public SelectedTabAttribute(string selectedtab)
        {
            _selectedtab = selectedtab;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SelectedTab = _selectedtab;
        }
    }
}