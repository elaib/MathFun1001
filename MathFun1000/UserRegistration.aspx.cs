﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MathFun1000
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        String queryStr;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_Confirmation.Text = "";
        }

        /**
         * This class handles the Register button.
         **/
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            registerUserWithSlowHash();
        }

        /**
         * Current Register User Method. 
         * Security: SlowHashSalt
         **/
        private void registerUserWithSlowHash()
        {
            bool methodStatus = true;
            if (InputValidation.ValidateUserName(textboxUserName.Text) == false)
            {
                methodStatus = false;
            }
            if (InputValidation.ValidateEmail(textboxEmailAddress.Text) == false)
            {
                methodStatus = false;
            }
            if (methodStatus == true)
            {
                String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
                conn.Open();
                queryStr = "";

                queryStr = "INSERT INTO db_9bad3d_test.userinfo (UserName, EmailAddress, SlowHashSalt)" +
                "VALUES(?UserName, ?EmailAddress, ?SlowHashSalt)";

                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("?UserName", textboxUserName.Text);
                cmd.Parameters.AddWithValue("?EmailAddress", textboxEmailAddress.Text);

                String saltHashReturned = PasswordHash.CreateHash(textboxPassword.Text);
                int commaIndex = saltHashReturned.IndexOf(":");
                String extractedString = saltHashReturned.Substring(0, commaIndex);
                commaIndex = saltHashReturned.IndexOf(":");
                extractedString = saltHashReturned.Substring(commaIndex + 1);
                commaIndex = extractedString.IndexOf(":");
                String salt = extractedString.Substring(0, commaIndex);
                commaIndex = extractedString.IndexOf(":");
                extractedString = extractedString.Substring(commaIndex + 1);

                String hash = extractedString;
                cmd.Parameters.AddWithValue("?SlowHashSalt", saltHashReturned);

                cmd.ExecuteReader();
                conn.Close();

                lbl_Confirmation.Text = "Congratulations, you are registered!";
            }
        }

        /**
         * Original working Register User Method, not used anymore. 
         * Security: Password in Text
         **/
        //private void registerUser()
        //{
        //    String connString = System.Configuration.ConfigurationManager.ConnectionStrings["webAppconnString"].ToString();
        //    conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        //    conn.Open();
        //    queryStr = "";

        //    queryStr = "INSERT INTO db_9bad3d_test.userinfo (UserName, EmailAddress, Password)" + 
        //        "VALUES('" + textboxUserName.Text + "','" + textboxEmailAddress.Text + "','" + textboxPassword.Text + "')";      

        //    cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
        //    cmd.ExecuteReader();

        //    conn.Close();
        //}
    }
}