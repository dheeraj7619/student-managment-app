using StudentDetailsInDigitalPlatform.ViewModels;

namespace StudentDetailsInDigitalPlatform.Models
{
    public interface IStudentRepositary
    {
        void Add(Student student);
        Student Edit(Student student);

        void Delete(int id);

        Student GetStudent(int id);
        IEnumerable<Student> Getstudents();

        List<Student> SearchByName(string name);
        public List<Student> SearchById(string Id);

    }
}
