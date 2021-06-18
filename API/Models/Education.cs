using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_eduaction")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public University University { get; set; }
        public ICollection<Profiling> Profilings { get; set; }
    }
}
