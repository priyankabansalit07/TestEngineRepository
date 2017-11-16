using System;
using System.Linq;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using TestEngine.Utility.Utils;
using TestEngine.Models.Models;


namespace TestEngine.Business.Services
{

    public class SectionService : ISectionService<SectionIntroModel>
    {
        /// <Load XML File>
        /// 
        /// </summary>
        public SectionIntroModel ReadXmlFile(SectionIntroModel model)
        {
          
            XDocument xmlDoc = Common.LoadXmlFile(model.eInfo.strFilename);
            if (xmlDoc != null)
            {
                var resultfirst = (from tutorial in xmlDoc.Descendants("info")
                                   select new
                                   {
                                       customexamid = tutorial.Element("customexamid").Value,
                                       userid = tutorial.Element("userid").Value,
                                       examid = tutorial.Element("examid").Value,
                                       reviewbutton = tutorial.Element("reviewbutton").Value,
                                       currenttime = tutorial.Element("currenttime").Value,
                                       currentsection = tutorial.Element("currentsection").Value,
                                       hdnsectionid = tutorial.Element("hdnsectionid").Value,
                                       hdnvarid = tutorial.Element("hdnvarid").Value,
                                       hdnvarSrNo = tutorial.Element("hdnvarSrNo").Value,
                                       timerem = tutorial.Element("timerem").Value,
                                       flowtype = tutorial.Element("flowtype").Value,
                                       flowtypetime = tutorial.Element("flowtypetime").Value,
                                       currentquestion = tutorial.Element("currentquestion").Value,
                                       totquesection = tutorial.Element("totquessection").Value,
                                       totques = tutorial.Element("totques").Value,
                                       questionno = tutorial.Element("questionno").Value,
                                       numberingcontinue = tutorial.Element("numberingcontinue").Value,
                                       varlan = tutorial.Element("varlan").Value,
                                       reviewsection = tutorial.Element("reviewsection").Value,
                                       next = tutorial.Element("next").Value,
                                       attemptstatus = tutorial.Element("attemptstatus").Value,
                                       enablered = tutorial.Element("enablered").Value,
                                       questionid = tutorial.Element("questionid").Value,
                                       resume = tutorial.Element("resume").Value,
                                       secRev = tutorial.Element("secRev").Value,
                                       RevType = tutorial.Element("RevType").Value,
                                       attemptid = tutorial.Element("enattemptid").Value,
                                       sebattemptid = tutorial.Element("sebattemptid").Value,
                                       secview = tutorial.Element("SecView").Value,
                                       queshist = tutorial.Element("QuestionHist").Value,
                                       username = (string)tutorial.Element("username"),
                                       email = (string)tutorial.Element("email"),
                                       bseb = (string)tutorial.Element("bseb"),
                                       //notime = (string)tutorial.Element("notime"),
                                       corpid = (string)tutorial.Element("userid").Attribute("sitecode")

                                   }).FirstOrDefault();


                model.eInfo.scustomsexamid = resultfirst.customexamid.ToString();
                model.uInfo.suserid = resultfirst.userid.ToString();
                //model.suserid = resultfirst.userid.ToString();
                model.eInfo.sexamid = resultfirst.examid.ToString();
                model.sreviewbutton = resultfirst.reviewbutton.ToString();
                model.scurrenttime = resultfirst.currenttime.ToString();
                model.currentsec = resultfirst.currentsection.ToString();
                model.shdnsectionid = resultfirst.hdnsectionid.ToString();
                model.shdnvarid = resultfirst.hdnvarid.ToString();
                model.shdnvarSrNo = resultfirst.hdnvarSrNo.ToString();
                model.stimerem = resultfirst.timerem.ToString();
                model.sflowtype = resultfirst.flowtype.ToString();
                model.stotquesection = resultfirst.totquesection.ToString();
                model.stotques = resultfirst.totques.ToString();
                model.squestionno = resultfirst.questionno.ToString();
                model.snumberingcontinue = resultfirst.numberingcontinue.ToString();
                model.snext = resultfirst.next.ToString();
                model.sattemptstatus = resultfirst.attemptstatus.ToString();
                model.senablered = resultfirst.enablered.ToString();
                model.sflowtypetime = resultfirst.flowtypetime.ToString();
                model.squestionid = resultfirst.questionid.ToString();
                model.sresume = resultfirst.resume.ToString();
                model.ssecRev = resultfirst.secRev.ToString();
                model.sRevType = resultfirst.RevType.ToString();
                model.svarlan = resultfirst.varlan.ToString();
                model.sreviewsection = resultfirst.reviewsection.ToString();
                model.eInfo.sattemptid = resultfirst.attemptid.ToString();
                model.ssebattemptid = resultfirst.sebattemptid.ToString();
                model.ssecview = resultfirst.secview.ToString();
                model.queshist = resultfirst.queshist.ToString();
                model.sbseb = resultfirst.bseb.ToString();
                model.uInfo.susername = resultfirst.username.ToString();
                // snotime = resultfirst.notime.ToString();
                model.sitecode = resultfirst.corpid.ToString();
                model.scorpid = resultfirst.corpid.ToString(); //set this to  view page
            }
            else
            {
                model = null;
            }
            return model;
        }




