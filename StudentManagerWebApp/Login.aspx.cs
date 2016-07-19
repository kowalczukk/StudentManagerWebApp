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
        public static string person = "error";
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
                    person = u.who;
                    Main.id = u.id;
                    break;
                }                                
            }

            if(person == "error")
            {
                messageLabel.Visible = true;
                loginBox.Text = "";
                passwordBox.Text = "";
            }
            else if(person== "student")
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
        public string who;
        public int id;
        public user(string Username, string Password, string Who, int Id)
        {            
            username = Username;
            password = Password;
            who = Who;
            id = Id;
        }
    }
}