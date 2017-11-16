using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Data;
using System.IO;
using TestEngine.Business.Services;
using TestEngine.Models.Models;
using TestEngine.Utility.Utils;


namespace TestEngine.Controllers
{
    public class SectionIntroController : BaseController
    {
        SectionService service = new SectionService();
        SectionIntroModel model = new SectionIntroModel();
      
        public ActionResult Index()
        {
            ExamBasicInfo emodel = new ExamBasicInfo();
            UserInfo umodel = new UserInfo();
            LanguageModel lmodel = new LanguageModel();

            model.eInfo = emodel;
            model.uInfo = umodel;
            model.lang = lmodel;

            //Getting Gloabal Variables
            model.redirecturl = GlobalVar.redirectutl;
            model.eInfo.examfilepath = GlobalVar.examfilepath;
            model.mastersetmain = GlobalVar.mastersetmain;
            model.mastersetin = GlobalVar.mastersetin;
            model.eInfo.wwwroot = GlobalVar.pathphotouploadfind;


            //read language
            model.lang.langFilePath= "E:\\Sukhvir TestEngine\\Files\\XmlFiles\\Language\\Language.xml";

            if (string.IsNullOrEmpty(Convert.ToString(Session["Languagetype"])))
            {
                model.lang.languageType = "2";
            }
            else
            {
                model.lang.languageType = Convert.ToString(Session["Languagetype"]);
            }

            lmodel = Common.ReadLanguageXML(model.lang.langFilePath,model.lang.languageType,"SI");
            model.lang = lmodel;


            //Creating file name
            Session["attemptid"] = Request.QueryString["attemptid"].ToString(); //Just for testing by Priyanka
            if (Session["attemptid"] != null)
            {
                model.eInfo.attemptid = Session["attemptid"].ToString();
                model.eInfo.filename = model.eInfo.attemptid;

                //model.eInfo.mainpath = System.Web.HttpContext.Current.Server.MapPath("~")+model.eInfo.examfilepath+"/";//.Replace(model.eInfo.wwwroot, "XmlFiles/" + model.eInfo.examfilepath.ToString() + "/");

                //model.eInfo.strFilename = model.eInfo.mainpath + model.eInfo.filename + ".xml";

                //model.eInfo.attemptid = Session["attemptid"].ToString();
                model.eInfo.strFilename= "E:\\XMLFile\\" + model.eInfo.attemptid + ".xml"; 
            }
            else
            {
                if (Request.ServerVariables["HTTP_USER_AGENT"].ToString() == "ERSEBNAUK")
                {
                    Response.Write("<html><head></head><body><input type='hidden' name='hdn_autoclose' id='hdn_autoclose' value='An unexpected event occured. Click OK to the close this window and Run/Download the executable file to resume your test.' /></body></html>");
                    Response.End();
                }
                else
                {
                    RedirectionURL("ErrorPage");
                }
            }

            
            if (!string.IsNullOrEmpty(Request.Form["secRev"]))
            {
                Session["attemptid"] = Request.Form["attemptid"].ToString();
                model.eInfo.attemptid = Convert.ToString(Session["attemptid"]); //Set attemptid in View Later
                model.hiddenattemptid = model.eInfo.attemptid;
                model.ssrev = "true";
            }

           

            //Caching
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();
          
            //Reading Exam File
            model=service.ReadXmlFile(model);


            model.secid = Convert.ToString(this.Request.QueryString["secid"]);
            model.codeassess = Convert.ToString(this.Request.QueryString["codeassess"]);
           // model.eInfo.strFilename = "E:\\XMLFile\\" + model.eInfo.attemptid + ".xml";


            if (model.snotime == "True")
            {
                model.examstimerem = "11:59:59";
            }
            else
            {
                model.examstimerem = model.stimerem;
            }
            

            string cssdata = "";



                if (!string.IsNullOrEmpty(model.secid))
                {
                    model.secchange = "true";
                    string newsectionid = model.secid;
                    service.LoadSectionQuestion(model);

                }
                else if (!string.IsNullOrEmpty(model.codeassess))
                {


                    if (System.IO.File.Exists(model.eInfo.strFilename))
                    {
                        XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);
                       // xmldoc.Load(model.eInfo.strFilename);

                        var sectiontype = (from tutorial in xmlDoc.Descendants("Section")
                                           where tutorial.Attribute("type").Value == "3"
                                           select new
                                           {
                                               secid = tutorial.Attribute("id").Value,
                                               seccomp = tutorial.Attribute("completed").Value,
                                               sectime = tutorial.Element("Time").Value,


                                           }).FirstOrDefault();


                        if (sectiontype.sectime != "00:00:00")
                        {


                            model.secchange = "true";
                            string newsectionid = sectiontype.secid.ToString();
                            model.scurrentsection = newsectionid;
                            model.scurrentquestion = "1";
                        }
                        else
                        {
                            service.LoadMainQuestion(model);

                        }
                    }
                }
                else
                {


                    if (!string.IsNullOrEmpty(Request.Form["hdnsectionid"]))
                    {
                        model.shdnsectionid = Request.Form["hdnsectionid"].ToString();

                    }

                    if (!string.IsNullOrEmpty(Request.Form["hdnvarid"]))
                    {
                        model.shdnvarid = Request.Form["hdnvarid"].ToString();

                    }

                    if (!string.IsNullOrEmpty(Request.Form["timet"]))
                    {
                        model.revstimerem = Request.Form["timet"].ToString();

                    }

                    if (!string.IsNullOrEmpty(Request.Form["hdnvarSrNo"]))
                    {
                        model.shdnvarSrNo = Request.Form["hdnvarSrNo"].ToString();

                    }
                    if (!string.IsNullOrEmpty(Request.Form["revcurrenttime"]))
                    {
                        model.revcurrenttime = Request.Form["revcurrenttime"].ToString();

                    }

                    //scurrentsection = 1;

                 model=service.LoadMainQuestion(model);

                    if (model.sRevType == "exam" || model.sRevType == "sec")
                    {

                        if (model.sreviewbutton == "true" || model.sreviewbutton == "true2")
                        {

                            model.scurrentquestion = model.shdnvarid;
                            model.scurrentsection = model.shdnsectionid;
                            service.CreateHidden(model.eInfo.strFilename,model.shdnsectionid,model.shdnvarid);
                        }
                    }
                        

                    try
                    {
                        int test = Convert.ToInt32(model.scurrentquestion);
                    }
                    catch
                    {
                        model.sreviewbutton = "false";
                        service.LoadMainQuestion(model);
                    }
                }