        public SectionIntroModel LoadXml(SectionIntroModel model)
        {

            string newqueshist = "";
            string time1 = "";

            XDocument xmlDoc = Common.LoadXmlFile(model.eInfo.strFilename);
            XmlDocument xmldoc = Common.LoadXmlDocument(model.eInfo.strFilename);

            if (xmlDoc != null)
            {
                var numberingtype1 = (from tutorial in xmlDoc.Descendants("Test")
                                      select new
                                      {
                                          varTime = tutorial.Element("Time").Value,
                                          vartimeshow = tutorial.Element("Time").Attribute("show").Value,
                                      }).FirstOrDefault();


                if (numberingtype1.vartimeshow.ToString() == "1")
                {

                    if (model.sRevType == "exam")
                    {
                        if (model.revcurrenttime == "")
                        {
                            if (model.scurrenttime == "")
                            {
                                model.scurrenttime = DateTime.Now.ToString();
                            }

                            model.revcurrenttime = model.scurrenttime;

                        }
                    }
                }

                if (model.sRevType == "sec")
                {
                    if (model.revcurrenttime == "")
                    {
                        model.revcurrenttime = model.scurrenttime;

                    }
                }

                model.checkanswersText = model.lang.lblproceed; //"Click here to proceed";

                Int32 totsections = (from tutorial in xmlDoc.Descendants("Section") select tutorial).Count();


                if (model.sRevType != "exam" && model.sRevType != "sec")
                {
                    model.sreviewbutton = "false";

                }

                if (model.shdnsectionid == "")
                {
                    if (!(model.sreviewbutton != "true"))
                    {
                        model.sreviewbutton = "false";
                    }
                }


                if (totsections < Convert.ToInt32(model.scurrentsection))
                {
                    model.redirecturl = "../Result-test.aspx?newui=true";

                    return model;
                    //return model.redirectURL;
                    // RedirectionURL("../Result-test.aspx?newui=true");
                }

                var result = (from tutorial in xmlDoc.Descendants("Section")
                              where tutorial.Attribute("id").Value == model.scurrentsection
                              select new
                              {
                                  varsectiontitle = tutorial.Element("title").Value,
                                  vardesc = tutorial.Element("section_description").Value,
                                  varshowreview = tutorial.Attribute("showreview").Value,
                                  varshowmarked = tutorial.Attribute("showmark").Value,
                                  varshowtitle = tutorial.Attribute("showtitle").Value,
                                  varSectionTime = tutorial.Element("Time").Value,
                                  vartype = tutorial.Attribute("type").Value,
                                  varshowintro = tutorial.Attribute("showintro").Value,
                                  showcodeassess = (string)tutorial.Element("codeassess")

                              }).FirstOrDefault();

                model.lblTitle = result.varsectiontitle.ToString();
                // lblTitle.Text = result.varsectiontitle.ToString(); --priyanka

                string varredurl = "";


                if (result.vartype.ToString() == "2")
                {
                    varredurl = "../exam-typing.aspx";
                }
                else if (result.vartype.ToString() == "3")
                {

                    XmlNode xmlRoot;

                    xmlRoot = xmldoc.SelectSingleNode("//ExpertRating//Section[@id=" + model.scurrentsection.ToString() + "]");
                    string checkcodesec = xmlRoot.Attributes[9].InnerText.ToString();


                    if (result.varSectionTime.Trim() != "00:00:00")
                    {
                        UpdateCodeAccessHist(model.eInfo.strFilename, newqueshist);

                        if (result.varSectionTime.Trim() != "00:00:00")
                        {
                            model.redirecturl = "coderedirect.aspx?secid=" + model.scurrentsection;
                            return model;
                            // RedirectionURL();
                        }
                        else
                        {
                            model = Processing(model);
                        }
                    }
                    else
                    {
                        model = Processing(model);
                    }
                }
                else if (result.vartype.ToString() == "5")
                {
                    varredurl = "exam-adaptive.aspx";
                }
                else if (result.vartype.ToString() == "6")
                {
                    varredurl = "essayenginetest.aspx";
                }
                else if (result.vartype.ToString() == "7")
                {
                    varredurl = "exam-timer.aspx";
                    // Session["timersection"] = "1"; --priyanka
                }
                else if (result.vartype.ToString() == "8")
                {
                    varredurl = "Exam-Timer-Adaptive.aspx";

                }
                else if (result.vartype.ToString() == "9")
                {
                    varredurl = "Exam-File.aspx";

                }
                else if (result.vartype.ToString() == "18")
                {
                    varredurl = "configmic.aspx";
                }
                else if (result.vartype.ToString() == "40")
                {
                    varredurl = "surveyone.aspx";
                }
                else if (result.vartype.ToString() == "41")
                {
                    varredurl = "surveytwo.aspx";
                }
                else if (result.vartype.ToString() == "42")
                {
                    varredurl = "surveymcq.aspx";
                }
                else
                {
                    varredurl = "ExamPage";
                    //  varredurl = "/testengine/lmw.aspx"; --priyanka
                }
                model.redirecturl = varredurl;
                // Session["redurl"] = varredurl;
                //   ViewState["redurl"] = varredurl; --priyanka

                var numberingtype = (from tutorial in xmlDoc.Descendants("Test")
                                     select new
                                     {
                                         val = tutorial.Element("Numbering").Value,
                                         varTime = tutorial.Element("Time").Value,
                                         varReview = tutorial.Element("Review").Attribute("individual").Value,
                                         varShow = tutorial.Element("Review").Attribute("show").Value,
                                         vartimeShow = tutorial.Element("Time").Attribute("show").Value,
                                         inforevtype = tutorial.Element("info").Element("RevType").Value,
                                         infoexamid = tutorial.Element("info").Element("examid").Value,
                                     }).FirstOrDefault();


                if (numberingtype.vartimeShow == "1")
                {
                    if (model.sRevType == "exam" || model.sRevType == "sec")
                    {

                        if (model.revstimerem == "00:00:00")
                        {

                            model.stimerem = "00:00:00";
                            model.sreviewbutton = "false";
                        }
                    }
                    else
                    {
                        model.stimerem = numberingtype.varTime;
                    }

                    model.Time = model.stimerem + " (Overall)";
                    //   lblTime.Text = stimerem + " (Overall)";  --priyanka
                    model.sflowtypetime = "1";
                }
                else
                {

                    model.sflowtypetime = "2";
                    if (model.sRevType == "sec")
                    {

                        if (model.revstimerem == "00:00:00")
                        {
                            model.stimerem = "00:00:00";
                            model.sRevType = "";
                            model.sreviewbutton = "false";
                        }
                    }
                    else
                    {
                        model.stimerem = result.varSectionTime;
                    }
                    model.Time = model.stimerem;
                    // lblTime.Text = stimerem;     --priyanka
                }

                if (numberingtype.varReview == "1")
                {

                    model.sflowtype = "2";
                }
                else
                {
                    model.sflowtype = "1";
                }
                if (result.vartype.ToString() == "7" || result.vartype.ToString() == "8")
                {
                    model.stimerem = "1";
                }

                if (result.vartype.ToString() != "8" || result.vartype.ToString() != "7")
                {


                    if (model.stimerem == "00:00:00")
                    {

                        if (numberingtype.varShow == "1")
                        {
                            model = UpdateQuesHist(model);

                            if (numberingtype.varReview == "0")
                            {
                                model.redirecturl = "../Result-test.aspx?newui=true";
                                //RedirectionURL("../Result-test.aspx?newui=true");
                            }
                            else
                            {

                                if (Convert.ToInt32(model.scurrentsection) < totsections)
                                {
                                    CompleteSection(model.eInfo.strFilename, model.scurrentsection);
                                    model.scurrentsection = (Convert.ToInt32(model.scurrentsection) + 1).ToString();

                                    UpdateSec(model.eInfo.strFilename, model.scurrentsection, model.snotime, model.stimerem, model.sRevType);
                                    model.redirecturl = "Index";
                                    return model;
                                    //RedirectionURL("section-intro-new.aspx");
                                }
                                else
                                {

                                    if (numberingtype.varReview == "1")
                                    {
                                        model.redirecturl = "../Result-test.aspx?newui=true";
                                        return model;
                                        //RedirectionURL("../Result-test.aspx?newui=true");
                                    }
                                    else
                                    {
                                        UpdateRev("2", model.eInfo.strFilename);
                                        model.redirecturl = "Review.aspx";
                                        return model;
                                        //RedirectionURL("Review.aspx");
                                    }
                                }
                            }
                        }
                        else if (numberingtype.varShow == "0")
                        {
                            model.redirecturl = "../Result-test.aspx?newui=true";
                            return model;
                            //RedirectionURL("../Result-test.aspx?newui=true");
                        }
                        else
                        {

                            if (Convert.ToInt32(model.scurrentsection) < totsections)
                            {
                                model.scurrentsection = (Convert.ToInt32(model.scurrentsection) + 1).ToString();
                                UpdateSec(model.eInfo.strFilename, model.scurrentsection, model.snotime, model.stimerem, model.sRevType);
                                model.redirecturl = "Index";
                                return model;
                                //RedirectionURL("section-intro-new.aspx");
                            }
                            else
                            {
                                UpdateRev("2", model.eInfo.strFilename);
                                model.redirecturl = "Review.aspx";
                                return model;
                                //RedirectionURL("Review.aspx");
                            }
                        }
                    }

                }

                Int32 totpages = (from tutorial in xmlDoc.Descendants("ExamQuestions")
                                  where tutorial.Attribute("section_id").Value == model.scurrentsection
                                  select new
                                  {
                                      varid = tutorial.Attribute("id").Value,
                                  }).Max(s => Convert.ToInt32(s.varid));

                if (numberingtype.val == "1")
                {
                    Int32 varquestions = (from tutorial in xmlDoc.Descendants("ExamQuestions") select tutorial).Count();

                    model.stotquesection = totpages.ToString();
                    model.stotques = varquestions.ToString();
                    model.squestionno = "1";
                    model.snumberingcontinue = "cont";
                }
                else
                {
                    Int32 varquestions = (from tutorial in xmlDoc.Descendants("ExamQuestions") where tutorial.Attribute("section_id").Value == model.scurrentsection select tutorial).Count();

                    model.stotquesection = totpages.ToString();
                    model.stotques = varquestions.ToString();
                    model.squestionno = "1";
                    model.snumberingcontinue = "nocont";
                }

                //Get master file Information
                if (result.vartype.ToString() == "5" || result.vartype.ToString() == "8")
                {
                    string filenameMaster1 = numberingtype.infoexamid;
                   // string mainpathMaster1 = System.Web.HttpContext.Current.Server.MapPath("~").Replace(model.eInfo.wwwroot, "xmlfiles/" + model.mastersetmain + "/" + model.mastersetin);
                   string strFilenameMaster1= "E:\\Sukhvir TestEngine\\Files\\XmlFiles\\MasterFiles" + "/" + filenameMaster1 + ".xml";
                    //string strFilenameMaster1 = mainpathMaster1 + "/" + filenameMaster1 + ".xml";

                    XmlDocument xmldoc11 = Common.LoadXmlDocument(strFilenameMaster1);


                    if (result.vartype.ToString() == "8")
                    {
                        model.stimerem = "1";
                    }

                    XmlNode xmlRoot;

                    xmlRoot = xmldoc11.SelectSingleNode("//test//sections//section[@id=" + model.scurrentsection + "]");

                    model.stotques = xmlRoot.ChildNodes[3].Attributes["questioncount"].Value;

                    model.Question = model.stotques;

                    //  ViewBag.Ques = varques;

                    //lblQues.Text = varques;  //Priyanka

                }
                else
                {
                    model.Question = (from tutorial in xmlDoc.Descendants("ExamQuestions") where tutorial.Attribute("section_id").Value == model.scurrentsection select tutorial).Count().ToString();
                    //  lblQues.Text = (from tutorial in xmlDoc.Descendants("ExamQuestions") where tutorial.Attribute("section_id").Value == scurrentsection select tutorial).Count().ToString(); -- priyanka
                }


                if (result.vardesc.ToString() != "")
                {
                    model.Div1text = result.vardesc.ToString().Replace("QUESTIONER", model.Question).Replace("TIMERER", model.Time);
                    // Div1.InnerHtml = result.vardesc.ToString().Replace("QUESTIONER", lblQues.Text).Replace("TIMERER", lblTime.Text); --priyanka

                }

                if (model.sRevType == "sec")
                {
                    model.sreviewbutton = "true2";

                }
                if (model.sreviewbutton == "true" || model.sreviewbutton == "true2")
                {

                    model.scurrentsection = model.shdnsectionid;
                    model.scurrentquestion = model.shdnvarid;

                }

                if (model.scurrentsection == "")
                {

                    model.scurrentsection = model.currentsec;
                    model.scurrentquestion = "1";
                }

                if (model.sRevType == "sec" || numberingtype.vartimeShow == "1")
                {

                    if (model.sRevType == "sec")
                    {

                        if (model.revcurrenttime != "")
                        {

                            newqueshist = "SecRev##" + model.scurrentsection + "##" + model.stimerem;

                            DateTime temp;
                            DateTime temp1;

                            string oldstrem = model.stimerem, newstrem = "", elapsed = "", elapsednew = "";
                            string stt = "", diff = "";
                            if (model.revstimerem != "")
                            {
                                stt = model.revstimerem;
                                diff = stt;

                                try
                                {
                                    DateTime localtimecheck = Convert.ToDateTime(diff);
                                    model.stimerem = diff;
                                    newstrem = stt;
                                }
                                catch
                                {

                                    if (DateTime.TryParse(model.scurrenttime.ToString(), out temp))
                                    {
                                        elapsed = (DateTime.Now - DateTime.Parse(model.scurrenttime)).ToString();
                                    }
                                    else
                                    {
                                        elapsed = "00:00:05";
                                    }


                                    DateTime st = Convert.ToDateTime(model.stimerem);
                                    DateTime en = Convert.ToDateTime(elapsed);

                                    TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
                                    TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

                                    diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

                                    if (diff.IndexOf("-") != -1)
                                    {
                                        diff = "00:00:00";
                                    }


                                    model.stimerem = diff;
                                    newstrem = diff;

                                }

                            }
                            else
                            {
                                if (DateTime.TryParse(model.revcurrenttime.ToString(), out temp))
                                {
                                    elapsed = (DateTime.Now - DateTime.Parse(model.revcurrenttime)).ToString();
                                }
                                else
                                {
                                    elapsed = "00:00:05";
                                }


                                DateTime st = Convert.ToDateTime(model.stimerem);
                                DateTime en = Convert.ToDateTime(elapsed);

                                TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
                                TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

                                diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

                                if (diff.IndexOf("-") != -1)
                                {
                                    diff = "00:00:00";
                                }


                                model.stimerem = diff;
                                newstrem = diff;

                            }

                            DateTime stnew = Convert.ToDateTime(oldstrem);
                            DateTime ennew = Convert.ToDateTime(newstrem);

                            TimeSpan myTimeSpan1new = new TimeSpan(stnew.Hour, stnew.Minute, stnew.Second);
                            TimeSpan myTimeSpan2new = new TimeSpan(ennew.Hour, ennew.Minute, ennew.Second);

                            elapsednew = (myTimeSpan1new.Subtract(myTimeSpan2new)).ToString();

                            string checktime = elapsednew.ToString();
                            if (checktime.Contains("."))
                            {
                                elapsednew = elapsednew.Remove(8);
                            }

                            model.stimerem = diff;
                            newqueshist += "##" + elapsednew + "##" + model.stimerem + "$$";

                            if (model.sflowtypetime.ToString() == "1")
                            {
                                var numberingtype3 = from tutorial in xmlDoc.Descendants("Test")
                                                     select tutorial;
                                foreach (XElement itemElementd in numberingtype3)
                                {
                                    itemElementd.SetElementValue("Time", model.stimerem);
                                }
                            }
                            else
                            {
                                var numberingtype3 = from tutorial in xmlDoc.Descendants("Section")
                                                     where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
                                                     select tutorial;
                                foreach (XElement itemElementd in numberingtype3)
                                {
                                    itemElementd.SetElementValue("Time", model.stimerem);
                                }
                            }
                        }
                    }
                }

                if (model.sRevType == "exam")
                {
                    if (numberingtype.vartimeShow == "1")
                    {

                        newqueshist = "ExamRev##" + model.stimerem;

                        if (model.revcurrenttime != "")
                        {

                            DateTime temp;
                            DateTime temp1;

                            string oldstrem = model.stimerem, newstrem = "", elapsed = "", elapsednew = "";
                            string stt = "", diff = "";
                            if (model.revstimerem != "")
                            {
                                stt = model.revstimerem;
                                diff = stt;

                                try
                                {
                                    DateTime localtimecheck = Convert.ToDateTime(diff);
                                    model.stimerem = diff;
                                    newstrem = stt;
                                }
                                catch
                                {

                                    if (DateTime.TryParse(model.scurrenttime.ToString(), out temp))
                                    {
                                        elapsed = (DateTime.Now - DateTime.Parse(model.scurrenttime)).ToString();
                                    }
                                    else
                                    {
                                        elapsed = "00:00:05";
                                    }


                                    DateTime st = Convert.ToDateTime(model.stimerem);
                                    DateTime en = Convert.ToDateTime(elapsed);

                                    TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
                                    TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

                                    diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

                                    if (diff.IndexOf("-") != -1)
                                    {
                                        diff = "00:00:00";
                                    }


                                    model.stimerem = diff;
                                    newstrem = diff;

                                }

                            }
                            else
                            {
                                if (DateTime.TryParse(model.revcurrenttime.ToString(), out temp))
                                {
                                    elapsed = (DateTime.Now - DateTime.Parse(model.revcurrenttime)).ToString();
                                }
                                else
                                {
                                    elapsed = "00:00:05";
                                }


                                DateTime st = Convert.ToDateTime(model.stimerem);
                                DateTime en = Convert.ToDateTime(elapsed);

                                TimeSpan myTimeSpan1 = new TimeSpan(st.Hour, st.Minute, st.Second);
                                TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

                                diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

                                if (diff.IndexOf("-") != -1)
                                {
                                    diff = "00:00:00";
                                }

                                model.stimerem = diff;
                                newstrem = diff;

                            }

                            DateTime stnew = Convert.ToDateTime(oldstrem);
                            DateTime ennew = Convert.ToDateTime(newstrem);

                            TimeSpan myTimeSpan1new = new TimeSpan(stnew.Hour, stnew.Minute, stnew.Second);
                            TimeSpan myTimeSpan2new = new TimeSpan(ennew.Hour, ennew.Minute, ennew.Second);

                            elapsednew = (myTimeSpan1new.Subtract(myTimeSpan2new)).ToString();

                            string checktime = elapsednew.ToString();
                            if (checktime.Contains("."))
                            {

                                elapsednew = elapsednew.Remove(8);
                            }

                            model.stimerem = diff;
                            newqueshist += "##" + elapsednew + "##" + newstrem + "$$";
                        }
                    }
                    else
                    {
                        newqueshist = "SExamRev$$";
                    }


                    if (model.sflowtypetime.ToString() == "1")
                    {
                        var numberingtype3 = from tutorial in xmlDoc.Descendants("Test")
                                             select tutorial;
                        foreach (XElement itemElementd in numberingtype3)
                        {
                            itemElementd.SetElementValue("Time", model.stimerem);
                        }
                    }
                    else
                    {
                        var numberingtype3 = from tutorial in xmlDoc.Descendants("Section")
                                             where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
                                             select tutorial;
                        foreach (XElement itemElementd in numberingtype3)
                        {
                            itemElementd.SetElementValue("Time", model.stimerem);
                        }

                    }

                }

                model.queshist = model.queshist + newqueshist;


                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");

                xmlRootInfo.ChildNodes[4].InnerText = "";
                xmlRootInfo.ChildNodes[5].InnerText = model.scurrentsection;

                if (model.snotime == "True")
                {
                    xmlRootInfo.ChildNodes[9].InnerText = "11:59:59";
                }
                else
                {
                    xmlRootInfo.ChildNodes[9].InnerText = model.stimerem;
                }
                xmlRootInfo.ChildNodes[10].InnerText = model.sflowtype;
                xmlRootInfo.ChildNodes[11].InnerText = model.sflowtypetime;
                xmlRootInfo.ChildNodes[12].InnerText = model.scurrentquestion;
                xmlRootInfo.ChildNodes[13].InnerText = model.stotquesection;
                xmlRootInfo.ChildNodes[14].InnerText = model.stotques;
                xmlRootInfo.ChildNodes[15].InnerText = model.squestionno;
                xmlRootInfo.ChildNodes[16].InnerText = model.snumberingcontinue;
                xmlRootInfo.ChildNodes[39].InnerText = model.queshist;

                if (model.ssrev == "true")
                {
                    xmlRootInfo.ChildNodes[24].InnerText = "on";
                }
                else
                {
                    xmlRootInfo.ChildNodes[24].InnerText = "";
                }


                var SectionPalette = (from sec in xmlDoc.Descendants("Section")
                                      where sec.Attribute("id").Value == xmlRootInfo.ChildNodes[5].InnerText.ToString()
                                      select new
                                      {
                                          varnotvisited = sec.Element("SectionPalette").Attribute("notvisited").Value,
                                          varsectotques = sec.Element("SectionPalette").Attribute("sectotques").Value,
                                          varsecvisited = sec.Element("SectionPalette").Attribute("secvisited").Value,
                                      }).SingleOrDefault();

                xmldoc.Save((model.eInfo.strFilename));

                //XDocument xmlDoc2 = XDocument.Load(model.eInfo.strFilename);
                var numberingtype2 = from tutorial in xmlDoc.Descendants("Section")
                                     where tutorial.Attribute("id").Value == xmlRootInfo.ChildNodes[5].InnerText.ToString()
                                     select tutorial;

                foreach (XElement itemElementd in numberingtype2)
                {
                    itemElementd.SetElementValue("Time", model.stimerem);
                }
                xmlDoc.Save(model.eInfo.strFilename);

                if (model.shdnsectionid != "")
                {
                    model.redirecturl = varredurl;
                    return model;
                    // RedirectionURL(varredurl);
                    // Response.Redirect(varredurl);

                }


                if (result.varshowintro == "0" || model.ssecview == "1")
                {
                    XmlNode xmlRootSec;
                    xmlRootSec = xmldoc.SelectSingleNode("//Section[@id=" + xmlRootInfo.ChildNodes[5].InnerText.ToString() + "]//SectionPalette");
                    xmlRootSec.Attributes[6].InnerText = "visited";
                    xmldoc.Save((model.eInfo.strFilename));
                    model.redirecturl = varredurl;
                    return model;
                    //RedirectionURL(varredurl);

                }
                else
                {
                    if (SectionPalette.varsecvisited.ToString() == "visited")
                    {
                        model.redirecturl = varredurl;
                        return model;
                        //RedirectionURL(varredurl);
                    }
                    else
                    {

                        XmlNode xmlRootSec;
                        xmlRootSec = xmldoc.SelectSingleNode("//Section[@id=" + xmlRootInfo.ChildNodes[5].InnerText.ToString() + "]//SectionPalette");
                        xmlRootSec.Attributes[6].InnerText = "visited";
                        xmldoc.Save((model.eInfo.strFilename));
                    }
                }

            }
            else
            {
                model = null;
                return model;
            }

            return model;
        }

