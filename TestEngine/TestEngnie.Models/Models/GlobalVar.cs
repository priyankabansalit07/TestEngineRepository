using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestEngnie.Models.Models
{
    public static class GlobalVar
    {
        public const string pathphotouploadfind = "TestEngine";// "firstnaukri";
        public const string mastersetmain = "MasterSettings";
        public const string masterinstruct = "testsettinghistory";
        public const string mastersetin = "fnaukri";
        public const string redirecturlglobal = "/SectionIntro/ErrorPage/";
        public const string redirectutlseb = "http://assessments.firstnaukri.com/";
        public const string imagepath = "wwwroot";

        public const string examfilepath = @"\XmlFiles\TakeTest";
        //   public const string examfilepath = @"\xmlfiles\elance6500";

        public const string stylesheetpath = "http://expertrating.com/employers/css/redmond/style1qwqqw.css";

        public const string headerpath = "http://www.expertrating.com/employers/images/header/jetairways1.gif";

        public const string redirectutl = "/SectionIntro/ErrorPage/";

        static int _globalValue;

        public static int GlobalValue
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        }

        public static bool GlobalBoolean;

    }
}