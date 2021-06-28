using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace API.ViewModel
{
    public class ProfilVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public string UniversityName { get; set; }
    }
}
