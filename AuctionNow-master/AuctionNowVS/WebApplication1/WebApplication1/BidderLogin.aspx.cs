﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;

namespace WebApplication1
{
    public partial class BidderLogin : System.Web.UI.Page
    {
        //establishing an SQL conection
        SqlCommand cmd = new SqlCommand();
        SqlConnection connection = new SqlConnection();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet dataset = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            //connection
            connection.ConnectionString = "Server=tcp:auction-now.database.windows.net,1433;Initial Catalog=AuctionNow;Persist Security Info=False;User ID=Shayne@auction-now.database.windows.net;Password= auctionteam$4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //select the data from bidder registration and insert it into the login table
            SqlCommand select = new SqlCommand("SELECT * FROM BidderRegistration WHERE Username=" + TextBox5.Text + "'and Password=" + TextBoxPassword.Text + "'", connection);
            SqlCommand insert = new SqlCommand("insert into LoginTable" + "(Username,Password)values(@Username,@Password)", connection);
            insert.Parameters.AddWithValue("@Username", TextBox5.Text);
            insert.Parameters.AddWithValue("@Password", TextBoxPassword.Text);
            insert.ExecuteNonQuery();
            //exception handle if fields are empty
            if (String.IsNullOrEmpty(TextBox5.Text) ||String.IsNullOrEmpty(TextBoxPassword.Text))
            {

                message("Login Failed, please check for missing fields");

            }
            else
            {
                message("Login successful");
            }

        }
        public void message(String msg)
        {

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + msg + "')</script>");
        }
    }
}