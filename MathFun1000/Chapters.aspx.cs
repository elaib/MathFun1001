﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MathFun1000
{
    public partial class Chapters : System.Web.UI.Page
    {
        private Tutorial tut1;
        private Tutorial tut2;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Problems.aspx?chapter=" + "1");
        }

        


    }
}