﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace HZ.Web
{
    public partial class login : System.Web.UI.Page
    {
        protected System.Data.SqlClient.SqlConnection Cn;
        protected System.Data.SqlClient.SqlCommand Cm;
        protected System.Data.SqlClient.SqlCommand Dm;
        protected System.Data.SqlClient.SqlDataAdapter Da;
        protected System.Data.DataSet Ds;
        protected System.Data.SqlClient.SqlDataReader Dr;
        protected System.Data.SqlClient.SqlDataReader Cr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                logins();
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            logins();
        }

        public void logins()
        {
            if (TextBox1.Text != null && TextBox2.Text != null)
            {
                string str = "server=.;database=HUAZHRNG;uid=ab;pwd=123;Trusted_Connection=no";
                Cn = new SqlConnection(str);
                Cn.Open();

                Dm = new SqlCommand("SELECT * FROM user_table WHERE username='" + TextBox1.Text + "'", Cn);
                Cr = Dm.ExecuteReader();

                if (Cr.Read())
                {
                    Session["username"] = TextBox1.Text;
                    Cr.Close();

                    Cm = new SqlCommand("SELECT * FROM user_table WHERE  password ='" + TextBox2.Text + "'", Cn);
                    Dr = Cm.ExecuteReader();
                    if (Dr.Read())//用户名和密码是否正确
                    {
                        
                        Session["password"] = TextBox2.Text;

                        Response.Write("<script>alert('登陆成功')</script>");
                        Dr.Close();

                    }
                    else
                    {

                        Response.Write("<script>alert('密码错误！')</script>");
                    }
                    Cn.Close();
                }
                else
                {
                    Response.Write("<script>alert('用户名不存在！')</script>");
                    
                }
            }
            else
            {
                Response.Write("<script>alert('请输入用户名和密码！')</script>");
            }

        }
    }
}