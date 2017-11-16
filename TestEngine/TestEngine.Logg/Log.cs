using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using context = System.Web.HttpContext;
using TestEngine.DataBase;

namespace TestEngine.Log
{
    public class Log : ILog
    {
        Demo_sukhvirEntities db = new Demo_sukhvirEntities();
        private Log()
        {
        }
        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());

        public static Log GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        /// <summary>
        /// Use to insert log details in ExceptionLogs table
        /// </summary>
        /// <param name="message"></param>
        public void LogException(Exception message)
        {
           
            string StackTraceMsg = message.StackTrace.ToString();
            string[] AppendLineSplit = StackTraceMsg.Split(':');
            string[] LineNo = AppendLineSplit[2].Split('\r');
          
            ExceptionLog tbl = new ExceptionLog();
            tbl.LogLineNumber =LineNo[0];
            tbl.LogMessage = message.Message.ToString();
            tbl.LogTime = DateTime.Now.ToString();
            tbl.LogUrl = context.Current.Request.Url.ToString();
            tbl.StackTime = message.StackTrace.ToString();
            db.ExceptionLogs.Add(tbl);
            db.SaveChanges();


        }
    }
}
