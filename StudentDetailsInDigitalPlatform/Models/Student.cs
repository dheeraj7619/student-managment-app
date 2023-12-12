using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentDetailsInDigitalPlatform.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Father Name")]
        public string FatherName { get; set; }
        [Required]
        [DisplayName("Mother Name")]
        public string MotherName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string ? Photo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Admission Number")]

        public int AdmissionNumber { get; set; }
        [Required]
        [DisplayName("Course Name")]

        public Course CourseName { get; set; }
        [Required]
        [DisplayName("SSLC Percent")]

        public string SslcPercent { get; set; }
        [Required]
        [DisplayName("PUC Percent")]
        public string PucPercent { get; set; }
        [Required]
        public string Caste { get; set;}
        [Required]
        [DisplayName("Addhar Number ")]
        [MaxLength(12)]
        public string AddharNumber { get; set; }
        [Required]
        [DisplayName("Register Number ")]
        [MaxLength(8)]
        public string RegisterNumber { get; set; }

    }
}
