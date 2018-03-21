using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManager.Apps
{
    public class Student
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string RegisterNumber { get; set; }

        public void Save()
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            SqlConnection con = new SqlConnection(conStr);

            try
            {
                con.Open();
                string insertStudentSql = "INSERT INTO Students(Name,RegisterNumber)" +
                                        "Values(@Name,@RegisterNumber);" +
                                        "SELECT SCOPE_IDENTITY()";

                SqlCommand insertCommand = new SqlCommand(insertStudentSql, con);

                insertCommand.Parameters.AddWithValue("@Name", this.Name);
                insertCommand.Parameters.AddWithValue("@RegisterNumber", this.RegisterNumber);


                //insertCommand.ExecuteNonQuery();
                int retValue = int.Parse(insertCommand.ExecuteScalar().ToString());
                this.id = retValue;


            }

            catch (Exception excp)
            {
                throw;
            }
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            string conStr = ConfigurationManager
                            .ConnectionStrings["conStr"]
                            .ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            try
            {
                con.Open();
                string selectStudentQuery = "SELECT * FROM STUDENTS";
                SqlCommand cmd = new SqlCommand(selectStudentQuery, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student();
                    s.id = (int)reader["id"];
                    s.Name = (string)reader["Name"];
                    s.RegisterNumber = (string)reader["RegisterNumber"];

                    students.Add(s);
                }

            }
            catch (Exception excp)
            {
                throw;
            }

            return students;
        }

        public static int GetTotalStudentCount()
        {
            int totalStudents = 0;
            string conStr = ConfigurationManager
                            .ConnectionStrings["conStr"]
                            .ConnectionString;
            SqlConnection con = new SqlConnection(conStr);

            try
            {
                con.Open();
                string sqlQuery = "select count(*) from Students";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                totalStudents = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch
            {
                throw;
            }
            return totalStudents;
        }
    }
}