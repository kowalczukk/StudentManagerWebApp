using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentManagerWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        List<user> Users = new List<user>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Users.Add(new user("s1", "123", "student", 1));
            Users.Add(new user("s2", "123", "student", 2));
            Users.Add(new user("s3", "123", "student", 3));
            Users.Add(new user("s4", "123", "student", 4));
            Users.Add(new user("s5", "123", "student", 5));
            Users.Add(new user("t1", "123", "teacher", 6));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {           
            foreach (user u in Users)
            {
                if (loginBox.Text == u.username && passwordBox.Text == u.password)
                {
                    Session["Login"] = loginBox.Text;
                    Session["Role"] = u.role;
                    Session["ID"] = u.id;                  
                    break;
                }                                
            }

            if(Session["Login"] == null)
            {
                messageLabel.Visible = true;
                loginBox.Text = "";
                passwordBox.Text = "";
            }
            else if(Session["Login"].ToString() == "student")
            {
                
                Server.Transfer("StudentDetails.aspx");
            }    
            else
                Server.Transfer("Main.aspx");
        }
    }

    class user
    {
        public string username;
        public string password;        
        public string role;
        public int id;
        public user(string Username, string Password, string Role, int Id)
        {            
            username = Username;
            password = Password;
            role = Role;
            id = Id;
        }
    }
}