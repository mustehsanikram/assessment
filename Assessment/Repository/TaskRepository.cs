using Assessment.Logging;
using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assessment.Repository
{
    public class TaskRepository
    {
        private SqlConnection con;
         
        private void connection()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
                con = new SqlConnection(constr);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
           

        }
        public bool AddTask(TaskModel model)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("uspAddNewTask", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Title", model.Title);
                com.Parameters.AddWithValue("@Description", model.Description);
                com.Parameters.AddWithValue("@DueDate", model.DueDate);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return true;

                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return false;
            }

        }

        public List<TaskModel> GetAllTasks()
        {
            List<TaskModel> TaskList = new List<TaskModel>();
            try
            {
                connection();
                


                SqlCommand com = new SqlCommand("uspGetAllTasks", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind EmpModel generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {

                    TaskList.Add(

                        new TaskModel
                        {
                            TaskId = Convert.ToInt32(dr["Id"]),
                            Title = Convert.ToString(dr["Title"]),
                            Description = Convert.ToString(dr["Description"]),
                            DueDate = Convert.ToDateTime(dr["DueDate"])

                        }
                        ) ;
                }

                return TaskList;
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
                return TaskList;
            }
            
        }
        public bool UpdateTask(TaskModel model)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("uspUpdateTaskDetails", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TaskId", model.TaskId);
                com.Parameters.AddWithValue("@Title", model.Title);
                com.Parameters.AddWithValue("@Description", model.Description);
                com.Parameters.AddWithValue("@DueDate", model.DueDate);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
                return false;
            }
         
        }

        public bool DeleteTask(int Id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("uspDeleteTask", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TaskId", Id);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
                return false;
            }
            
        }
    }
}