        public SectionIntroModel Processing(SectionIntroModel model)
        {
            XDocument xmlDoc = Common.LoadXmlFile(model.eInfo.strFilename);
            if (xmlDoc != null)
            {
                var settings = (from tutorial in xmlDoc.Descendants("Review")
                                select new
                                {
                                    varindividual = tutorial.Attribute("individual").Value,
                                    varshow = tutorial.Attribute("show").Value,
                                }).FirstOrDefault();

                model.redirecturl = "../Result-test.aspx?newui=true";


                Int32 totsections = (from tutorial in xmlDoc.Descendants("Section") select tutorial).Count();

                if (totsections <= Convert.ToInt32(model.scurrentsection))
                {
                    if (settings.varindividual == "0" && settings.varshow == "1")
                    {
                        UpdateRev("2", model.eInfo.strFilename);
                        model.sreviewsection = "false";
                        model.redirecturl = "Review.aspx";
                        return model;
                        //RedirectionURL("Review.aspx");
                    }
                    else if (settings.varindividual == "0" && settings.varshow == "0")
                    {
                        return model;
                        //RedirectionURL(redriecturl);
                    }
                    else if (settings.varindividual == "0" && settings.varshow == "2")
                    {
                        return model;
                        //RedirectionURL(redriecturl);
                    }

                }

                if (settings.varindividual.ToString() == "1")
                {
                    var result = (from tutorial in xmlDoc.Descendants("Section")
                                  where tutorial.Attribute("id").Value == model.scurrentsection.ToString()
                                  select new
                                  {
                                      varsectiontitle = tutorial.Element("title").Value,
                                      vardesc = tutorial.Element("section_description").Value,
                                      varshowreview = tutorial.Attribute("showreview").Value,
                                  }).FirstOrDefault();


                    if (result.varshowreview.ToString() != "1" && totsections <= Convert.ToInt32(model.scurrentsection.ToString()))
                    {

                        return model;
                        //RedirectionURL(redriecturl);
                    }

                    if (result.varshowreview.ToString() != "1" && totsections > Convert.ToInt32(model.scurrentsection.ToString()))
                    {


                        CompleteSection(model.eInfo.strFilename, model.scurrentsection);

                        //UpdateSec();
                        model.redirecturl = "section-intro-new.aspx";
                        return model;
                        //RedirectionURL("section-intro-new.aspx");
                    }
                    else
                    {
                        UpdateRev("1", model.eInfo.strFilename);
                        model.sreviewsection = "true";
                        model.redirecturl = "Review-Section.aspx";
                        return model;
                        //RedirectionURL("");
                    }
                }
                else
                {
                    CompleteSection(model.eInfo.strFilename, model.scurrentsection);

                    if (model.ssecview == "1")
                    {
                        int sec = Convert.ToInt32(model.scurrentsection) + 1;
                        model.redirecturl = "Index"; //?secid=" + sec + "";
                        model.secid = Convert.ToString(sec);
                        return model;

                        //RedirectionURL("section-intro-new.aspx?secid=" + sec + "");
                    }
                    else
                    {
                        model.redirecturl = "Index";
                        return model;
                        //   RedirectionURL("section-intro-new.aspx");
                    }
                }
            }
            else
            {
                model = null;
            }

            return model;
        }

