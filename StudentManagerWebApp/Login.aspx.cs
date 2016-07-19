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
        List<User> Users = new List<User>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Users.Add(new User("s1", "123", "student", 1));
            Users.Add(new User("s2", "123", "student", 2));
            Users.Add(new User("s3", "123", "student", 3));
            Users.Add(new User("s4", "123", "student", 4));
            Users.Add(new User("s5", "123", "student", 5));
            Users.Add(new User("t1", "123", "teacher", 6));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {           
            foreach (User u in Users)
            {
                if (loginBox.Text == u.Username && passwordBox.Text == u.Password)
                {
                    Session["Role"] = u.Role;
                    Session["ID"] = u.Id;                  
                    break;
                }                                
            }

            if(Session["Role"] == null)
            {
                messageLabel.Visible = true;
                loginBox.Text = "";
                passwordBox.Text = "";
            }
            else if(Session["Role"].ToString() == "student")
            {
                
                Server.Transfer("StudentDetails.aspx");
            }    
            else
                Server.Transfer("Main.aspx");
        }
    }

    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
        public User(string username, string password, string role, int id)
        {            
            Username = username;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}