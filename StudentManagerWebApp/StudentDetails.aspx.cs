using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace StudentManagerWebApp
{
    public partial class StudentDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            idBox.Text = Session["ID"].ToString();
            GetStudent();
            if(Session["Role"].ToString() == "student")
            {
                addMarkLabel.Visible = false;
                addMarkButton.Visible = false;
                numberBox.Visible = false;
                numberLabel.Visible = false;
                typeBox.Visible = false;
                typeLabel.Visible = false;
                GridView.Columns[3].Visible = false;
                GridView.Columns[4].Visible = false;
                returnButton.Text = "WYLOGUJ";
            }
            if (!Page.IsPostBack)
                LoadSubjects();
        }

        public void LoadSubjects()
        {
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetSubjects]", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        DropDownList.DataSource = cmd.ExecuteReader();
                        DropDownList.DataTextField = "SubjectName";
                        DropDownList.DataValueField = "ID";
                        DropDownList.DataBind();
                        
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

        public void LoadMarks()
        {
            GridView.DataSource = null;
            GridView.DataBind();          
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[LoadMarks]", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@studentid", Session["ID"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@subjectid", Convert.ToInt32(DropDownList.SelectedValue)));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridView.DataSource = ds;
                            GridView.DataBind();
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

        public void GetStudent()
        {
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetOneStudent]", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", Session["ID"].ToString()));
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            firstNameBox.Text = (string)dr["FirstName"];
                            lastNameBox.Text = (string)dr["LastName"];
                            break;
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView.EditIndex = e.NewEditIndex;
            LoadMarks();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView.EditIndex = -1;
            LoadMarks();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView.Rows[e.RowIndex];
            HiddenField hiddenId = (HiddenField)row.FindControl("hiddenMarkID");
            TextBox txtNumber = (TextBox)row.Cells[1].Controls[0];
            TextBox txtType = (TextBox)row.Cells[2].Controls[0];

            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();                    
                    SqlCommand cmd = new SqlCommand("[dbo].[UpdateMarks]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", hiddenId.Value));
                    cmd.Parameters.Add(new SqlParameter("@number", txtNumber.Text));
                    cmd.Parameters.Add(new SqlParameter("@whatfor", txtType.Text));
                    cmd.ExecuteNonQuery();
                    GridView.EditIndex = -1;
                    LoadMarks();
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView.Rows[e.RowIndex];
            HiddenField hiddenId = (HiddenField)row.FindControl("hiddenMarkID");

            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();                    
                    SqlCommand cmd = new SqlCommand("[dbo].[DeleteMark]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", hiddenId.Value));
                    cmd.ExecuteNonQuery();
                    GridView.EditIndex = -1;
                    LoadMarks();
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

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMarks();
        }

        protected void returnButton_Click(object sender, EventArgs e)
        {
            if(Session["Role"].ToString() == "student")
            {
                Session["ID"] = null;
                Session["Role"] = null;
                Server.Transfer("Login.aspx");
            }
            else
                Server.Transfer("Main.aspx");
        }

        protected void addMarkButton_Click(object sender, EventArgs e)
        {
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                try
                {
                    con.Open();                    
                    SqlCommand cmd = new SqlCommand("[dbo].[AddMark]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@subjectid", DropDownList.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@studentid", idBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@number", numberBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@whatfor", typeBox.Text));
                    cmd.ExecuteNonQuery();
                    LoadMarks();
                    numberBox.Text = "";
                    typeBox.Text = "";
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
    }
}