        #region "Update Section and other details in Info tag"
        public void UpdateSec(string strFilename, string scurrentsection, string snotime, string stimerem, string sRevType)
        {

            XmlDocument xmldoc1 = Common.LoadXmlDocument(strFilename);
            if (xmldoc1 != null)
            {
                XmlNode xmlRoot;

                xmlRoot = xmldoc1.SelectSingleNode("//Test//info");

                xmlRoot.ChildNodes[3].InnerText = "false";
                xmlRoot.ChildNodes[5].InnerText = scurrentsection;
                if (snotime == "True")
                {
                    xmlRoot.ChildNodes[9].InnerText = "11:59:59";
                }
                else
                {
                    xmlRoot.ChildNodes[9].InnerText = stimerem;
                }
                xmlRoot.ChildNodes[13].InnerText = "1";
                xmlRoot.ChildNodes[25].InnerText = sRevType;
                // xmldoc1.Save((strFilename1)); --priyanka 
                xmldoc1.Save(strFilename);
            }

        }
        #endregion

        #region "Update Review Button"

        public void UpdateRev(string type, string strFilename)
        {

            //string strFilename = "E:\\XMLFile\\" + attemptid + ".xml";

            XmlDocument xmldoc = Common.LoadXmlDocument(strFilename);
            if (xmldoc != null)
            {
                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");

                if (type == "1")
                {
                    xmlRootInfo.ChildNodes[3].InnerText = "true2";
                }
                else if (type == "2")
                {

                    xmlRootInfo.ChildNodes[3].InnerText = "true";
                }

                xmldoc.Save((strFilename));
            }
        }
        #endregion





