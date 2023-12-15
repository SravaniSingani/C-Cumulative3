using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using SchoolDbAssignmnet.Models;

namespace SchoolDbAssignmnet.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        // Route to Views/Teacher/List.cshtml
        public ActionResult List(string SearchKey=null)
        {
            //method provides list of teachers

            List<Teacher> Teachers = new List<Teacher>();

            // use teacher data controller

            TeacherDataController Controller = new TeacherDataController();

            Teachers = (List<Teacher>)Controller.ListTeachers(SearchKey);


            return View(Teachers);
        }

        // Get: /Teacher/Show/{TeacherId}

        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            // getting teacher info from database
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            // getting teacher info from database
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Author/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmpNumber, decimal Salary)
        {
           

            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmpNumber);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmpNumber = EmpNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        /// <summary>
        /// Routes to dynamically generated 'Teacher Update' page. Gathers information of the selected teacher from the database
        /// </summary>
        /// <param name="id"> Teacher ID</param>
        /// <returns>
        /// A dynamic webpage with a form consisting of existing details of the teacher. The form details can be modified accordingly.
        /// </returns>
        /// <example>
        /// GET: /Teacher/Update/{id}
        /// </example>


        public ActionResult Update(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            // getting teacher info from database
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a Post request with the information of the existing teacher specified by the selected id.
        /// Conveys this info to the API and redirects to the 'Teacher Show' page of the updated teacher.
        /// </summary>
        /// <param name="id"> Teacher ID</param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="EmpNumber"></param>
        /// <param name="Salary"></param>
        /// <returns>
        /// A dynamic webpage with the updated details of the teacher
        /// </returns>
        /// <example>
        /// POST: /Teacher/Update/{id}
        /// Form Data  / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFname": "Sravani"
        /// "TeacherLname": "Singani"
        /// "EmpNumber": "T111"
        /// "Salary": "77.7" 
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmpNumber, decimal Salary)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmpNumber = EmpNumber;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}