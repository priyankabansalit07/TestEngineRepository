using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml;

namespace TestEngine.Models.Models
{
    public class Common
    {
        #region "XDocument"
        public static XDocument LoadXmlFile(string filename)
        {
            XDocument xmlDoc = new XDocument();
            if (System.IO.File.Exists(filename))
            {
                xmlDoc = XDocument.Load(filename);
            }
            else
            {
                xmlDoc = null;
            }
            return xmlDoc;
        }
        #endregion

        #region "XMLDocument"
        public static XmlDocument LoadXmlDocument(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (System.IO.File.Exists(filename))
            {
                xmlDoc.Load(filename);
            }
            else
            {
                xmlDoc = null;
            }
            return xmlDoc;

        }
        #endregion

        #region "Evaluate Ascii Values"
        public static string RevAsciiValues(string varCheck)
        {
            varCheck = varCheck.Replace("&#60;", "<");
            varCheck = varCheck.Replace("&#62;", ">");
            varCheck = varCheck.Replace("&#61;", "=");
            varCheck = varCheck.Replace("&#39;", "'");
            varCheck = varCheck.Replace("&#41;", ")");
            varCheck = varCheck.Replace("&#40;", "(");
            varCheck = varCheck.Replace("&#45;", "-");
            varCheck = varCheck.Replace("&#44;", ",");
            return varCheck;
        }
        #endregion