        //private void funStatus(string attemptid)
        //{
        //    SqlConnection con;
        //    string conn = ConfigurationManager.ConnectionStrings["conn"].ToString();
        //    con = new SqlConnection(conn);

        //    if (con.State == ConnectionState.Closed)
        //        con.Open();

        //   // string attemptid = Session["attemptid"].ToString();

        //    SqlCommand cmd = new SqlCommand(" select status from naukuserproducts where attemptid=" + attemptid, con);
        //    string status = cmd.ExecuteScalar().ToString();
        //    if (con.State == ConnectionState.Open)
        //        con.Close();
        //    if (status == "Aborted")
        //    {

        //      //  RedirectToAction("AbortPage");
        //      //  Response.Redirect("abortpage.aspx");
        //    }

        //}

        #region "update Code Access history"
        public void UpdateCodeAccessHist(string strFilename, string newqueshist)
        {
            XmlDocument xmldoc = Common.LoadXmlDocument(strFilename);

            if (xmldoc != null)
            {
                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");

                string queshist = xmlRootInfo.ChildNodes[39].InnerText.ToString();

                queshist += newqueshist + "CDEAS$$";
                xmlRootInfo.ChildNodes[39].InnerText = queshist;

                xmldoc.Save((strFilename));
            }
        }
        #endregion

