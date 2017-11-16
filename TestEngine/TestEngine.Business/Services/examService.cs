using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestEngnie.Models.Models;
using System.Xml.Linq;
using System.Xml;


namespace TestEngine.Business.Services
{

    public class examService
    {
        SectionService ss = new SectionService();
        public SectionIntroModel Processing(SectionIntroModel model)
        {

            XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);

            var settings = (from tutorial in xmlDoc.Descendants("Review")
                            select new
                            {
                                varindividual = tutorial.Attribute("individual").Value,
                                varshow = tutorial.Attribute("show").Value,
                            }).FirstOrDefault();
            model.redirecturl= "../Result-test.aspx?newui=true";
            

            Int32 totsections = (from tutorial in xmlDoc.Descendants("Section") select tutorial).Count();

            if (totsections <= Convert.ToInt32(model.scurrentsection))
            {
                if (settings.varindividual == "0" && settings.varshow == "1")
                {
                    ss.UpdateRev("2",model.eInfo.attemptid);
                    ss.CompleteSection(model.eInfo.strFilename,model.scurrentsection);
                    CompleteSectionHist("S", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    model.sreviewsection = "false";
                    model.redirecturl = "Review.aspx";
                    //redirectionurl("Review.aspx");
                }
                else if (settings.varindividual == "0" && settings.varshow == "0")
                {

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                   
                    return model;
                    //redirectionurl(redriecturl);
                }
                else if (settings.varindividual == "0" && settings.varshow == "2")
                {

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    
                    return model;
                    //redirectionurl(redriecturl);
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

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    return model;
                   // redirectionurl(redriecturl);
                }

                if (result.varshowreview.ToString() != "1" && totsections > Convert.ToInt32(model.scurrentsection.ToString()))
                {

                    CompleteSectionHist("S", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    ss.CompleteSection(model.eInfo.strFilename,model.scurrentsection);
                    model.scurrentsection = Convert.ToString(Convert.ToInt16(model.scurrentsection) + 1);
                    UpdateSec(model.scurrentsection,model.eInfo.strFilename,model.sreviewbutton);
                    model.redirecturl = "Section-intro-new.aspx";
                    return model;
                    //  redirectionurl("Section-intro-new.aspx");
                }
                else
                {
                    ss.UpdateRev("1",model.eInfo.attemptid);

                    model.sreviewsection = "true";
                    model.redirecturl = "Review-Section.aspx";
                    return model;
                    //redirectionurl("Review-Section.aspx");
                }
            }
            else
            {
                ss.CompleteSection(model.eInfo.strFilename,model.currentsec);

                if (model.ssecview == "1")
                {

                    CompleteSectionHist("S", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    int sec = Convert.ToInt32(model.scurrentsection) + 1;
                    model.redirecturl = "Section-intro-new.aspx";
                    return model;
                    //Response.Redirect("Section-intro-new.aspx?secid=" + sec + "");
                }
                else
                {

                    CompleteSectionHist("S",model.scurrentsection,model.stimerem,model.eInfo.strFilename);
                    model.redirecturl = "Section-intro-new.aspx";
                    return model;
                    // redirectionurl("Section-intro-new.aspx");
                }

            }

            return model;

        }


        public void CompleteSectionHist(string type,string scurrentsection,string stimerem,string strFilename)
        {
            string newquesthist = "";

            if (type == "S")
            {
                newquesthist = "SecComp##" + scurrentsection + "##" + stimerem + "$$";
            }

            if (type == "SE")
            {
                newquesthist = "SecComp##" + scurrentsection + "##" + stimerem + "$$XEND$$";
            }

            else if (type == "C")
            {
                newquesthist = "SecChanged##" + scurrentsection + "##" + stimerem + "$$";

            }
            else if (type == "E")
            {
                newquesthist = "XEND$$";

            }

            //string filename = Session["attemptid"].ToString();
            //string newqueshist = "";
            //string mainpath = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/");
            //string strFilename = mainpath + filename + ".xml";
            XmlDocument xmldoc = new XmlDocument();
            if (System.IO.File.Exists(strFilename))
            {

                xmldoc.Load(strFilename);

                XmlNode xmlRootInfo = xmldoc.SelectSingleNode("//Test//info");

                string queshist = xmlRootInfo.ChildNodes[39].InnerText.ToString();

                queshist += newquesthist;
                xmlRootInfo.ChildNodes[39].InnerText = queshist;

                xmldoc.Save((strFilename));
            }
        }


        public void UpdateSec(string scurrentsection,string strFilename,string sreviewbutton)
        {
           // scurrentsection = (Convert.ToInt32(scurrentsection) + 1).ToString();


            //string filename1 = Session["attemptid"].ToString();
            //string mainpath1 = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/");
            //string strFilename1 = mainpath1 + filename1 + ".xml";

            XmlDocument xmldoc1 = new XmlDocument();
            if (System.IO.File.Exists(strFilename))
            {
                xmldoc1.Load(strFilename);
            }

            XmlNode xmlRoot;

            xmlRoot = xmldoc1.SelectSingleNode("//Test//info");


            xmlRoot.ChildNodes[5].InnerText = scurrentsection;

            if (sreviewbutton != "true")
            {

                xmlRoot.ChildNodes[3].InnerText = "false";
            }
            xmlRoot.ChildNodes[12].InnerText = "1";
            xmldoc1.Save((strFilename));
        }


       


        public void updateQuesTime(string strFilename,string scurrentsection, string scurrentquestion,string sRevType,string stimerem)
        {
            XDocument xmlDoc = Common.LoadXmlFile(strFilename);

            if (xmlDoc!=null)
            {
                var result = from tutorial in xmlDoc.Descendants("ExamQuestions")
                             where tutorial.Attribute("id").Value == scurrentquestion.ToString()
                             && tutorial.Attribute("section_id").Value == scurrentsection.ToString()
                             select tutorial;


                foreach (XElement itemElement in result)
                {
                    string questime = "";
                    string utcquestime = DateTime.UtcNow.ToString();

                    string quesshowtime = itemElement.Element("QuesShowTime").Value.ToString();
                    string utcshowtime = itemElement.Element("UTCQuesTime").Value.ToString();

                    if (sRevType == "sec")
                    {
                        questime = "S" + stimerem;
                        utcquestime = "S" + utcquestime;
                    }
                    else if (sRevType == "exam")
                    {
                        questime = "E" + stimerem;
                        utcquestime = "E" + utcquestime;
                    }
                    else
                    {
                        questime = stimerem;
                    }

                    if (quesshowtime == "")
                    {
                        quesshowtime = stimerem.ToString();
                    }
                    else
                    {
                        quesshowtime = quesshowtime + "," + questime.ToString();
                    }

                    if (utcshowtime == "")
                    {
                        utcshowtime = utcquestime;
                    }
                    else
                    {
                        utcshowtime = utcshowtime + "," + utcquestime;
                    }

                    itemElement.SetElementValue("QuesShowTime", quesshowtime);
                    itemElement.SetElementValue("UTCQuesTime", utcshowtime);
                }
                xmlDoc.Save(strFilename);
            }
        }

        public void processingexit(SectionIntroModel model)
        {

            //string filename = Session["attemptid"].ToString();
            //string mainpath = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath.ToString() + "/");
            //string strFilename = mainpath + filename + ".xml";
            //XmlDocument xmldoc = new XmlDocument();

            //xmldoc.Load(strFilename);

            XDocument xmlDoc = XDocument.Load(model.eInfo.strFilename);

            var settings = (from tutorial in xmlDoc.Descendants("Review")
                            select new
                            {
                                varindividual = tutorial.Attribute("individual").Value,
                                varshow = tutorial.Attribute("show").Value,
                            }).FirstOrDefault();



            string redriecturl = "../Result-test.aspx?newui=true";
            Int32 totsections = (from tutorial in xmlDoc.Descendants("Section") select tutorial).Count();

            if (totsections <= Convert.ToInt32(model.scurrentsection.ToString()))
            {
                if (settings.varindividual.ToString() == "0" && settings.varshow.ToString() == "1")
                {
                    ss.UpdateRev("2",model.eInfo.attemptid);
                    ss.CompleteSection(model.eInfo.strFilename,model.scurrentsection);
                    CompleteSectionHist("S",model.scurrentsection,model.stimerem,model.eInfo.strFilename);
                    model.sreviewsection = "false";
                    model.redirecturl= "Review.aspx";
                   
                    
                }
                else if (settings.varindividual.ToString() == "0" && settings.varshow.ToString() == "0")
                {

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);

                    
                    //redirectionurl(redriecturl);
                }
                else if (settings.varindividual.ToString() == "0" && settings.varshow.ToString() == "2")
                {

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);

                    //redirectionurl(redriecturl);
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

                    CompleteSectionHist("SE", model.scurrentsection, model.stimerem, model.eInfo.strFilename);
                    //redirectionurl(redriecturl);
                }

                if (result.varshowreview.ToString() != "1" && totsections > Convert.ToInt32(model.scurrentsection.ToString()))
                {

                    CompleteSectionHist("S", model.scurrentsection, model.stimerem, model.eInfo.strFilename);

                    UpdateSec(model.scurrentsection,model.eInfo.strFilename,model.sreviewbutton);
                    model.redirecturl= "Section-intro-new.aspx";
                    //redirectionurl("Section-intro-new.aspx");
                }
                else
                {
                    ss.UpdateRev("1",model.eInfo.attemptid);

                    model.sreviewsection = "true";
                    model.redirecturl = "Review-Section.aspx";
                    //redirectionurl("Review-Section.aspx");
                }
            }
            else
            {
                CompleteSectionHist("S",model.scurrentsection,model.stimerem,model.eInfo.strFilename);
                ss.CompleteSection(model.eInfo.strFilename,model.scurrentsection);
                model.redirecturl = "Section-intro-new.aspx";
                //redirectionurl("Section-intro-new.aspx");
            }

        }

        public void updatetime(string strFilename)
        {
            //string filename1 = Session["attemptid"].ToString();
            //string mainpath1 = System.Web.HttpContext.Current.Server.MapPath("~").Replace(wwwroot, "xmlfiles/" + examfilepath + "/");
            //string strFilename1 = mainpath1 + filename1 + ".xml";

            XmlDocument xmldoc1 = new XmlDocument();
            if (System.IO.File.Exists(strFilename))
            {
                xmldoc1.Load(strFilename);
            }

            string stt = DateTime.Now.ToString();

            XmlNode xmlRoot;

            xmlRoot = xmldoc1.SelectSingleNode("//Test//info");

            string unguidval = Guid.NewGuid().ToString();

           // unguid.Value = unguidval; -- priyanka

            xmlRoot.ChildNodes[4].InnerText = stt;   //currenttime
            xmlRoot.ChildNodes[45].InnerText = unguidval;
            xmldoc1.Save((strFilename));



        }
    }
}


