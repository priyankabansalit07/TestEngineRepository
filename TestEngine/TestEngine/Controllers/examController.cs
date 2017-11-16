using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TestEngnie.Models.Models;
using TestEngine.Business.Services;
using System.Xml;



namespace TestEngine.Controllers
{
    
    public class examController : Controller
    {
        SectionService sv = new SectionService();
        examService es = new examService();
        
        // GET: exam
        public ActionResult Index(SectionIntroModel model)
        {


            UserInfo uInfo = new UserInfo();
            ExamBasicInfo eInfo = new ExamBasicInfo();
            LanguageModel lmodel = new LanguageModel();

            model.uInfo = uInfo;
            model.eInfo = eInfo;
            model.lang = lmodel;

            model.lang.langFilePath = "E:\\Sukhvir TestEngine\\Files\\XmlFiles\\Language\\Language.xml";

            if (string.IsNullOrEmpty(Convert.ToString(Session["Languagetype"])))
            {
                model.lang.languageType = "2";
            }
            else
            {
                model.lang.languageType = Convert.ToString(Session["Languagetype"]);
            }

            lmodel = Common.ReadLanguageXML(model.lang.langFilePath, model.lang.languageType,"EP");
            model.lang = lmodel;

            return View(model);





            //bool sessionOut = false;
            
            //model.startTime = DateTime.Now;

            //model.redirecturl = GlobalVar.redirectutl;
            //model.eInfo.examfilepath = GlobalVar.examfilepath;
            //model.mastersetmain = GlobalVar.mastersetmain;
            //model.mastersetin = GlobalVar.mastersetin;
            //model.eInfo.wwwroot = GlobalVar.pathphotouploadfind;

            //if (Session["attemptid"] == null)
            //{
            //    if (!string.IsNullOrEmpty(model.eInfo.attemptid))
            //    {
            //        Session["attemptid"] = model.eInfo.attemptid;
            //        sessionOut = true;
            //    }
            //    else
            //    {
            //        RedirectToAction(model.redirecturl);
            //    }

            //}
            //else
            //{
            //    model.eInfo.attemptid = Convert.ToString(Session["attemptid"]);
            //}

            //model = sv.ReadXmlFile(model);

            //XDocument xmldoc = Common.LoadXmlFile(model.eInfo.strFilename);
           
            //if (xmldoc != null)
            //{
            //    var result = (from tutorial in xmldoc.Descendants("Section")
            //                  where tutorial.Attribute("id").Value == model.scurrentsection
            //                  select new
            //                  {

            //                      vartype = tutorial.Attribute("type").Value,

            //                  }).FirstOrDefault();

            //    if (result.vartype == "6")
            //    {
            //        Response.Redirect("essayengine.aspx");
            //    }
            //}
            //else
            //{
            //    RedirectToAction(model.redirecturl);
            //}

            //if (Convert.ToInt32(model.scurrentquestion.ToString()) > Convert.ToInt32(model.stotquesection.ToString()))
            //{
            //    if (model.sreviewbutton != "false")
            //    {
            //        sv.CompleteSection(model.eInfo.strFilename, model.currentsec);
            //    }

            //    model = sv.Processing(model);
            //}
            //if (model.ssecview == "1")
            //{

            //    if (!string.IsNullOrEmpty(Request.Form["newsectionid"]))
            //    {
            //        model.secchange = "true";
            //        model.newsecid = Request.Form["newsectionid"].ToString();
            //        int index = model.newsecid.IndexOf(",");
            //        if (index > 0)
            //        {
            //            model.newsecid = model.newsecid.Substring(0, index);
            //        }
            //       UpdateXML("4",model);
            //    }

            //    if (!string.IsNullOrEmpty(Request.Form["endtest"]))
            //    {
            //        if (Request.Form["endtest"].ToString() == "1")
            //        {

            //            es.CompleteSectionHist("E",model.scurrentsection,model.stimerem,model.eInfo.strFilename);
            //            Response.Redirect("../Result-test.aspx?newui=true");
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(Request.Form["timeupsec"]))
            //{
            //    UpdateXML("1",model);

            //    sv.CompleteSection(model.eInfo.strFilename,model.scurrentsection);
            //    processingexit();
            //}



            //if (System.IO.File.Exists(model.eInfo.strFilename))
            //{
            //    XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);
            //    string resultfirst = (from tutorial in xmlDoc.Descendants("completed")