        #region "Compelete Section" 
        /// <summary>
        /// Change the inner text of complete attribute of current section to Complete. 
        /// </summary>

        public void CompleteSection(string strFilename, string scurrentsection)
        {
            XmlDocument xmldoc = Common.LoadXmlDocument(strFilename);
            if (xmldoc != null)
            {
                XmlNode xmlRoot;
                xmlRoot = xmldoc.SelectSingleNode("//ExpertRating//Section[@id=" + scurrentsection.ToString() + "]");
                xmlRoot.Attributes[9].InnerText = "completed";

                xmldoc.Save((strFilename));
            }
        }
        #endregion


        public SectionIntroModel LoadSectionQuestion(SectionIntroModel model)
        {
            model.scurrentsection = model.secid;
            model.sRevType = "";
            string filename = model.eInfo.attemptid;
            string strFilename = "E:\\XMLFile\\" + model.eInfo.attemptid + ".xml";
            //string mainpath = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/");
            //string strFilename = mainpath + filename + ".xml";

            XmlDocument xmldoc = Common.LoadXmlDocument(model.eInfo.strFilename);

            if (xmldoc != null)
            {
                XDocument xmlDoc = Common.LoadXmlFile(model.eInfo.strFilename);


                XmlNode xmlRoot;
                xmldoc.Load(strFilename);
                xmlRoot = xmldoc.SelectSingleNode("//ExpertRating//Section[@id=" + model.secid + "]");
                string checkcodesec = xmlRoot.Attributes[9].InnerText.ToString();
                try
                {

                    bool iscode = false;


                    var sectiontype = (from tutorial in xmlDoc.Descendants("Section")
                                       where tutorial.Attribute("id").Value == model.scurrentsection
                                       select new
                                       {
                                           sectype = tutorial.Attribute("type").Value,
                                           sectotalques = (string)tutorial.Attribute("totalquescount"),
                                           sectime = tutorial.Element("Time").Value

                                       }).FirstOrDefault();

                    if (sectiontype.sectype == "3")
                    {

                        var sectioncompnext = (from tutorial in xmlDoc.Descendants("Section")
                                               where tutorial.Attribute("id").Value == model.scurrentsection && tutorial.Element("Time").Value != "00:00:00"
                                               select new
                                               {
                                                   secid = tutorial.Attribute("id").Value,

                                               }).FirstOrDefault();

                        if (sectioncompnext.secid != null)
                        {
                            iscode = true;

                        }

                    }

                    if (iscode == false)
                    {


                        XDocument xmlDocNew = Common.LoadXmlFile(strFilename); //XDocument.Load(strFilename);
                        if (xmlDocNew != null)
                        {
                            var resultfirst = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                               where tutorial.Element("Incomplete").Value == ""
                                               && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                               orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                               select tutorial.Attribute("id").Value).FirstOrDefault();

                            Int32 totquesatt;

                            Int32 totpages = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                              select new
                                              {
                                                  varid = tutorial.Attribute("id").Value,
                                              }).Max(s => Convert.ToInt32(s.varid));



                            if (model.snumberingcontinue == "cont")
                            {
                                totquesatt = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Element("Incomplete").Value != ""
                                              select tutorial.Attribute("id").Value).Count();
                            }
                            else
                            {
                                totquesatt = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Element("Incomplete").Value != ""
                                              && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                              select tutorial.Attribute("id").Value).Count();
                            }

                            string varresques = "";

                            if (resultfirst == null)
                            {
                                var resultsecond = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                                    where tutorial.Element("Incomplete").Value == "N"
                                                   && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                                    orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                                    select tutorial.Attribute("id").Value).Max();

                                try
                                {
                                    varresques = resultsecond.ToString();
                                }
                                catch
                                {
                                }
                                model.scurrentquestion = varresques;
                            }
                            else
                            {
                                varresques = resultfirst.ToString();

                                model.scurrentquestion = varresques;
                            }

