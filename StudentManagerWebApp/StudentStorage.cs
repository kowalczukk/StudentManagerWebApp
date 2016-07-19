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
    public class StudentStorage
    {
        public static List<cStudent> getAllStudents()
        {
            List<cStudent> StudentsList = new List<cStudent>();
            var connectionFromConfigarion = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using (SqlConnection con = new SqlConnection(connectionFromConfigarion.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetStudents]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        StudentsList.Add(
                            new cStudent()
                            {
                                Id = (int)dr["ID"],
                                FirstName = (string)dr["FirstName"],
                                LastName = (string)dr["LastName"]
                            }
                            );
                    }
                    dr.Close();
                }
            }
            return StudentsList;
        }

        public static void addStudent(cStudent student)
        {
            //using (StudentsDataDataContext db = new StudentsDataDataContext())
            //{
            //    Student newStudent = new Student
            //    {
            //        ID = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName
            //    };

            //    db.Students.InsertOnSubmit(newStudent);
            //    db.SubmitChanges();
            //}

        }
    }
}