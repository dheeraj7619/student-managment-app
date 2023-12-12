using StudentDetailsInDigitalPlatform.Models;

namespace StudentDetailsInDigitalPlatform.ViewModels
{
    public class StudentEditViewModel : StudentViewModel
    {
        public int Id { get; set; }
        public string ? ExistingPhoto{ get; set; }

    }
}