                            var resultreview = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                                where tutorial.Element("Incomplete").Value == ""
                                               && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                                orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                                select tutorial.Attribute("id").Value).Max();

                            if (resultreview == null)
                            {
                                //var resultreview1 = (from tutorial in xmlDoc.Descendants("ExamQuestions")
                                //                     where tutorial.Element("Incomplete").Value != ""
                                //                    && tutorial.Attribute("section_id").Value == scurrentsection.ToString()
                                //                     orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                //                     select Convert.ToInt32(tutorial.Attribute("id").Value)).Max();

                                //scurrentquestion = resultreview1.ToString();

                                model.scurrentquestion = "1";

                            }


                            // xmldoc.Load(strFilename);

                            XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");
                            string nqueshist = xmlRootInfo.ChildNodes[39].InnerText.ToString();

                            XmlNode xmlRoot1;
                            xmlRoot1 = xmldoc.SelectSingleNode("//ExpertRating//Section[@id=" + model.scurrentsection.ToString() + "]");
                            string sectimerem = xmlRoot1.ChildNodes[2].InnerText;

                            if (nqueshist.Contains("MSEC##" + model.scurrentsection + "##"))
                            {
                                nqueshist += "MSECREV##" + model.scurrentsection + "##" + sectimerem + "$$";
                                xmlRootInfo.ChildNodes[39].InnerText = nqueshist;
                                xmldoc.Save((strFilename));

                                model.queshist = nqueshist;
                            }
                            if (nqueshist.Contains("ESEC##" + model.scurrentsection + "##"))
                            {
                                nqueshist += "ESECREV##" + model.scurrentsection + "##" + sectimerem + "$$";
                                xmlRootInfo.ChildNodes[39].InnerText = nqueshist;
                                xmldoc.Save((strFilename));

                                model.queshist = nqueshist;
                            }
                        }
                    }
                }
                catch
                {
                    LoadMainQuestion(model);
                }
            }
            return model;
        }

        public SectionIntroModel LoadMainQuestion(SectionIntroModel model)
        {
            string newsec = "";
            if (model.sreviewbutton != "")
            {
                if (model.sreviewbutton == "false")
                {
                    //  XmlDocument xmldoc = new XmlDocument();

                    if (System.IO.File.Exists(model.eInfo.strFilename))
                    {
                        XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);
                        //  xmldoc.Load(model.eInfo.strFilename);

                        var settings = (from tutorial in xmlDoc.Descendants("Review")
                                        select new
                                        {
                                            varindividual = tutorial.Attribute("individual").Value,
                                            varshow = tutorial.Attribute("show").Value,
                                        }).FirstOrDefault();


                        Int32 totsections = (from tutorial in xmlDoc.Descendants("Section") select tutorial).Count();


                        if (settings.varindividual.ToString() == "0" && settings.varshow.ToString() == "1")
                        {

                            var sectioncomp = (from tutorial in xmlDoc.Descendants("Section")
                                               where tutorial.Element("Time").Value != "00:00:00" & tutorial.Attribute("completed").Value != "completed"
                                               select new
                                               {
                                                   secid = tutorial.Attribute("id").Value,

                                               }).FirstOrDefault();


                            if (sectioncomp == null)
                            {
                                var sectioncomp1 = (from tutorial in xmlDoc.Descendants("Section")
                                                    where tutorial.Element("Time").Value != "00:00:00"
                                                    select new
                                                    {
                                                        secid = tutorial.Attribute("id").Value,

                                                    }).FirstOrDefault();

                                if (sectioncomp1 != null)
                                {

                                    newsec = sectioncomp1.secid.ToString();
                                }
                            }
                            else
                            {
                                newsec = sectioncomp.secid.ToString();

                            }
                        }

                        else if (settings.varindividual.ToString() == "1" && settings.varshow.ToString() == "1")
                        {

                            var sectioncomp = (from tutorial in xmlDoc.Descendants("Section")
                                               where tutorial.Attribute("completed").Value == "not"
                                               select new
                                               {
                                                   secid = tutorial.Attribute("id").Value,

                                               }).FirstOrDefault();


                            if (sectioncomp == null)
                            {

                                var sectioncomp1 = (from tutorial in xmlDoc.Descendants("Section")
                                                    where tutorial.Attribute("showreview").Value == "1" && tutorial.Element("Time").Value != "00:00:00" && tutorial.Element("Time").Value != ""
                                                    select new
                                                    {
                                                        secid = tutorial.Attribute("id").Value,
                                                        sectype = tutorial.Attribute("type").Value,

                                                    }).FirstOrDefault();
                                if (sectioncomp1 != null)
                                {

                                    newsec = sectioncomp1.secid.ToString();

                                }


                            }
                            else
                            {
                                var checkcodeass = (from tutorial in xmlDoc.Descendants("Section")
                                                    where tutorial.Attribute("id").Value == sectioncomp.secid.ToString()
                                                    select new
                                                    {
                                                        secid = tutorial.Attribute("id").Value,
                                                        sectype = tutorial.Attribute("type").Value,
                                                        seccomp = tutorial.Attribute("completed").Value,
                                                        codetime = tutorial.Element("Time").Value,

                                                    }).FirstOrDefault();

                                if (checkcodeass.sectype == "3" && checkcodeass.codetime == "00:00:00")
                                {

                                    var sectioncomp1 = (from tutorial in xmlDoc.Descendants("Section")
                                                        where tutorial.Attribute("showreview").Value == "1" && tutorial.Element("Time").Value != "00:00:00" && tutorial.Element("Time").Value != ""
                                                        select new
                                                        {
                                                            secid = tutorial.Attribute("id").Value,
                                                            sectype = tutorial.Attribute("type").Value,

                                                        }).FirstOrDefault();
                                    if (sectioncomp1 != null)
                                    {

                                        newsec = sectioncomp1.secid.ToString();
                                    }

                                }
                                else
                                {
                                    newsec = sectioncomp.secid.ToString();
                                }

                            }

                        }

                        else
                        {
                            var sectioncomp = (from tutorial in xmlDoc.Descendants("Section")
                                               where tutorial.Attribute("completed").Value != "completed"
                                               select new
                                               {
                                                   secid = tutorial.Attribute("id").Value,

                                               }).FirstOrDefault();

                            if (sectioncomp == null)
                            {

                            }
                            else
                            {

                                newsec = sectioncomp.secid.ToString();
                            }
                        }

                        if (newsec == "")
                        {
                            newsec = totsections.ToString();
                        }


                        model.scurrentsection = newsec;

                        var sectiontype = (from tutorial in xmlDoc.Descendants("Section")
                                           where tutorial.Attribute("id").Value == model.scurrentsection
                                           select new
                                           {
                                               sectype = tutorial.Attribute("type").Value,
                                               sectotalques = (string)tutorial.Attribute("totalquescount")

                                           }).FirstOrDefault();

                        if (sectiontype.sectype.ToString() != "3")
                        {

                            XDocument xmlDocNew = XDocument.Load(model.eInfo.strFilename);


                            var resultfirst = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                               where tutorial.Element("Incomplete").Value == ""
                                               && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                               orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                               select tutorial.Attribute("id").Value).FirstOrDefault();

                            Int32 totquesatt;

                            Int32 totpages = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                              select new
                                              {
                                                  varid = tutorial.Attribute("id").Value,
                                              }).Max(s => Convert.ToInt32(s.varid));



                            if (model.snumberingcontinue == "cont")
                            {
                                totquesatt = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Element("Incomplete").Value != ""
                                              select tutorial.Attribute("id").Value).Count();
                            }
                            else
                            {
                                totquesatt = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                              where tutorial.Element("Incomplete").Value != ""
                                              && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                              select tutorial.Attribute("id").Value).Count();
                            }

                            string varresques = "";

                            if (resultfirst == null)
                            {
                                var resultsecond = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                                    where tutorial.Element("Incomplete").Value == "N"
                                                   && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                                    orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                                    select tutorial.Attribute("id").Value).Max();

                                try
                                {
                                    varresques = resultsecond.ToString();
                                }
                                catch
                                {
                                }
                                model.scurrentquestion = varresques;
                            }
                            else
                            {
                                varresques = resultfirst.ToString();

                                model.scurrentquestion = varresques;
                            }

                            var resultreview = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                                where tutorial.Element("Incomplete").Value == ""
                                               && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                                orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                                select tutorial.Attribute("id").Value).Max();

                            if (resultreview == null)
                            {
                                var resultreview1 = (from tutorial in xmlDocNew.Descendants("ExamQuestions")
                                                     where tutorial.Element("Incomplete").Value != ""
                                                    && tutorial.Attribute("section_id").Value == model.scurrentsection.ToString()
                                                     orderby Convert.ToInt32(tutorial.Attribute("id").Value)
                                                     select Convert.ToInt32(tutorial.Attribute("id").Value)).Max();

                                model.scurrentquestion = resultreview1.ToString();
                            }

                        }

                        //Response.End();
                    }

                }
                else
                {
                    model.scurrentquestion = model.scurrentquestion.ToString();
                }
            }

            return model;

        }

        public SectionIntroModel UpdateQuesHist(SectionIntroModel model)
        {
            //string filename = Session["attemptid"].ToString();
            string newqueshist = "";
            //string strFilename = "E:\\XMLFile\\" + attemptid + ".xml";
            //string mainpath = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/"); --priyanka
            //string strFilename = mainpath + filename + ".xml";

            XmlDocument xmldoc = new XmlDocument();
            if (System.IO.File.Exists(model.eInfo.strFilename))
            {
                if (model.revcurrenttime != "")
                {
                    newqueshist = "ExamRev##" + model.scurrentsection + "##" + model.examstimerem;

                    string elapsed = "";
                    DateTime temp;
                    if (DateTime.TryParse(model.revcurrenttime.ToString(), out temp))
                    {
                        elapsed = (DateTime.Now - DateTime.Parse(model.revcurrenttime)).ToString();
                    }
                    else
                    {
                        elapsed = "00:00:05";
                    }

                    DateTime st1 = Convert.ToDateTime(model.stimerem);
                    DateTime en = Convert.ToDateTime(elapsed);

                    TimeSpan myTimeSpan1 = new TimeSpan(st1.Hour, st1.Minute, st1.Second);
                    TimeSpan myTimeSpan2 = new TimeSpan(en.Hour, en.Minute, en.Second);

                    string diff = (myTimeSpan1.Subtract(myTimeSpan2)).ToString();

                    if (diff.IndexOf("-") != -1)
                    {
                        diff = "00:00:00";
                    }

                    string checktime = elapsed.ToString();
                    if (checktime.Contains("."))
                    {
                        elapsed = elapsed.Remove(8);
                    }

                    model.stimerem = diff;
                    newqueshist += "##" + model.examstimerem + "##" + model.stimerem + "$$";
                }


                xmldoc.Load(model.eInfo.strFilename);

                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");

                string queshist = xmlRootInfo.ChildNodes[39].InnerText.ToString();

                queshist += newqueshist + "EXEL$$";
                xmlRootInfo.ChildNodes[39].InnerText = queshist;

                xmldoc.Save((model.eInfo.strFilename));
            }
            return model;
        }

        public void CreateHidden(string strFilename, string shdnsectionid, string shdnvarid)
        {
            XmlDocument xmldoc = new XmlDocument();

            if (System.IO.File.Exists(strFilename))
            {
                xmldoc.Load(strFilename);
                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");
                xmlRootInfo.ChildNodes[4].InnerText = DateTime.Now.ToString();
                xmlRootInfo.ChildNodes[5].InnerText = shdnsectionid;
                xmlRootInfo.ChildNodes[12].InnerText = shdnvarid;
                xmlRootInfo.ChildNodes[15].InnerText = shdnvarid;
                xmldoc.Save((strFilename));
            }
        }

        public string StoreVal(string val, string str)
        {
            string[] arr = val.Split(',');

            string retval = "";
            for (int rr = 0; rr < arr.Length; rr++)
            {
                string st = arr[rr].ToString().Substring(0, arr[rr].ToString().IndexOf("|||"));
                int ff = arr[rr].ToString().IndexOf("|||") + 3;
                int ee = arr[rr].ToString().Length - arr[rr].ToString().IndexOf("|||") - 4;
                if (st == str)
                {
                    retval = arr[rr].ToString().Substring(arr[rr].ToString().IndexOf("|||") + 3, arr[rr].ToString().Length - arr[rr].ToString().IndexOf("|||") - 3);
                }
            }
            return retval;
        }

    }
}