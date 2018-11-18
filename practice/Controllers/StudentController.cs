using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice.Models;

namespace practice.Controllers
{
    public class StudentController : Controller
    {
        Project1Context _ORM = null;
        IHostingEnvironment _ENV = null;

        public StudentController(Project1Context ORM, IHostingEnvironment ENV) 
        {
            _ORM = ORM;
            _ENV = ENV;
        }
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateStudent(Student1 S, IFormFile ProfilePicture, IFormFile Cv)
        {

            string wwwRootPath = _ENV.WebRootPath;
            string FTPPathForPPs = wwwRootPath + "/WebData/PPs/";

            string UniqueName = Guid.NewGuid().ToString();
            string FileExtension = Path.GetExtension(ProfilePicture.FileName);

            FileStream FS = new FileStream(FTPPathForPPs + UniqueName + FileExtension, FileMode.Create);

            ProfilePicture.CopyTo(FS);






            FS.Close();

            S.ProfilePicture = "/WebData/PPs/" + UniqueName + FileExtension;

            string CVPath = "/WebData/CVs/" + Guid.NewGuid().ToString() + Path.GetExtension(Cv.FileName);
            FileStream CVS = new FileStream(wwwRootPath + CVPath, FileMode.Create);
            Cv.CopyTo(CVS);
            CVS.Close();
            S.Cv = CVPath;




            _ORM.Student1.Add(S);
            _ORM.SaveChanges();



            MailMessage oEmail = new MailMessage();
            oEmail.From = new MailAddress("ansachaudhry4@gmail.com");
            oEmail.To.Add(new MailAddress(S.Email));
            oEmail.CC.Add(new MailAddress("ansachaudhry4@gmail.com"));
            oEmail.Subject = "Welcome to ABC";
            oEmail.Body = "Dear " + S.Name + ",<br><br>" +
                "Thanks for registering with us." +
                "<br><br>" +
                "<b>Regards</b>,<br>Ansa Team";
            oEmail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(S.Cv))
            {
                oEmail.Attachments.Add(new Attachment(wwwRootPath + S.Cv));
            }



            SmtpClient oSMTP = new SmtpClient();
            oSMTP.Host = "smtp.gmail.com";
            oSMTP.Port = 587; //465 //25
            oSMTP.EnableSsl = true;
            oSMTP.Credentials = new System.Net.NetworkCredential("ansachaudhry4@gmail.com", "ansaabbas4");

            try
            {
                oSMTP.Send(oEmail);
            }
            catch (Exception ex)
            {

            }

            _ORM.Student1.Add(S);
            _ORM.SaveChanges();
            ViewBag.Message = "Registration is successfully Done";
            return View();
        }
        
        public IActionResult StudentDetail(int id)

        {
            Student1 S = _ORM.Student1.Where(m => m.Id == id).FirstOrDefault<Student1>();
            
            return View(S);
        }
     
        [HttpGet]
        public IActionResult AllStudents()

        {
            IList<Student1> AllStudents = _ORM.Student1.ToList<Student1>();
            return View(AllStudents);
        }

        [HttpPost]
        public IActionResult AllStudents(string SearchByName, string SearchByClass)
        {

            IList<Student1> AllStudents = _ORM.Student1.Where(m => m.Name.Contains(SearchByName) || m.Department.Contains(SearchByName)).ToList<Student1>();
            return View(AllStudents);

        }

        public IActionResult DeleteStudent(Student1 S)
        {
    
            _ORM.Student1.Remove(S);
            _ORM.SaveChanges();
            
            return RedirectToAction("AllStudents");
        }
        public string DeleteStudentByAjax(Student1 S)
        {
            string result;
            try
            {
                _ORM.Student1.Remove(S);
                _ORM.SaveChanges();
                result = "Yes";
            }
            catch
            {
                result = "No";
            }
            return result;
        }
        [HttpGet]
        public IActionResult EditStudent(int Id)
        {

            Student1 S = _ORM.Student1.Where(m => m.Id == Id).FirstOrDefault<Student1>();
            return View(S);
        }
        public string GetStudentsNames()
        {
            string Result = "";

            var r = Request;

            IList<Student1> All = _ORM.Student1.ToList<Student1>();
            Result += "<h1 class='alert alert-success'>Total Students: " + All.Count + "</h1>";

            foreach (Student1 S in All)
            {
                Result += "<a href='/Student/StudentDetail?Id=" + S.Id + "'><p><span class='glyphicon glyphicon-user'></span> " + S.Name + "</p></a> <a href='/student/deletestudent1?id=" + S.Id + "'>Delete</a>";
            }

            return Result;
        }

        public string ShowAd()
        {
            string Ad = "";
            Ad = "<img class='img img-responsive' src='http://lorempixel.com/400/400/sports/Theta-Solutions/'/>";
            return Ad;
        }
        [HttpPost]
        public IActionResult EditStudent(Student1 S)
        {
            Student1 student = _ORM.Student1.Where(m => m.Id.Equals(S)).FirstOrDefault();

            _ORM.Student1.Update(S);
            _ORM.SaveChanges();
            ViewBag.MessageSucess = "Record Updated Succefully";
           
                    return View(S);
                }
       

       



    }

    
}
