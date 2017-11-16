using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestEngine.DataBase;
using TestEngine.Log;

namespace TestEngine.Controllers
{
    public class BaseController : Controller
    {
        private ILog _ILog;
        private Demo_sukhvirEntities db = new Demo_sukhvirEntities();
        public BaseController()
        {
            _ILog = Log.Log.GetInstance;
        }

        /// <summary>
        /// Overriding the OnException method for exception
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)

        {
            _ILog.LogException(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            this.View("Error").ExecuteResult(this.ControllerContext);
        }
    }
}