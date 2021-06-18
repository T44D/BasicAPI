using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_t_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        public int EducationId { get; set; }
        public Account Account { get; set; }
        public Education Education { get; set; }
    }
}
