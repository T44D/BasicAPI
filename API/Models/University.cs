using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }
}