        public static LanguageModel ReadLanguageXML(string filepath, string LanguageType, string page)
        {
            LanguageModel lmodel = new LanguageModel();

            lmodel.langFilePath = filepath;
            lmodel.languageType = LanguageType;

            XDocument xmlLang = LoadXmlFile(filepath);


            if (xmlLang != null)
            {
                if (page == "SI")   //SectionIntro
                {
                    var language = (from tutorial in xmlLang.Descendants("language")
                                    where tutorial.Attribute("id").Value == lmodel.languageType
                                    select new
                                    {
                                        //section1 = tutorial.Element("section").Value,
                                        //Exam = tutorial.Element("Exam").Value,
                                        //sectimerem = tutorial.Element("sectimerem").Value,
                                        //test = tutorial.Element("test").Value,
                                        //autoSubmit = tutorial.Element("autoSubmit").Value,
                                        //submitSession = tutorial.Element("submitSession").Value,
                                        //secnextcomp1 = tutorial.Element("secnextcomp1").Value,
                                        //sectionhead = tutorial.Element("sectionhead").Value,
                                        //ques = tutorial.Element("ques").Value,
                                        //RemeiningTime = tutorial.Element("RemeiningTime").Value,
                                        //QuestionPalette = tutorial.Element("QuestionPalette").Value,
                                        //next = tutorial.Element("next").Value,
                                        //mark = tutorial.Element("mark").Value,
                                        //teststartinst = tutorial.Element("teststartinst").Value,
                                        //legendlabel = tutorial.Element("legendlabel").Value,
                                        //quesans = tutorial.Element("quesans").Value,
                                        //quesnotans = tutorial.Element("quesnotans").Value,
                                        //notviewed = tutorial.Element("notviewed").Value,
                                        //MarkedandAnswered = tutorial.Element("MarkedandAnswered").Value,
                                        //MarkedandUnanswered = tutorial.Element("MarkedandUnanswered").Value,
                                        //Version = tutorial.Element("Version").Value,
                                        //Previous = tutorial.Element("previous").Value,
                                        //nextsec = tutorial.Element("nextsec").Value,
                                        //lastques = tutorial.Element("lastques").Value,
                                        //unmark = tutorial.Element("unmark").Value,
                                        //graphicheadtext = tutorial.Element("graphicheadtext").Value,
                                        //quescomptext = tutorial.Element("quescomptext").Value,
                                        //javascriptenable = tutorial.Element("javascriptenable").Value,
                                        //confirmboxhead = tutorial.Element("confirmboxhead").Value,
                                        //confirmtext = tutorial.Element("confirmtext").Value,
                                        //timeuplabel = tutorial.Element("timeuplabel").Value,
                                        //timeexhausted = tutorial.Element("timeexhausted").Value,
                                        //secautocomp2 = tutorial.Element("secautocomp2").Value,
                                        //codesechead = tutorial.Element("codesechead").Value,
                                        //codesessionproceed = tutorial.Element("codesessionproceed").Value,
                                        //alreadyComplated = tutorial.Element("alreadyComplated").Value,
                                        //alreadyattempt = tutorial.Element("alreadyattempt").Value,
                                        //javaerrorheader = tutorial.Element("javaerrorheader").Value,
                                        //javaerrortext = tutorial.Element("javaerrortext").Value,
                                        //quesmarkans = tutorial.Element("quesmarkans").Value,
                                        //quesunmarkans = tutorial.Element("quesunmarkans").Value,
                                        //timeuphead = tutorial.Element("timeuphead").Value,
                                        //seccompleted = tutorial.Element("seccompleted").Value,
                                        //quesview = tutorial.Element("quesview").Value,
                                        //totalques = tutorial.Element("totalques").Value,
                                        //browse = tutorial.Element("browse").Value,
                                        //calcheader = tutorial.Element("calcheader").Value,
                                        //instrclose = tutorial.Element("instrclose").Value,
                                        //submitresponse2 = tutorial.Element("submitresponse2").Value,
                                        //submitresponse1 = tutorial.Element("submitresponse1").Value,
                                        //pageload2 = tutorial.Element("pageload2").Value,
                                        //pageload1 = tutorial.Element("pageload1").Value,
                                        //connectivityissue = tutorial.Element("connectivityissue").Value,
                                        //Attempt = tutorial.Element("Attempt").Value,
                                        //of = tutorial.Element("of").Value,
                                        //yeslabel = tutorial.Element("yeslabel").Value,
                                        //nolabel = tutorial.Element("nolabel").Value,
                                        //oklabel = tutorial.Element("oklabel").Value,
                                        //endtest = tutorial.Element("endtest").Value,

                                        lblproceed = tutorial.Element("lblproceed").Value,
                                        lblremTime = tutorial.Element("lblremTime").Value,
                                        lbltotQuestions = tutorial.Element("lbltotQuestions").Value,
                                        lblsecInst = tutorial.Element("lblsecInst").Value,
                                        lblimageErr = tutorial.Element("lblimageErr").Value

                                    }
 ).FirstOrDefault();
                    //lmodel.confirmtext = language.confirmtext;
                    //lmodel.Exam = language.Exam;
                    //lmodel.sectimerem = language.sectimerem;
                    //lmodel.test = language.test;
                    //lmodel.section1 = language.section1;
                    //lmodel.autoSubmit = language.autoSubmit;
                    //lmodel.submitSession = language.submitSession;
                    //lmodel.secnextcomp1 = language.secnextcomp1;
                    //lmodel.RemeiningTime = language.RemeiningTime;
                    //lmodel.ques = language.ques;
                    //lmodel.sectionhead = language.sectionhead;
                    //lmodel.QuestionPalette = language.QuestionPalette;
                    //lmodel.next = language.next;
                    //lmodel.mark = language.mark;
                    //lmodel.teststartinst = language.teststartinst;
                    //lmodel.legendlabel = language.legendlabel;
                    //lmodel.quesans = language.quesans;
                    //lmodel.quesnotans = language.quesnotans;
                    //lmodel.notviewed = language.notviewed;
                    //lmodel.MarkedandAnswered = language.MarkedandAnswered;
                    //lmodel.MarkedandUnanswered = language.MarkedandUnanswered;
                    //lmodel.Version = language.Version;
                    //lmodel.Previous = language.Previous;
                    //lmodel.nextsec = language.nextsec;
                    //lmodel.lastques = language.lastques;
                    //lmodel.unmark = language.unmark;
                    //lmodel.graphicheadtext = language.graphicheadtext;
                    //lmodel.quescomptext = language.quescomptext;
                    //lmodel.javascriptenable = language.javascriptenable;
                    //lmodel.confirmboxhead = language.confirmboxhead;
                    //lmodel.confirmboxhead = language.confirmboxhead;
                    //lmodel.timeuplabel = language.timeuplabel;
                    //lmodel.timeexhausted = language.timeexhausted;
                    //lmodel.secautocomp2 = language.secautocomp2;
                    //lmodel.codesechead = language.codesechead;
                    //lmodel.codesessionproceed = language.codesessionproceed;
                    //lmodel.alreadyComplated = language.alreadyComplated;
                    //lmodel.alreadyattempt = language.alreadyattempt;
                    //lmodel.javaerrorheader = language.javaerrorheader;
                    //lmodel.javaerrortext = language.javaerrortext;
                    //lmodel.quesmarkans = language.quesmarkans;
                    //lmodel.quesunmarkans = language.quesunmarkans;
                    //lmodel.timeuphead = language.timeuphead;
                    //lmodel.seccompleted = language.seccompleted;
                    //lmodel.quesview = language.quesview;
                    //lmodel.totalques = language.totalques;
                    //lmodel.browse = language.browse;
                    //lmodel.calcheader = language.calcheader;
                    //lmodel.instrclose = language.instrclose;
                    //lmodel.submitresponse2 = language.submitresponse2;
                    //lmodel.submitresponse1 = language.submitresponse1;
                    //lmodel.pageload2 = language.pageload2;
                    //lmodel.pageload1 = language.pageload1;
                    //lmodel.connectivityissue = language.connectivityissue;
                    //lmodel.Attempt = language.Attempt;
                    //lmodel.of = language.of;
                    //lmodel.yeslabel = language.yeslabel;
                    //lmodel.nolabel = language.nolabel;
                    //lmodel.oklabel = language.oklabel;
                    //lmodel.endtest = language.endtest;

                    lmodel.lblproceed = language.lblproceed;
                    lmodel.lblTime = language.lblremTime;
                    lmodel.lbltotQues = language.lbltotQuestions;
                    lmodel.lblsecInst = language.lblsecInst;
                    lmodel.imgError = language.lblimageErr;
                }
                else if (page == "EP")    //Exam Page
                {
                    var language = (from tutorial in xmlLang.Descendants("language")
                                    where tutorial.Attribute("id").Value == lmodel.languageType
                                    select new
                                    {
                                        ques = tutorial.Element("ques").Value,
                                        next = tutorial.Element("next").Value,
                                        previous = tutorial.Element("previous").Value,
                                        mark = tutorial.Element("mark").Value,
                                        unmark = tutorial.Element("unmark").Value,
                                        nextsec = tutorial.Element("nextsec").Value,
                                        totaltimerem = tutorial.Element("totaltimerem").Value,
                                        sectimerem = tutorial.Element("sectimerem").Value,
                                        endtest = tutorial.Element("endtest").Value,
                                        submitresponse1 = tutorial.Element("submitresponse1").Value,
                                        submitresponse2 = tutorial.Element("submitresponse2").Value,
                                        pageload1 = tutorial.Element("pageload1").Value,
                                        pageload2 = tutorial.Element("pageload2").Value,
                                        connectivitylabel1 = tutorial.Element("connectivitylabel1").Value,
                                        connectivitylabel2 = tutorial.Element("connectivitylabel2").Value,
                                        confirmboxhead = tutorial.Element("confirmboxhead").Value,
                                        confirmtext = tutorial.Element("confirmtext").Value,
                                        yeslabel = tutorial.Element("yeslabel").Value,
                                        nolabel = tutorial.Element("nolabel").Value,
                                        timeuplabel = tutorial.Element("timeuplabel").Value,
                                        timeuptext = tutorial.Element("timeuptext").Value,
                                        secautocomp1 = tutorial.Element("secautocomp1").Value,
                                        secautocomp2 = tutorial.Element("secautocomp2").Value,
                                        secnextcomp1 = tutorial.Element("secnextcomp1").Value,
                                        secnextcomp2 = tutorial.Element("secnextcomp2").Value,
                                        testcomp1 = tutorial.Element("testcomp1").Value,
                                        testcomp2 = tutorial.Element("testcomp2").Value,
                                        autosubmitmessage = tutorial.Element("autosubmitmessage").Value,
                                        codesechead = tutorial.Element("codesechead").Value,
                                        codetext = tutorial.Element("codetext").Value,
                                        javaerrorheader = tutorial.Element("javaerrorheader").Value,
                                        javaerrortext = tutorial.Element("javaerrortext").Value,
                                        sectionhead = tutorial.Element("sectionhead").Value,
                                        codeswitchtext = tutorial.Element("codeswitchtext").Value,
                                        quesans = tutorial.Element("quesans").Value,
                                        quesnotans = tutorial.Element("quesnotans").Value,
                                        quesvisited = tutorial.Element("quesvisited").Value,
                                        quesmarkans = tutorial.Element("quesmarkans").Value,
                                        quesunmarkans = tutorial.Element("quesunmarkans").Value,
                                        quesview = tutorial.Element("quesview").Value,
                                        totalques = tutorial.Element("totalques").Value,
                                        timeuphead = tutorial.Element("timeuphead").Value,
                                        seccompleted = tutorial.Element("seccompleted").Value,
                                        instrhead = tutorial.Element("instrhead").Value,
                                        instrtext = tutorial.Element("instrtext").Value,
                                        instrclose = tutorial.Element("instrclose").Value,
                                        quescomptext = tutorial.Element("quescomptext").Value,
                                        graphicheadtext = tutorial.Element("graphicheadtext").Value,
                                        audioheadtext = tutorial.Element("audioheadtext").Value,
                                        lastques = tutorial.Element("lastques").Value,
                                        of = tutorial.Element("of").Value,
                                        legendlabel = tutorial.Element("legendlabel").Value,
                                        calcheader = tutorial.Element("calcheader").Value,
                                        quespalette = tutorial.Element("quespalette").Value,
                                        backbtnpress = tutorial.Element("backbtnpress").Value,
                                        oklabel = tutorial.Element("oklabel").Value,


                                    }).FirstOrDefault();

                    lmodel.slanques = language.ques;
                    lmodel.ssnext = language.next;
                    lmodel.sprevious = language.previous;
                    lmodel.sreview = language.mark;
                    lmodel.sunreview = language.unmark;
                    lmodel.sexamtime = language.totaltimerem;
                    lmodel.snextsec = language.nextsec;
                    lmodel.endtext = language.endtest;
                    lmodel.ssectiontime = language.sectimerem;
                    lmodel.subresponse1 = language.submitresponse1;
                    lmodel.subresponse2 = language.submitresponse2;
                    lmodel.subload1 = language.pageload1;
                    lmodel.subload2 = language.pageload2;
                    lmodel.subconissue1 = language.connectivitylabel1;
                    lmodel.subconissue2 = language.connectivitylabel2;
                    lmodel.subconfirmhead = language.confirmboxhead;
                    lmodel.subconfirmtest = language.confirmtext;
                    lmodel.subyes = language.yeslabel;
                    lmodel.subno = language.nolabel;
                    lmodel.subtimeuphead = language.timeuplabel;
                    lmodel.subtimeuptext = language.timeuptext;
                    lmodel.subsecautocomp1 = language.secautocomp1;
                    lmodel.subsecautocomp2 = language.secautocomp2;
                    lmodel.subsecnextcomp1 = language.secnextcomp1;
                    lmodel.subsecnextcomp2 = language.secnextcomp2;
                    lmodel.sautosubmitmessage = language.autosubmitmessage;
                    lmodel.subtestcomp1 = language.testcomp1;
                    lmodel.subtestcomp2 = language.testcomp2;
                    lmodel.subcodehead = language.codesechead;
                    lmodel.subcodetext = language.codetext;
                    lmodel.javaerrorhead = language.javaerrorheader;
                    lmodel.javaerrortext = language.javaerrortext;
                    lmodel.sectionshead = language.sectionhead;
                    lmodel.codeswitchtext = language.codeswitchtext;
                    lmodel.spanans = language.quesans;
                    lmodel.spannotans = language.quesnotans;
                    lmodel.spannotvisi = language.quesvisited;
                    lmodel.spanmarkans = language.quesmarkans;
                    lmodel.spanmarkunasn = language.quesunmarkans;
                    lmodel.spancurrview = language.quesview;
                    lmodel.totqueshead = language.totalques;
                    lmodel.timeuphead = language.timeuphead;
                    lmodel.seccomphead = language.seccompleted;
                    lmodel.insthead = language.instrhead;
                    lmodel.instructionstext = language.instrtext;
                    lmodel.instclose = language.instrclose;
                    lmodel.compqueshead = language.quescomptext;
                    lmodel.ofh5 = language.of;
                    lmodel.graphichead = language.graphicheadtext;
                    lmodel.audiohead = language.audioheadtext;
                    lmodel.lastqueshead = language.lastques;
                    lmodel.legendhead = language.legendlabel;
                    lmodel.calchead = language.calcheader;
                    lmodel.quespallhead = language.quespalette;
                    lmodel.backbtnpress = language.backbtnpress;
                    lmodel.oklabel = language.oklabel;
                }
            }
            else
            {
                if(page=="SI")
                {
                    lmodel.lblproceed = "Click here to proceed";
                    lmodel.lblTime = "Time";
                    lmodel.lbltotQues = "Total Questions";
                    lmodel.lblsecInst = "Section Instructions";
                    lmodel.imgError = "Your Browser does not support images or you have it turned off. To see this page as it is, please enable images";
                }
                else if(page=="EP")
                {
                    lmodel.slanques = "Question";
                    lmodel.ssnext = "Next";
                    lmodel.sprevious = "Previous";
                    lmodel.sreview = "Mark for Review";
                    lmodel.sunreview = "Unmark for Review";
                    lmodel.sexamtime = "Total Remaining Time";
                    lmodel.snextsec = "Proceed to Next Section";
                    lmodel.endtext = "End Test";
                    lmodel.ssectiontime = "Remaining Time for section";
                    lmodel.subresponse1 = "Submitting your response....";
                    lmodel.subresponse2 = "Do not press back or refresh buttons";
                    lmodel.subload1 = "Loading...........";
                    lmodel.subload2 = "Do not press back or refresh buttons";
                    lmodel.subconissue1 = "Your system seems to be facing connectivity issues..";
                    lmodel.subconissue2 = "Your test timer has been paused until the issue is resolved.";
                    lmodel.subconfirmhead = "Confirmation Box";
                    lmodel.subconfirmtest = "Are you sure you want to end the Test ?";
                    lmodel.subyes = "Yes";
                    lmodel.subno = "No";
                    lmodel.subtimeuphead = "Time Up !";
                    lmodel.subtimeuptext = "You have exhausted the time allocated for this";
                    lmodel.subcodehead = "Proceed to the CodeAssess section";
                    lmodel.subcodetext = "Once you proceed to the CodeAssess section, you will not be able to navigate to other sections of this test. Do you want to proceed ?";
                    lmodel.javaerrorhead = "This page uses Javascript.";
                    lmodel.javaerrortext = "Your browser either doesn't support Javascript or you have it turned off.Please enable Javascript in your browser to proceed with the test.";
                    lmodel.sectionshead = "Sections";
                    lmodel.codeswitchtext = "You can switch to and attempt this section only after you have attempted all the other sections.";
                    lmodel.spanans = "Answered";
                    lmodel.spannotans = "Not Answered";
                    lmodel.spannotvisi = "Not Visited";
                    lmodel.spanmarkans = "Marked  and Answered";
                    lmodel.spanmarkunasn = "Marked and  Unanswered";
                    lmodel.spancurrview = "Currently in View";
                    lmodel.totqueshead = "Total Questions";
                    lmodel.timeuphead = "Time Exhausted";
                    lmodel.seccomphead = "Section Completed";
                    lmodel.insthead = "Instructions";
                    lmodel.instructionstext = "Instruction";
                    lmodel.compqueshead = "* Please provide an answer before you proceed.";
                    lmodel.ofh5 = "of";
                    lmodel.graphichead = "This question is based on the graphic shown below.";
                    lmodel.audiohead = "This question is based on the audio given below.";
                    lmodel.lastqueshead = "* This is the last question of the section.";
                    lmodel.legendhead = "Legend";
                    lmodel.calchead = "Calculator";
                    lmodel.quespallhead = "Question Palette";
                }
            }
            return lmodel;

        }


    }
}
