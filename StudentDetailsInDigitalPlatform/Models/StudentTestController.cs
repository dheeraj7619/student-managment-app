using StudentDetailsInDigitalPlatform.Migrations;
using StudentDetailsInDigitalPlatform.ViewModels;

namespace StudentDetailsInDigitalPlatform.Models
{
    public class StudentTestController: IStudentRepositary
    {
        List<Student> students;

        public StudentTestController()
        {
            this.students = new List<Student>()
            {
                new Student() {Name="dheeraj",FatherName="dheeraj",MotherName="peter",Email="Abcd@gmail.com",
                    PhoneNumber="hhh",
                }
            };
        }
        public void Add(Student student)
        {
            this.students.Add(student);
        }

        public void Delete(int id)
        {
           this.students.Remove(GetStudent(id));
        }

        public Student Edit(Student student)
        {
            Student s =this.students.Find(e=>e.StudentId==student.StudentId);
            student.Name = student.Name;
            return s;
        }

        public Student GetStudent(int id)
        {
            return this.students.Find(e=>e.StudentId==id);
        }

        public IEnumerable<Student> Getstudents()
        {
            return this.students;
        }

        public List<Student> SearchById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Student> SearchByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