               model= service.LoadXml(model);
            Session["redurl"] = model.redirecturl;

            string st = "";
                st += "<input type='hidden' name='hdn_mac' id='hdn_mac' value='' />";
                st += "<input type='hidden' name='hdn_attemptid' id='hdn_attemptid' value=" + model.eInfo.attemptid + " />";
                st += "<input type='hidden' name='hdn_exitifcamchanged' id='hdn_exitifcamchanged' value='1' />";

                if (Request.ServerVariables["HTTP_USER_AGENT"].ToString() == "ERSEBNAUK")
                {
                    ViewBag.SEBText = st;
                    // SEB.InnerHtml = st; --priyanka
                }

                if (Session["attemptid"] != null)
                {
                    model.eInfo.attemptid = Session["attemptid"].ToString();
                }
                else
                {
                    if (Request.ServerVariables["HTTP_USER_AGENT"].ToString() == "ERSEBNAUK")
                    {

                        Response.Write("<html><head></head><body><input type='hidden' name='hdn_autoclose' id='hdn_autoclose' value='An unexpected event occured. Click OK to the close this window and Run/Download the executable file to resume your test.' /></body></html>");
                        Response.End();

                    }
                    else
                    {
                        RedirectionURL("ErrorPage");
                        //  Response.Redirect(redirecturl);
                    }
                }

                if (System.IO.File.Exists(model.eInfo.strFilename))
                {
                    XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);
                    cssdata = (from tutorial in xmlDoc.Descendants("Test")
                               select tutorial.Element("cssData").Value).Single().ToString();

                    ViewBag.header1ImgURL = service.StoreVal(cssdata, "Header");

                //  header1.ImageUrl = StoreVal(cssdata, "Header"); --priyanka
            }

            //if (scorpid == "1209327" || scorpid == "1209328") 
            //{
            //    ViewBag.stylesheetpath1Href = "css/stylecanew.css";
            //    ViewBag.header1ImageUrl= "http://www.expertratinginc.com/ca/images/ca-image.png";
            //    //header1.ImageUrl = "http://www.expertratinginc.com/ca/images/ca-image.png";
            //    header1.Style.Add("max-height", "70px");
            //    header1.Style.Add("max-width", "350px");
            //}
            //else if (scorpid == "1252039")
            //{
            //    stylesheetpath1.Href = "css/styleey.css";
            //    //header1.ImageUrl = "http://www.expertratinginc.com/newinterface/images/EY.gif";
            //    header1.Style.Add("max-height", "70px");
            //    header1.Style.Add("width", "auto !important");
            //}
            //else
            //{
            //    stylesheetpath1.Href = "css/style.css";
            //}

            if (model.sbseb == "Y")
                {
                    ViewBag.lblbsebjsText = "<script type='text/javascript' src='/cdm/browserseb/includes/bsebscreenconfigs.js'></script>";
                    //lblbsebjs.Text = "<script type='text/javascript' src='/cdm/browserseb/includes/bsebscreenconfigs.js'></script>";
                    StreamReader fsr = new StreamReader(Server.MapPath("/browserseb/includes/bsebscreenconfigs.js"));
                    string scriptbsebjs = fsr.ReadToEnd();
                    fsr.Close();
                    fsr.Dispose();
                    if (model.scorpid == "1252039")
                    {
                        ViewBag.lblbsebjsText = "<script type='text/javascript' src='/browsersebeny/includes/bsebscreenconfigs.js'></script>'";
                        // lblbsebjs.Text = "<script type='text/javascript' src='/browsersebeny/includes/bsebscreenconfigs.js'></script>'";
                    }
                    else
                    {
                        ViewBag.lblbsebjsText = "<script type='text/javascript' src='/browserseb/includes/bsebscreenconfigs.js'></script>'";
                        //lblbsebjs.Text = "<script type='text/javascript' src='/browserseb/includes/bsebscreenconfigs.js'></script>'";
                    }
                }
            
            return View(model);
        }

        public ActionResult AbortPage()
        {
            return View();
        }

        public ActionResult ExamPage()
        {
            return View();
        }

        public void RedirectionURL(string Action)
        {
            RedirectToAction(Action);
        }

        public string LoadExam()
        {
            string redurl = Convert.ToString(Session["redurl"]);

            if (string.IsNullOrEmpty(redurl))
            {
                if (Request.ServerVariables["HTTP_USER_AGENT"].ToString() == "ERSEBNAUK")
                {
                    Response.Write("<html><head></head><body><input type='hidden' name='hdn_autoclose' id='hdn_autoclose' value='An unexpected event occured. Click OK to the close this window and Run/Download the executable file to resume your test.' /></body></html>");
                    Response.End();
                }
                else
                {
                    RedirectionURL(model.redirecturl);
                }
            }

            Session["values"] = null;

            //for testing
            RedirectToAction("Index", "Exam",model);

            return redurl;
            

        }
    }
}