            //                          select tutorial.Value).Single().ToString();


            //    //   Response.Write(resultfirst);


            //    if (resultfirst == "completed")
            //    {
            //        Response.Redirect(model.redirecturl);
            //    }


            //}



            //string st = "";
            //st += "<input type='hidden' name='hdn_mac' id='hdn_mac' value='' />";
            //st += "<input type='hidden' name='hdn_attemptid' id='hdn_attemptid' value=" + Session["attemptid"].ToString() + " />";
            //st += "<input type='hidden' name='hdn_exitifcamchanged' id='hdn_exitifcamchanged' value='1' />";

            //if (Request.ServerVariables["HTTP_USER_AGENT"].ToString() == "ERSEBNAUK")
            //{

            //    SEB.InnerHtml = st;

            //}





            //if (model.sresume != "")
            //{
            //    model.scurrentquestion = model.squestionid.ToString();

            //    model.sresume = "";

            //}
            //if (model.scurrentquestion == "")
            //{
            //    model.scurrentquestion = "1";
            //}


            //if (Page.IsPostBack == false)
            //{

            //    es.updatetime(model.eInfo.strFilename);

            //}

            //if (model.sreviewbutton != "")
            //{
            //    if (model.sreviewbutton.ToString() == "false")
            //    {
            //        //    txtReview.Visible = false;
            //    }
            //    else
            //    {
            //        if (model.sRevType == "rev-sec")
            //        {
            //            model.sreviewbutton = "true2";

            //        }
            //        //     txtReview.Visible = true;
            //    }
            //}
            //else
            //{
            //    //   txtReview.Visible = false;
            //}




            ////now adding the bsebscreenconfigs.js if the store contains isbsebenabled = Y
            //if (model.sbseb == "Y")
            //{
            //    //lblbsebjs.Text = "<script type='text/javascript' src='/cdm/browserseb/includes/bsebscreenconfigs.js'></script>";
            //    StreamReader fsr = new StreamReader(Server.MapPath("/browserseb/includes/bsebscreenconfigs.js"));
            //    string scriptbsebjs = fsr.ReadToEnd();
            //    fsr.Close();
            //    fsr.Dispose();

            //    if (model.scorpid == "1252039")
            //    {
            //        lblbsebjs.Text = "<script type='text/javascript' src='/browsersebeny/includes/bsebscreenconfigs.js'></script>'";
            //    }
            //    else
            //    {
            //        lblbsebjs.Text = "<script type='text/javascript' src='/browserseb/includes/bsebscreenconfigs.js'></script>'";
            //    }
            //}

            //return View(model);
        }

        //public void UpdateXML(string type, SectionIntroModel model)
        //{

        //    string newquesthist = "";
        //    string questimehist = "";
        //    string newelapsednew = "", newsavetimerem = "";


        //    if (model.sreviewbutton.ToString() == "false")
        //    {

        //        if (model.scurrentquestion == "1")
        //        {

        //            if (!model.queshist.Contains("MSEC##" + model.scurrentsection + "$$"))
        //            {
        //                questimehist += "MSEC##" + model.scurrentsection + "$$";
        //            }
        //        }
        //    }

        //    if (type == "1")
        //    {
        //        questimehist += "N##";
        //    }
        //    else if (type == "2")
        //    {
        //        questimehist += "P##";
        //    }
        //    else if (type == "3")
        //    {
        //        questimehist += "R##";
        //    }
        //    else if (type == "4")
        //    {
        //        questimehist += "V##";
        //    }

        //    questimehist += model.scurrentsection + "##" + model.scurrentquestion + "##" + model.stimerem;


        //    string next = "";
        //    string oldstrem = "", newstrem = "";


        //    string stt = "";
        //    string backen = "";

        //    es.updateQuesTime(model.eInfo.strFilename, model.scurrentsection, model.scurrentquestion, model.sRevType, model.stimerem);


        //    //string filename = Session["attemptid"].ToString();
        //    //string mainpath = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/");
        //    //string strFilename = mainpath + filename + ".xml";

        //    XmlDocument xmldoc = new XmlDocument();

        //    DateTime temp;
        //    DateTime temp1;

        //    string elapsed = "", elapsednew = "";

        //    string diff = "";


        //    oldstrem = model.stimerem;

        //    if (!string.IsNullOrEmpty(Request.Form["timet"]) && IsPageRefresh == false)
        //    {
        //        stt = Request.Form["timet"].ToString();
        //        diff = stt;

