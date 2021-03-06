﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MathFun1000.Account
{
    public partial class Login : Page
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        MySql.Data.MySqlClient.MySqlDataReader reader;
        String queryStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            LoginWithPasswordHashFunction();

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        /**
         * This method checks the password in the textbox against the hashed version stored in the database.
         * Security: SlowHashSalt Implemented.
         **/
        private void LoginWithPasswordHashFunction()
        {
            List<String> salthashList = null;
            List<String> nameList = null;
            try
            {
                String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

                conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
                conn.Open();
                queryStr = "";
                queryStr = "SELECT EmailAddress, SlowHashSalt FROM db_9bad3d_test.userinfo WHERE UserName=?uname";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("?uname", form_login.UserName);
                reader = cmd.ExecuteReader();

                while (reader.HasRows && reader.Read())
                {
                    if (salthashList == null)
                    {
                        salthashList = new List<String>();
                        nameList = new List<String>();
                    }
                    String saltHashes = reader.GetString(reader.GetOrdinal("SlowHashSalt"));
                    salthashList.Add(saltHashes);

                    String userName = reader.GetString(reader.GetOrdinal("EmailAddress"));
                    nameList.Add(userName);
                }
                reader.Close();

                if (salthashList != null)
                {
                    for (int i = 0; i < salthashList.Count; i++)
                    {
                        queryStr = "";
                        bool validUser = PasswordHash.ValidatePassword(form_login.Password, salthashList[i]);
                        if (validUser == true)
                        {
                            Session["uname"] = nameList[i];
                            Response.BufferOutput = true;
                            Response.Redirect("LoggedIn.aspx", false);
                        }
                        else
                        {
                            form_login.FailureText = "User not authenticated";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                form_login.FailureText = "E: User not authenticated";
            }
        }

        /**
         * This is the Regex check for the input to handle SQL Injecttion. 
         **/
        private bool checkAgainstWhiteList(String userInput)
        {
            var regExpression = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9!@#$%^&*]*$");
            if (regExpression.IsMatch(userInput))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //protected void form_login_authenticate(object sender, AuthenticateEventArgs e)
        //{
        //    LoginWithPasswordHashFunction();
        //}
    }
}