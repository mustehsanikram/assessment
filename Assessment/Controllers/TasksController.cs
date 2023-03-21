
using Assessment.Logging;
using Assessment.Models;
using Assessment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class TasksController : Controller
    {

        public ActionResult GetAllTaskDetails()
        {
            try
            {
                TaskRepository TaskRepo = new TaskRepository();
                ModelState.Clear();
                return View(TaskRepo.GetAllTasks());
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //ViewBag.Message = "Some Technical Error occurred,Please visit after some time";
                //throw;
            }
            return View();
            
        }

        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(TaskModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaskRepository TaskRepo = new TaskRepository();

                    if (TaskRepo.AddTask(model))
                    {
                        ViewBag.Message = "Task details added successfully";
                    }
                }

                
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //ViewBag.Message = "Some Technical Error occurred,Please visit after some time";
                
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            try
            {
                TaskRepository TaskRepo = new TaskRepository();

                return View(TaskRepo.GetAllTasks().Find(Emp => Emp.TaskId == id));
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
                //ViewBag.Message = "Some Technical Error occurred,Please visit after some time";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(TaskModel model)
        {
            try
            {
                TaskRepository TaskRepo = new TaskRepository();

                TaskRepo.UpdateTask(model);
                return RedirectToAction("GetAllTaskDetails");
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
               // ViewBag.Message = "Some Technical Error occurred,Please visit after some time";
            }
            return View();
        }

        public ActionResult DeleteTask(int id)
        {
            try
            {
                TaskRepository TaskRepo = new TaskRepository();
                if (TaskRepo.DeleteTask(id))
                {
                    ViewBag.AlertMsg = "Task details deleted successfully";

                }
                return RedirectToAction("GetAllTaskDetails");

            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
                //ViewBag.Message = "Some Technical Error occurred,Please visit after some time";
            }
            return View();
        }
       
       
    }
}
