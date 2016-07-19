using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace StudentManagerWebApp
{
    

    public partial class Main : System.Web.UI.Page
    {
        public static int id;
        public static string firstname;
        public static string lastname;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadStudents();

        }

        public void LoadStudents()
        {

            //using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            //SqlCommand cmd = new SqlCommand("[dbo].[GetStudents]", con);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            ////connect(cmd);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    dataGridView.DataSource = ds;
            //    dataGridView.DataBind();
            //}
            //closeConnection();
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetStudents]", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dataGridView.DataSource = ds;
                            dataGridView.DataBind();
                        }

                    }
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        protected void dataGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dataGridView.EditIndex = e.NewEditIndex;
            LoadStudents();
        }

        protected void dataGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dataGridView.EditIndex = -1;
            LoadStudents();
        }

        protected void dataGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)dataGridView.Rows[e.RowIndex];
            HiddenField hiddenId = (HiddenField)row.FindControl("hiddenStudentID");
            TextBox txtLastName = (TextBox)row.Cells[2].Controls[0];
            TextBox txtFirstName = (TextBox)row.Cells[3].Controls[0];

            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    //string sql = string.Format("UPDATE Students set LastName = '{0}', FirstName = '{1}' WHERE ID='{2}'", txtLastName.Text, txtFirstName.Text, hiddenId.Value);
                    SqlCommand cmd = new SqlCommand("[dbo].[UpdateStudent]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", hiddenId.Value));
                    cmd.Parameters.Add(new SqlParameter("@fn", txtFirstName.Text));
                    cmd.Parameters.Add(new SqlParameter("@ln", txtLastName.Text));
                    cmd.ExecuteNonQuery();
                    dataGridView.EditIndex = -1;
                    LoadStudents();
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    //string sql = string.Format("Insert into Students (ID, LastName, FirstName) values ((select max(ID) from Students)+1, '{0}', '{1}')", lastNameBox.Text, firstNameBox.Text);
                    SqlCommand cmd = new SqlCommand("[dbo].[AddStudent]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fn", firstNameBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@ln", lastNameBox.Text));
                    cmd.ExecuteNonQuery();
                    LoadStudents();
                    lastNameBox.Text = "";
                    firstNameBox.Text = "";
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        protected void dataGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)dataGridView.Rows[e.RowIndex];
            HiddenField hiddenId = (HiddenField)row.FindControl("hiddenStudentID");

            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    //string sql = string.Format("Delete from Students Where ID = '{0}'", hiddenId.Value);
                    SqlCommand cmd = new SqlCommand("[dbo].[DeleteStudent]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", hiddenId.Value));
                    cmd.ExecuteNonQuery();
                    dataGridView.EditIndex = -1;
                    LoadStudents();
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        protected void dataGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if(e.CommandName== "goToSD")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)dataGridView.Rows[index];
                HiddenField hiddenId = (HiddenField)row.FindControl("hiddenStudentID");
                id = Convert.ToInt32(hiddenId.Value);           
                Server.Transfer("StudentDetails.aspx");
                
            }
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("Login.aspx");
        }

        public void connect(SqlCommand cmd)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                }                
            }
        }

        public void closeConnection()
        {
            con.Close();
            con.Dispose();
        }
    }
        
}