        //        try
        //        {
        //            DateTime localtimecheck = Convert.ToDateTime(diff);
        //            model.stimerem = diff;
        //            newstrem = stt;
        //        }
        //        catch
        //        {

        //            if (DateTime.TryParse(model.scurrenttime.ToString(), out temp))
        //            {
        //                elapsed = (DateTime.Now - DateTime.Parse(model.scurrenttime)).ToString();
        //            }
        //            else
        //            {
        //                elapsed = "00:00:05";
        //            }


        //            DateTime st = Convert.ToDateTime(model.stimerem);
        //            DateTime en = Convert.ToDateTime(elapsed);

        //            TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
        //            TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

        //            diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

        //            if (diff.IndexOf("-") != -1)
        //            {
        //                diff = "00:00:00";
        //            }

        //            model.stimerem = diff;
        //            newstrem = diff;

        //        }

        //    }
        //    else
        //    {
        //        if (DateTime.TryParse(model.scurrenttime.ToString(), out temp))
        //        {
        //            elapsed = (DateTime.Now - DateTime.Parse(model.scurrenttime)).ToString();
        //        }
        //        else
        //        {
        //            elapsed = "00:00:05";
        //        }


        //        DateTime st = Convert.ToDateTime(model.stimerem);
        //        DateTime en = Convert.ToDateTime(elapsed);

        //        TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
        //        TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

        //        diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

        //        if (diff.IndexOf("-") != -1)
        //        {
        //            diff = "00:00:00";
        //        }


        //        model.stimerem = diff;
        //        newstrem = diff;

        //    }







        //    DateTime stnew = Convert.ToDateTime(oldstrem);
        //    DateTime ennew = Convert.ToDateTime(newstrem);

        //    TimeSpan myTimeSpan1new = new TimeSpan(stnew.Hour, stnew.Minute, stnew.Second);
        //    TimeSpan myTimeSpan2new = new TimeSpan(ennew.Hour, ennew.Minute, ennew.Second);

        //    elapsednew = (myTimeSpan1new.Subtract(myTimeSpan2new)).ToString();




        //    //  timelabel.InnerHtml = " " + model.stimerem.ToString(); --priyanka
        //    string[] timerspan = model.stimerem.ToString().Split(':');

        //    Int32 seconds = (Convert.ToInt32(timerspan[0]) * 60 * 60) + (Convert.ToInt32(timerspan[1]) * 60) + Convert.ToInt32(timerspan[2]);

        //    seconds = seconds * 1000;

        //    //  seconds1 = Convert.ToDouble(seconds); --priyanka  

        //    try
        //    {
        //        //Timer1.Interval = seconds;
        //    }
        //    catch
        //    {

        //    }

        //    string varsquestionids = "";


        //    if (System.IO.File.Exists(model.eInfo.strFilename))
        //    {
        //        //  xmldoc.Load(model.eInfo.strFilename);

        //        XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);
        //        var result = from tutorial in xmlDoc.Descendants("ExamQuestions")
        //                     where tutorial.Attribute("id").Value == model.scurrentquestion.ToString()
        //                     && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
        //                     select tutorial;


        //        //Timing code

        //        if (model.sflowtypetime.ToString() == "1")
        //        {
        //            var numberingtype = from tutorial in xmlDoc.Descendants("Test")
        //                                select tutorial;
        //            foreach (XElement itemElementd in numberingtype)
        //            {
        //                itemElementd.SetElementValue("Time", newstrem);
        //            }
        //        }
        //        else
        //        {
        //            var numberingtype = from tutorial in xmlDoc.Descendants("Section")
        //                                where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
        //                                select tutorial;
        //            foreach (XElement itemElementd in numberingtype)
        //            {
        //                itemElementd.SetElementValue("Time", newstrem);
        //            }
        //        }

        //        //Timing code ends

        //        string compulsorysection = (from tutorial in xmlDoc.Descendants("Section")
        //                                    where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
        //                                    select tutorial.Attribute("questioncompulsory").Value.ToString()).SingleOrDefault().ToString();

        //        string previousbtn = (from tutorial in xmlDoc.Descendants("Section")
        //                              where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
        //                              select tutorial.Attribute("showreview").Value.ToString()).SingleOrDefault().ToString();


