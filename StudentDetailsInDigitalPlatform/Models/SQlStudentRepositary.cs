using StudentDetailsInDigitalPlatform.ViewModels;

namespace StudentDetailsInDigitalPlatform.Models
{
    public class SQlStudentRepositary : IStudentRepositary
    {
        private  AppDBContext context;

        public SQlStudentRepositary(AppDBContext context)
        {
            this.context = context;
        }
        public void Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Student student = GetStudent(id);
            context.Students.Remove(student);
            context.SaveChanges();
        }

        public Student Edit(Student student)
        {
            Student s = context.Students.Find(student.StudentId);
            s.Name = student.Name;
            s.FatherName = student.FatherName;
            s.MotherName = student.MotherName;
            s.Gender = student.Gender;
            s.Email = student.Email;
            s.PhoneNumber = student.PhoneNumber;
            s.AddharNumber = student.AddharNumber;
            s.Address = student.Address;
            s.CourseName = student.CourseName;
            s.Photo = student.Photo;
            s.AdmissionNumber = student.AdmissionNumber;
            s.SslcPercent = student.SslcPercent;
            s.PucPercent = student.PucPercent;
            s.Caste = student.Caste;
            s.RegisterNumber = student.RegisterNumber;

            context.SaveChanges();
            return s;
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public IEnumerable<Student> Getstudents()
        {
            return context.Students.OrderBy(s=>s.Name);
        }

        public List<Student> SearchByName(string name)
        {
            return context.Students.Where(e => e.Name.Contains(name)).ToList();
        }

        public List<Student> SearchById(string Id)
        {
            return context.Students.Where(e => e.RegisterNumber.Contains(Id)).ToList();
        }
    }
}
