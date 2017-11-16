using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestEngine.Models.Models;
using TestEngine.Utility.Utils;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace TestEngine.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {


            LoginModel lm = new LoginModel();
            LanguageModel lang = new LanguageModel();

            lang.langFilePath = "E:\\Sukhvir TestEngine\\Files\\XmlFiles\\Language\\Language.xml";

            if (string.IsNullOrEmpty(Convert.ToString(Session["Languagetype"])))
            {
                lang.languageType = "1";
            }
            else
            {
                lang.languageType = Convert.ToString(Session["Languagetype"]);
            }

            lang = Common.ReadLanguageXML(lang.langFilePath, lang.languageType, "login");
            lm.language = lang;


            return View(lm);
        }

        public void TestPin()
        {
            string enableexamcode = "no";
            //object rs = Server.CreateObject("ADODB.recordset");
            //object corp = Server.CreateObject("ADODB.recordset");
            string pin = "";
            string msg = "";
            string sitecode = "";
            int jetrccounter = 0;
            string pntyp = "";
            bool letthecoderun = false;
            string schexamid = "";
            string jetexamid2;
            string varuserfname = "";
            string varuserlname = "";
            string varuseraddress = "";
            string varfirstuserid = "";
            string userid;
            string exmd;
            string attemptid;


            if (Request.QueryString["tp"].Length == 1)
            {
                string strTp = Convert.ToString(Request.QueryString["tp"]);

                if (string.IsNullOrEmpty(strTp))
                {
                    pin = Convert.ToString(Request.Form["tp"]).Trim();
                }
                else
                {
                    pin = strTp.Trim();
                }
                Session["pin_code"] = pin;
                string pincode = pin.Substring(1, pin.Length);

                var regex = "^(0|[1-9][0-9]*)$";
                var isValidnum = Regex.Match(pincode, regex, RegexOptions.IgnoreCase);

                string leftpin = pin.Substring(0, 1);
                if (leftpin != "P" && leftpin != "E")
                {
                    msg = "<span class=red><b>Invalid test pin</b><br>Please try again.</span>";
                }
                else if (pincode == "")
                {
                    msg = "<span class=red><b>Invalid test pin</b><br>Please try again.</span>";
                }
                else if (!isValidnum.Success)
                {
                    msg = "<span class=red><b>Invalid test pin</b><br>Please try again.</span>";
                }
                else
                {
                    string qry = "Select * from pin where pincode=" + pincode;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
                    SqlDataAdapter adp = new SqlDataAdapter(qry, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count == 1)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (Convert.ToString(dr["pintype"]) == "C")
                            {
                                msg = "<span class=red><b>Invalid test pin</b><br>Please try again.......</span>";
                            }
                            else if (Convert.ToString(dr["status"]) == "U")
                            {
                                sitecode = Convert.ToString(dr["sitecode"]).Trim();
                                if (sitecode == "949487")
                                {
                                    qry = "select count(*) as counter from userproducts where sitecode='949487' and status in ('Corp Passed','Failed','Completed')";
                                    SqlDataAdapter adp1 = new SqlDataAdapter(qry, con);
                                    DataSet ds1 = new DataSet();
                                    adp1.Fill(ds1);
                                    if (ds1 != null)
                                    {
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow dr1 = ds1.Tables[0].Rows[0];
                                            if (Convert.ToInt16(dr1["counter"]) >= 1)
                                            {
                                                jetrccounter = Convert.ToInt16(dr1["counter"]);
                                            }

                                            if (jetrccounter >= 15000)
                                            {
                                                Response.Write("<h4>Testing Services are unavailable. Please contact Administrator.</h4>");
                                                Response.End();
                                            }
                                        }
                                    }
                                }
                                pin = Convert.ToString(dr["pincode"]).Trim();
                                pntyp = Convert.ToString(dr["pintype"]);

                                qry = "select EVENTSTARTDATE,eventexpirydate,retakelimit,userid,attemptid,examid from userproducts where pincode='" + pin + "' and  status in('Unattempted','Incomplete') and sitecode='" + sitecode + "'";
                                SqlDataAdapter adp2 = new SqlDataAdapter(qry, con);
                                DataSet ds2 = new DataSet();
                                adp2.Fill(ds2);
                                if (ds2 != null)
                                {
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow dr2 = ds2.Tables[0].Rows[0];
                                        if (string.IsNullOrEmpty(Convert.ToString(dr2["eventexpirydate"])) || (string.IsNullOrEmpty(Convert.ToString(dr2["retakelimit"])) && Convert.ToDateTime(dr2["eventexpirydate"]) < DateTime.Now))
                                        {
                                            msg = "<span class=red><b>Invalid Test pin</b><br>This Test has been expired<br></span>";
                                        }
                                        else if (pntyp == "E")
                                        {
                                            if (string.IsNullOrEmpty(Convert.ToString(dr2["retakelimit"])) || Convert.ToDateTime(dr2["retakelimit"]) > DateTime.Now)
                                            {
                                                letthecoderun = true;
                                            }
                                            else if (Convert.ToDateTime(dr2["retakelimit"]) < DateTime.Now)
                                            {
                                                msg = "<span class=red><b>Invalid test pin</b><br>This test has been expired<br></span>";
                                                letthecoderun = false;
                                            }
                                            schexamid = Convert.ToString(dr2["examid"]);


                                            qry = "select * from exams where catid='223' and positive='2588' and examid='" + schexamid + "'";
                                            SqlDataAdapter adp3 = new SqlDataAdapter(qry, con);
                                            DataSet ds3 = new DataSet();
                                            adp3.Fill(ds3);
                                            if (ds3 != null)
                                            {
                                                if (ds3.Tables.Count > 0)
                                                {
                                                    if (ds3.Tables[0].Rows.Count > 0)
                                                    {
                                                        enableexamcode = "yes";
                                                    }
                                                }

                                            }

                                            qry = "select * from exams where catid='252' and examid='" + schexamid + "'";

                                            SqlDataAdapter adp4 = new SqlDataAdapter(qry, con);
                                            DataSet ds4 = new DataSet();
                                            adp4.Fill(ds4);
                                            if (ds4 != null)
                                            {
                                                if (ds4.Tables.Count > 0)
                                                {
                                                    if (ds4.Tables[0].Rows.Count > 0)
                                                    {
                                                        enableexamcode = "yes";
                                                    }
                                                }

                                            }

                                            string varredirectendtime = "";
                                            qry = "select address from users where address='1' and password<>'' and userid ='" + sitecode + "'";

                                            SqlDataAdapter adp5 = new SqlDataAdapter(qry, con);
                                            DataSet ds5 = new DataSet();
                                            adp5.Fill(ds5);
                                            if (ds5 != null)
                                            {
                                                if (ds5.Tables.Count > 0)
                                                {
                                                    if (ds5.Tables[0].Rows.Count > 0)
                                                    {
                                                        varredirectendtime = "yes";
                                                    }
                                                }
                                            }

                                            if (schexamid == "7634" || sitecode == "1209327" || sitecode == "1209327" || varredirectendtime == "yes")
                                            {
                                                DateTime varcurrentISTtime = Convert.ToDateTime(System.DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy HH:mm:ss tt"));

                                                DateTime varstartime = Convert.ToDateTime(dr2["EVENTSTARTDATE"]);
                                                DateTime varendtime = Convert.ToDateTime(dr2["eventexpirydate"]);
                                                if (varstartime != null && varendtime != null)
                                                {
                                                    double varstartdiff = (varstartime - varcurrentISTtime).TotalSeconds;
                                                    double varenddiff = (varendtime - varcurrentISTtime).TotalSeconds;

                                                    if (varstartdiff >= 0)
                                                    {
                                                        if (varenddiff <= 0)
                                                        {
                                                            letthecoderun = true;
                                                        }
                                                        else
                                                        {
                                                            msg = "<span class=red><b>Your Testing Schedule Time Expired. Please contact invigilator for more Information</b><br></span>";
                                                            letthecoderun = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        DateTime vartime = varstartime;
                                                        int varnewmm = vartime.Month;
                                                        int varnewdate = vartime.Day;
                                                        int varnewyear = vartime.Year;
                                                        //        int varnewtime = FormatDateTime(vartime, 3);
                                                        //vartime = varnewdate & "/" & varnewmm & "/" & varnewyear & " " & varnewtime

                                                        msg = "<span class=red><b>Kindly Wait. Testing will start at: " + vartime + ". Please contact invigilator for more Information</b><br></span>";
                                                        letthecoderun = false;

                                                    }
                                                }
                                            }

                                            if (sitecode == "949487")
                                            {
                                                jetexamid2 = Convert.ToString(dr2["examid"]).Trim();
                                                if (jetexamid2 == "7314")
                                                {

                                                    qry = "SELECT fname, lname, address FROM users where userid='" + Convert.ToString(dr2["userid"]) + "' and partner='949487'";
                                                    SqlDataAdapter adpcheckJet = new SqlDataAdapter(qry, con);
                                                    DataSet dsCheckJet = new DataSet();
                                                    adpcheckJet.Fill(dsCheckJet);
                                                    if (dsCheckJet != null)
                                                    {
                                                        if (dsCheckJet.Tables.Count > 0)
                                                        {
                                                            if (dsCheckJet.Tables[0].Rows.Count > 0)
                                                            {
                                                                DataRow drCheckJet = dsCheckJet.Tables[0].Rows[0];
                                                                varuserfname = Convert.ToString(drCheckJet["fname"]);
                                                                varuserlname = Convert.ToString(drCheckJet["lname"]);
                                                                varuseraddress = Convert.ToString(drCheckJet["address"]);

                                                            }
                                                        }
                                                    }

                                                    qry = "SELECT userid FROM users where fname='" + varuserfname + "' and lname='" + varuserlname + "' and address='" + varuseraddress + "' and partner='949487' and userid not in (" + Convert.ToString(dr2["userid"]) + ")";
                                                    adpcheckJet = new SqlDataAdapter(qry, con);
                                                    adpcheckJet.Fill(dsCheckJet);
                                                    if (dsCheckJet != null)
                                                    {
                                                        if (dsCheckJet.Tables.Count > 0)
                                                        {
                                                            if (dsCheckJet.Tables[0].Rows.Count > 0)
                                                            {
                                                                DataRow drCheckJet = dsCheckJet.Tables[0].Rows[0];
                                                                varfirstuserid = Convert.ToString(drCheckJet["userid"]);
                                                            }
                                                        }
                                                    }

                                                    qry = "SELECT status FROM userproducts where examid=7308 and sitecode='949487' and userid=" + varfirstuserid + " and status in ('Corp Passed', 'Failed', 'Completed')";
                                                    adpcheckJet = new SqlDataAdapter(qry, con);
                                                    adpcheckJet.Fill(dsCheckJet);
                                                    if (dsCheckJet == null)
                                                    {
                                                        msg = "<span class=red><b>Please enter the Test Pin for Test 1. Please contact Test Administrator.</b></span>";
                                                        letthecoderun = false;
                                                    }
                                                    else
                                                    {
                                                        if (dsCheckJet.Tables.Count == 0)
                                                        {
                                                            msg = "<span class=red><b>Please enter the Test Pin for Test 1. Please contact Test Administrator.</b></span>";
                                                            letthecoderun = false;
                                                        }
                                                        else if (dsCheckJet.Tables[0].Rows.Count == 0)
                                                        {
                                                            msg = "<span class=red><b>Please enter the Test Pin for Test 1. Please contact Test Administrator.</b></span>";
                                                            letthecoderun = false;
                                                        }
                                                    }

                                                    dsCheckJet = null;
                                                }

                                            }
                                            if (sitecode == "949487")
                                            {
                                                jetexamid2 = Convert.ToString(dr2["examid"]).Trim();
                                                if (jetexamid2 == "7314")
                                                {
                                                    qry = "SELECT fname, lname, address FROM users where userid='" + Convert.ToString(dr2["userid"]) + "' and partner='949487'";
                                                    SqlDataAdapter adpCheckJet = new SqlDataAdapter(qry, con);
                                                    DataSet dsCheckJet = new DataSet();
                                                    adpCheckJet.Fill(dsCheckJet);
                                                    if (dsCheckJet != null)
                                                    {
                                                        if (dsCheckJet.Tables.Count > 0)
                                                        {
                                                            if (dsCheckJet.Tables[0].Rows.Count > 0)
                                                            {
                                                                varfirstuserid = Convert.ToString(dsCheckJet.Tables[0].Rows[0]["userid"]);
                                                            }
                                                        }
                                                    }
                                                    dsCheckJet = null;
                                                    qry = "SELECT status FROM userproducts where examid=7308 and sitecode='949487' and userid=" + varfirstuserid + " and status in ('Corp Passed', 'Failed', 'Completed')";
                                                    adpCheckJet.Fill(dsCheckJet);
                                                    if (dsCheckJet == null)
                                                    {
                                                        if (dsCheckJet.Tables.Count == 0)
                                                        {
                                                            if (dsCheckJet.Tables[0].Rows.Count == 0)
                                                            {
                                                                msg = "<span class=red><b>Please enter the Test Pin for Test 1. Please contact Test Administrator.</b></span>";
                                                                letthecoderun = false;
                                                            }
                                                        }
                                                    }
                                                    dsCheckJet = null;

                                                }
                                            }

                                            if (letthecoderun)
                                            {
                                                //InsertTestTakenInXML();

                                                Session["non"] = true;

                                                userid = Convert.ToString(dr2["userid"]);

                                                attemptid = Convert.ToString(dr2["attemptid"]);

                                                exmd = Convert.ToString(dr2["examid"]);

                                                Session["exm"] = exmd;

                                                Session["userid"] = userid;

                                                Session["candidateid"] = userid;

                                                Session["cauthenticated"] = "true";

                                                Session["sitecode"] = sitecode;

                                                Session["pincode"] = pin;

                                                Session["pininup"] = "true";
                                                ds = null;

                                                qry = "select * from CorpSettings where  Corpid=" + sitecode;
                                                adp = new SqlDataAdapter(qry, con);
                                                adp.Fill(ds);
                                                if (ds != null)
                                                {
                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        if (ds.Tables[0].Rows.Count > 0)
                                                        {
                                                            dr = ds.Tables[0].Rows[0];
                                                            if (Convert.ToString(dr["ViewResult"]).Trim() == "Y")
                                                                Session["ViewResult"] = "true";
                                                            if (Convert.ToString(dr["SendMail"]) == "Y")
                                                            {
                                                                Session["SendMail"] = "true";
                                                                Session["mailto"] = Convert.ToString(dr["mailto"]).Trim();
                                                            }
                                                            if (Convert.ToString(dr["ReviewWin"]).Trim() == "Y")
                                                                Session["ReviewWin"] = "true";
                                                            if (String.IsNullOrEmpty(Convert.ToString(dr["Theme"]).Trim()))
                                                                Session["seltheme"] = "expertrating";
                                                            else
                                                                Session["seltheme"] = Convert.ToString(dr["Theme"]).Trim();
                                                            if (String.IsNullOrEmpty(Convert.ToString(dr["header"]).Trim()))
                                                                Session["selheader"] = "expertrating.gif";
                                                            else
                                                                Session["selheader"] = Convert.ToString(dr["header"]).Trim();

                                                            if (String.IsNullOrEmpty(Convert.ToString(dr["bgcolor"]).Trim()))
                                                                Session["selbgcolor"] = "";
                                                            else
                                                                Session["selbgcolor"] = "style='background-color:" + Convert.ToString(dr["bgcolor"]).Trim() + ";'";
                                                            if (string.IsNullOrEmpty(Convert.ToString(dr["passcode"]).Trim()))
                                                                Session["passcode"] = "";
                                                            else
                                                                Session["passcode"] = Convert.ToString(dr["passcode"]).Trim();
                                                        }
                                                    }
                                                }
                                                ds = null;
                                                qry = "select fname,lname from users where userid='" + userid + "'";
                                                adp = new SqlDataAdapter(qry, con);
                                                adp.Fill(ds);
                                                if (ds != null)
                                                {
                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        if (ds.Tables[0].Rows.Count > 0)
                                                        {
                                                            Session["fname"] = ds.Tables[0].Rows[0]["fname"];
                                                            Session["lname"] = ds.Tables[0].Rows[0]["lname"];
                                                        }
                                                    }
                                                }
                                                ds = null;

                                                string var_codeassessexamid = exmd;
                                                // string code_path = Server.MapPath("/");
                                                string code_FilePath = "E:\\Sukhvir TestEngine\\Files\\XmlFiles\\CodeAssessXML";
                                                XmlDocument xmldoc = Common.LoadXmlDocument(code_FilePath);
                                                if (xmldoc != null)
                                                {
                                                    if (xmldoc.SelectNodes("//exam[@examid=" + var_codeassessexamid + "]").Count == 1)
                                                    {
                                                        Session["employertyp"] = "false";
                                                        Session["pass_code_url"] = "corpcodeassess/startexam.asp"; //?erattemptid=" + attemptid;

                                                        if (sitecode == "1151004")
                                                        {
                                                            Response.Redirect("Support-page.aspx?attemptid"); // + attemptid & "&site=" & sitecode)
                                                        }
                                                        else
                                                        {
                                                            Response.Redirect("indexpasscode.asp");
                                                        }
                                                    }

                                                }

                                                if (sitecode == "1151004" || sitecode == "1254997")
                                                {
                                                    Response.Redirect("Support-page.aspx");//?attemptid=" & attemptid & "&site=" & sitecode
                                                }
                                                Session["employertyp"] = "false";
                                                Session["pass_code_url"] = "corpexamstart.asp";//?attemptid=" & attemptid

                                                // SPECER EXAMID ID CHECK FOR REDIRECTION ***********************************************

                                                if (exmd == "9850")
                                                {
                                                    Response.Redirect("/assessment/st.aspx"); //?attemptid=" & attemptid & ""
                                                }


                                                // SPENCER CHECK ENDS /////////////////////////////////////////////////////////////

                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = "<span class=red><b>Invalid test pin</b><br>Please try again.</span>";
                                    }
                                }
                                else
                                {
                                    msg = "<span class=red><b>Invalid test pin</b><br>Please try again.</span>";
                                }
                            }
                        }
                        else
                        {
                            msg = "<span class=red><b>Invalid test pin</b><br>Please try again.......</span>";
                        }
                    }
                    else
                    {
                        msg = "<span class=red><b>Invalid test pin</b><br>Please try again.......</span>";
                    }

                }

            }



        }


        //public DataSet ReturnDataset(SqlCommand qry,System.Data.CommandType qryType, string parameters)
        //{
        //    SqlDataAdapter adp = new SqlDataAdapter(qry);
        //    adp.SelectCommand.CommandType = qryType;
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);

        //    return ds;
        //}
    }
}