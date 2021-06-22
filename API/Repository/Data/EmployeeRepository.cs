using API.Context;
using API.Models;
using API.ViewModel;

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

                var emp = context.Employees.Find(employee.NIK);

                if (emp != null)
                {
                    //Account
                    account.NIK = emp.NIK;
                    account.Password = registerVM.Password;
                    context.Accounts.Add(account);
                    context.SaveChanges();

                    //Education
                    education.Degree = registerVM.Degree;
                    education.GPA = registerVM.GPA;
                    education.UniversityId = registerVM.UniversityId;
                    context.Educations.Add(education);
                    context.SaveChanges();
                    int eduId = education.EducationId;

                    //Profiling
                    profiling.NIK = emp.NIK;
                    profiling.EducationId = eduId;
                    context.Profilings.Add(profiling);

                    var regist = context.SaveChanges();
                    return regist;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int Login(LoginVM loginVM)
        {
            var emp = context.Accounts.Find(loginVM.NIK);
            if (emp != null)
            {
                if (emp.Password.Equals(loginVM.Password))
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
