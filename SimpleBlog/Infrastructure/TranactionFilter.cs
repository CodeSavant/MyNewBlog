using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{

    // global action filter. do not need to add attribute to action.
    public class TranactionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Database.Session.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                Database.Session.Transaction.Commit();
            }
            else
            {
                Database.Session.Transaction.Rollback();
            }
        }
    }
}