using StudentDetailsInDigitalPlatform.Models;

namespace StudentDetailsInDigitalPlatform.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Student>  Students{ get; set; }
        public string SearchTerm { get; set; } 
        public string SearchTermById { get; set; }

    }
}