        //        string varattemptedques = "";
        //        model.sattemptstatus = "";
        //        string varAttempt = "";
        //        model.senablered = "";

        //        foreach (XElement itemElement in result)
        //        {
        //            string varquesid = itemElement.Element("QuestionId").Value;
        //            string varQtype = itemElement.Element("QType").Value;
        //            string varmarkChecked = Request.Form["mark" + varquesid];
        //            string varCompu = itemElement.Attribute("compulsory").Value;
        //            varAttempt = itemElement.Element("Incomplete").Value;

        //            if (compulsorysection == "1")
        //                varCompu = "1";


        //            if (varmarkChecked == "on")
        //                itemElement.SetElementValue("Marked", "M");
        //            else
        //                itemElement.SetElementValue("Marked", "");


        //            //returnstrques += varquesid + "-" + resultfirst"opt" + varquesid] + "::");
        //            string varsol = "";
        //            if (Request.Form["opt" + varquesid] != null)
        //            {
        //                string[] separator = new string[] { "," };
        //                string[] strSplitArr = Request.Form["opt" + varquesid].Split(separator, StringSplitOptions.RemoveEmptyEntries);

        //                for (int q = 0; q < strSplitArr.Length; q++)
        //                {
        //                    //returnstrques += strSplitArr[q]);

        //                    if (strSplitArr[q] == "a")
        //                        varsol = varsol + itemElement.Element("OptionA").Attribute("value").Value;
        //                    if (strSplitArr[q] == "b")
        //                        varsol = varsol + itemElement.Element("OptionB").Attribute("value").Value;
        //                    if (strSplitArr[q] == "c")
        //                        varsol = varsol + itemElement.Element("OptionC").Attribute("value").Value;
        //                    if (strSplitArr[q] == "d")
        //                        varsol = varsol + itemElement.Element("OptionD").Attribute("value").Value;
        //                    if (strSplitArr[q] == "e")
        //                        varsol = varsol + itemElement.Element("OptionE").Attribute("value").Value;
        //                    if (strSplitArr[q] == "f")
        //                        varsol = varsol + itemElement.Element("OptionF").Attribute("value").Value;
        //                    if (strSplitArr[q] == "g")
        //                        varsol = varsol + itemElement.Element("OptionG").Attribute("value").Value;
        //                    if (strSplitArr[q] == "h")
        //                        varsol = varsol + itemElement.Element("OptionH").Attribute("value").Value;
        //                }

        //                //returnstrques += varsol);
        //                //Response.End();

        //                itemElement.SetElementValue("Attempt", varsol);
        //                itemElement.SetElementValue("OrgAttempt", Request.Form["opt" + varquesid].Replace(",", ""));
        //                itemElement.SetElementValue("Incomplete", "N");

        //                ViewState["questioncompus" + varquesid] = "ok";
        //            }
        //            else
        //            {
        //                if (model.secchange == "true")
        //                {

        //                }
        //                else
        //                {

        //                    itemElement.SetElementValue("Attempt", "");
        //                    itemElement.SetElementValue("OrgAttempt", "");


        //                    if (varCompu == "1")
        //                    {
        //                        varattemptedques = "no";
        //                        // ViewState["questioncompus" + varquesid] = "notok"; --priyanka
        //                        model.sattemptstatus = "no";
        //                        itemElement.SetElementValue("Incomplete", "");

        //                        model.senablered = "no";
        //                    }
        //                    else
        //                    {
        //                        // ViewState["questioncompus" + varquesid] = "ok"; --priyanka
        //                        itemElement.SetElementValue("Incomplete", "I");


        //                    }
        //                }
        //            }

        //            string savetimerem = model.stimerem;
        //            string prevTimeRe = itemElement.Element("TimeRemaining").Value.ToString();
        //            if (model.sRevType == "sec")
        //            {
        //                savetimerem = "S" + savetimerem;

        //            }
        //            else if (model.sRevType == "exam")
        //            {
        //                savetimerem = "E" + savetimerem;
        //            }

        //            newsavetimerem = model.stimerem;



        //            if (prevTimeRe == "")
        //            {
        //                prevTimeRe = savetimerem.ToString();
        //            }
        //            else
        //            {
        //                prevTimeRe = prevTimeRe + "," + savetimerem.ToString();
        //            }


        //            itemElement.SetElementValue("TimeRemaining", prevTimeRe);

        //            string checktime = elapsednew.ToString();
        //            if (checktime.Contains("."))
        //            {

