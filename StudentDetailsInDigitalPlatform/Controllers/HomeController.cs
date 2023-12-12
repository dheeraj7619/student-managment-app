using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDetailsInDigitalPlatform.Migrations;
using StudentDetailsInDigitalPlatform.Models;
using StudentDetailsInDigitalPlatform.ViewModels;
using System.Data;
using System.Diagnostics;

namespace StudentDetailsInDigitalPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private  IStudentRepositary studentRepositary;

        public HomeController(IWebHostEnvironment webHostEnvironment,IStudentRepositary studentRepositary)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.studentRepositary = studentRepositary;
        }

        [HttpGet]
        public IActionResult Index()
        {
           var students = this.studentRepositary.Getstudents();
            IndexViewModel i = new IndexViewModel();
            i.Students = students;
            return View(i);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename;
                Student student= new Student();
                student.Name = model.Name;
                student.FatherName = model.FatherName;
                student.MotherName = model.MotherName;
                student.Email = model.Email;
                student.Gender= model.Gender;
                student.Address= model.Address;
                student.PhoneNumber = model.PhoneNumber;
                student.AddharNumber = model.AddharNumber;
                student.AdmissionNumber = model.AdmissionNumber;
                student.RegisterNumber = model.RegisterNumber;
                student.Caste = model.Caste;
                student.CourseName= model.CourseName;
                student.SslcPercent = model.SslcPercent;
                student.PucPercent = model.PucPercent;

                if (model.PhotoPath != null)
                {
                    uniqueFilename = ProcessingImage(model);
                    student.Photo = uniqueFilename;
                }


                studentRepositary.Add(student);

                return RedirectToAction("Index");  



            }
            return View();
        }

        private string ProcessingImage(StudentViewModel model)
        {
            string uniqueFilename;
            string fileFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
            uniqueFilename = Guid.NewGuid().ToString() + "_" + model.PhotoPath?.FileName;
            string fileName = Path.Combine(fileFolder, uniqueFilename);
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                model.PhotoPath.CopyTo(fileStream);
            };
            return uniqueFilename;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
           Student student =  this.studentRepositary.GetStudent(id);
            return View(student);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Student student = this.studentRepositary.GetStudent(id);
            StudentEditViewModel s = new StudentEditViewModel();

            if (student != null)
            {


                s.Id = student.StudentId;
                s.Name = student.Name;
                s.FatherName = student.FatherName;
                s.MotherName = student.MotherName;
                s.Gender = student.Gender;
                s.Email = student.Email;
                s.PhoneNumber = student.PhoneNumber;
                s.AddharNumber = student.AddharNumber;
                s.Address = student.Address;
                s.CourseName = student.CourseName;
                s.AdmissionNumber = student.AdmissionNumber;
                s.SslcPercent = student.SslcPercent;
                s.PucPercent = student.PucPercent;
                s.Caste = student.Caste;
                s.RegisterNumber = student.RegisterNumber;
                s.ExistingPhoto = student.Photo;


            }
            return View(s);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(StudentEditViewModel model)
        {

            if(ModelState.IsValid)
            {
                Student s = this.studentRepositary.GetStudent(model.Id);

                if (model.PhotoPath!= null)
                {
                    if(model.ExistingPhoto != null) { 
                        var filePath = Path.Combine(this.webHostEnvironment.WebRootPath, "Images",model.ExistingPhoto);
                        System.IO.File.Delete(filePath); 
                    }
                    s.Photo = ProcessingImage(model);
                }
                s.Name = model.Name;
                s.FatherName = model.FatherName;
                s.MotherName = model.MotherName;
                s.Gender = model.Gender;
                s.Email= model.Email;
                s.PhoneNumber= model.PhoneNumber;
                s.AddharNumber = model.AddharNumber;
                s.Address= model.Address;
                s.CourseName= model.CourseName;
                s.AdmissionNumber= model.AdmissionNumber;
                s.SslcPercent= model.SslcPercent;
                s.PucPercent= model.PucPercent;
                s.Caste = model.Caste;
                s.RegisterNumber= model.RegisterNumber;

                this.studentRepositary.Edit(s);
                return RedirectToAction("Details", "Home", new {id=s.StudentId});

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
             studentRepositary.Delete(id);
           return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult SearchStudentByName(IndexViewModel model)
        {
            if(model.SearchTerm != null)
            {
                List<Student> students =  studentRepositary.SearchByName(model.SearchTerm);
                IndexViewModel i = new IndexViewModel();
                i.Students = students;
                i.SearchTerm = model.SearchTerm;
                return View("Index", i);

            }else if(model.SearchTermById !=null){

                List<Student> students = studentRepositary.SearchById(model.SearchTermById);
                IndexViewModel i = new IndexViewModel();
                i.Students = students;
                i.SearchTermById = model.SearchTermById;
                return View("Index", i);
            }
            return RedirectToAction("Index");
        }
    }
}