﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;


namespace WebApplication1
{

    public partial class InventorySheetViewOnly : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection connection = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {

            //loads all data into a table printed out by HTML
            if (!this.IsPostBack)
            {
                //Populating a DataTable from database.
                DataTable dt = this.GetData();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table border = '1'>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");

                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        if(column.ColumnName == "StartingPrice" || column.ColumnName == "SellingPrice")
                        {
                            html.Append("<td>");
                            html.Append("$"+row[column.ColumnName]);
                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                    }
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        //function to connect to database and retrieve all info 
        private DataTable GetData()
        {
            
            connection.ConnectionString = "Server=tcp:auction-now.database.windows.net,1433;Initial Catalog=AuctionNow;Persist Security Info=False;User ID=Shayne@auction-now.database.windows.net;Password= auctionteam$4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection.Open();
            {
                using (SqlCommand cmd = new SqlCommand("SELECT AuctionName,ItemName,Quantity,StartingPrice,SellingPrice,Condition,Comments,Size,StorageLocation FROM InventorySheet;"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = connection;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
    }
}