        //                elapsednew = elapsednew.Remove(8);
        //            }
        //            else
        //            {

        //            }

        //            newelapsednew = elapsednew;

        //            string prevTimeTaken = itemElement.Element("TimeTaken").Value.ToString();
        //            if (model.sRevType == "sec")
        //            {
        //                elapsednew = "S" + elapsednew;

        //            }
        //            else if (model.sRevType == "exam")
        //            {
        //                elapsednew = "E" + elapsednew;
        //            }



        //            if (prevTimeTaken == "")
        //            {
        //                prevTimeTaken = elapsednew;
        //            }
        //            else
        //            {
        //                prevTimeTaken = prevTimeTaken + "," + elapsednew;
        //            }



        //            itemElement.SetElementValue("TimeTaken", prevTimeTaken);


        //            //Code for checking compulsory questions

        //        }

        //        foreach (XElement itemElement in result)
        //        {
        //            if (model.sattemptstatus == "no")
        //            {
        //                itemElement.SetElementValue("Incomplete", "");
        //            }
        //        }

        //        questimehist += "##" + newelapsednew + "##" + newsavetimerem;

        //        newquesthist = model.queshist + questimehist + "$$";

        //        if (model.stimerem == "00:00:00")
        //        {
        //            newquesthist += "TE##" + model.scurrentsection + "##" + model.scurrentquestion + "$$";

        //        }

        //        if (model.sattemptstatus == "")
        //        {
        //            if (type == "1")
        //            {
        //                model.scurrentquestion = (Convert.ToInt32(model.scurrentquestion) + 1).ToString();
        //            }
        //            else if (type == "2")
        //            {
        //                model.scurrentquestion = (Convert.ToInt32(model.scurrentquestion) - 1).ToString();
        //            }
        //            else if (type == "3")
        //            {
        //                //       currentquestionshow.Value = (Convert.ToInt32(currentquestionshow.Value.ToString())).ToString();
        //            }

        //        }



        //        if (previousbtn != "2")
        //        {
        //            xmlDoc.Save(model.eInfo.strFilename);
        //        }
        //        if (varAttempt == "" && previousbtn == "2")
        //        {
        //            xmlDoc.Save(model.eInfo.strFilename);
        //        }

        //        else if (varAttempt != "" && previousbtn == "2")
        //        {
        //            //     redirectionurl("Exam.aspx");
        //        }


        //    }

        //    model.scurrenttime = DateTime.Now.ToString();
        //    //}

        //    //catch (Exception ex)
        //    //{
        //    // //   recreate(ex.ToString());
        //    //}

        //    //string filename1 = Session["attemptid"].ToString();
        //    //string mainpath1 = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath + "/");
        //    //string strFilename1 = mainpath1 + filename1 + ".xml";

        //    XmlDocument xmldoc1 = new XmlDocument();
        //    if (System.IO.File.Exists(model.eInfo.strFilename))
        //    {
        //        xmldoc1.Load(model.eInfo.strFilename);
        //    }

        //    XmlNode xmlRoot;

        //    xmlRoot = xmldoc1.SelectSingleNode("//Test//info");



        //    xmlRoot.ChildNodes[4].InnerText = model.scurrenttime;   //currenttime
        //    if (model.snotime == "True")
        //    {
        //        xmlRoot.ChildNodes[9].InnerText = "11:59:59";
        //    }
        //    else
        //    {
        //        xmlRoot.ChildNodes[9].InnerText = model.stimerem;
        //    }

        //    xmlRoot.ChildNodes[11].InnerText = model.sflowtypetime;
        //    xmlRoot.ChildNodes[20].InnerText = model.sattemptstatus;
        //    xmlRoot.ChildNodes[21].InnerText = model.senablered;
        //    xmlRoot.ChildNodes[12].InnerText = model.scurrentquestion;
        //    xmlRoot.ChildNodes[5].InnerText = model.scurrentsection;
        //    xmlRoot.ChildNodes[39].InnerText = newquesthist;
        //    //xmlRoot.ChildNodes[25].InnerText = "";
        //    xmldoc1.Save((model.eInfo.strFilename));

        //    if (model.ssecview == "1")
        //    {
        //        if (model.secchange == "true")
        //        {
        //            // Response.Redirect("Section-intro-new.aspx?secid=" + model.newsecid + ""); -priyanka
        //        }
        //    }
        //}
    }
}