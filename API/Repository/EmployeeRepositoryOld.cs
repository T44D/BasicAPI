using API.Context;
using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class EmployeeRepositoryOld : IEmployeeRepositoryOld
    {
        private readonly MyContext context;
        public EmployeeRepositoryOld(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string nik)
        {
            var find = context.Employees.Find(nik);
            context.Employees.Remove(find);
            var delete = context.SaveChanges();
            return delete;
        }
        public IEnumerable<Employee> Get()
        {
            var employee = context.Employees.ToList();
            return employee;
        }
        public Employee Get(string nik)
        {
            var employee = context.Employees.Find(nik);
            return employee;
        }
        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            var insert = context.SaveChanges();
            return insert;
        }
        public int Update(Employee employee, string nik)
        {
            var employees = context.Employees.Find(nik);
            employee.NIK = employees.NIK;
            if (employee.FirstName != null) employees.FirstName = employee.FirstName;
            if (employee.LastName != null) employees.LastName = employee.LastName;
            if (employee.Email != null) employees.Email = employee.Email;
            if (employee.PhoneNumber != null) employees.PhoneNumber = employee.PhoneNumber;
            if (employee.Salary != 0) employees.Salary = employee.Salary;
            if (employee.BirthDate != null) employees.BirthDate = employee.BirthDate;
            context.Entry(employees).State = EntityState.Modified;
            var update = context.SaveChanges();
            return update;
        }
    }
}
