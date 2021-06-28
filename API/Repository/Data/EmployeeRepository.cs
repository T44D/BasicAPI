using API.Context;
using API.Models;
using API.Utils;
using API.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public IEnumerable<ProfilVM> UserData()
        {
            var data = (from employee in context.Employees
                        join profiling in context.Profilings
                        on employee.NIK equals profiling.NIK
                        join education in context.Educations
                        on profiling.EducationId equals education.EducationId
                        join university in context.Universities
                        on education.UniversityId equals university.UniversityId
                        select new ProfilVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Gender = (ViewModel.Gender)employee.Gender,
                            Email = employee.Email,
                            Salary = employee.Salary,
                            PhoneNumber = employee.PhoneNumber,
                            BirthDate = employee.BirthDate,
                            Degree = education.Degree,
                            GPA = education.GPA,
                            UniversityName = university.UniversityName
                        }).ToList();
            return data;
        }

        public List<ProfilVM> Profil(string nik)
        {
            var data = (from employee in context.Employees
                        join profiling in context.Profilings
                        on employee.NIK equals profiling.NIK
                        join education in context.Educations
                        on profiling.EducationId equals education.EducationId
                        join university in context.Universities
                        on education.UniversityId equals university.UniversityId
                        where employee.NIK == $"{nik}"
                        select new ProfilVM
                        {
                            NIK = employee.NIK,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Gender = (ViewModel.Gender)employee.Gender,
                            Email = employee.Email,
                            Salary = employee.Salary,
                            PhoneNumber = employee.PhoneNumber,
                            BirthDate = employee.BirthDate,
                            Degree = education.Degree,
                            GPA = education.GPA,
                            UniversityName = university.UniversityName
                        }).ToList();
            return data;
        }

        public int Register(RegisterVM registerVM)
        {
            var check1 = context.Employees.Find(registerVM.NIK);
            if (check1 == null)
            {
                var check2 = context.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault<Employee>();
                if (check2 == null)
                {
                    var university = context.Universities.Find(registerVM.UniversityId);
                    if (university != null)
                    {
                        //Employee
                        Employee employee = new Employee();
                        employee.NIK = registerVM.NIK;
                        employee.FirstName = registerVM.FirstName;
                        employee.LastName = registerVM.LastName;
                        employee.Gender = (Models.Gender)registerVM.Gender;
                        employee.Email = registerVM.Email;
                        employee.Salary = registerVM.Salary;
                        employee.PhoneNumber = registerVM.PhoneNumber;
                        employee.BirthDate = registerVM.BirthDate;
                        context.Employees.Add(employee);
                        context.SaveChanges();

                        //Account
                        string hash = Hashing.Hash(registerVM.Password);
                        var role = context.Roles.Single(r => r.RoleId == 1);
                        var role2 = context.Roles.Single(r => r.RoleId == 2);
                        Account account = new Account()
                        {
                            NIK = employee.NIK,
                            Password = hash,
                            Roles = new List<Role>()
                        };
                        account.Roles.Add(role);
                        account.Roles.Add(role2);
                        context.Accounts.Add(account);
                        context.SaveChanges();

                        //Education
                        Education education = new Education();
                        education.Degree = registerVM.Degree;
                        education.GPA = registerVM.GPA;
                        education.UniversityId = registerVM.UniversityId;
                        context.Educations.Add(education);
                        context.SaveChanges();

                        //Profiling
                        Profiling profiling = new Profiling();
                        profiling.NIK = employee.NIK;
                        profiling.EducationId = education.EducationId;
                        context.Profilings.Add(profiling);
                        context.SaveChanges();

                        return 3;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
