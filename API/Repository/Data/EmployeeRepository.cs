using API.Context;
using API.Models;
using API.ViewModel;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext) {
            this.context = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            Account account = new Account();
            Education education = new Education();
            Profiling profiling = new Profiling();
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
                        account.NIK = employee.NIK;
                        account.Password = registerVM.Password;
                        context.Accounts.Add(account);
                        context.SaveChanges();

                        //Education
                        education.Degree = registerVM.Degree;
                        education.GPA = registerVM.GPA;
                        education.UniversityId = registerVM.UniversityId;
                        context.Educations.Add(education);
                        context.SaveChanges();

                        //Profiling
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

        public int Login(LoginVM loginVM)
        {
            var emp = context.Employees
                .Where(e => (e.NIK == loginVM.NIK) || (e.Email == loginVM.Email)).FirstOrDefault<Employee>();
            if (emp != null)
            {
                if (emp.Account.Password.Equals(loginVM.Password))
                {
                    return 2;
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
