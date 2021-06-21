using API.Models;
using System.Collections.Generic;

namespace API.Repository.Interfaces
{
    interface IEmployeeRepositoryOld
    {
        IEnumerable<Employee> Get();
        Employee Get(string Nik);
        int Insert(Employee employee);
        int Delete(string nik);
        int Update(Employee employee, string nik);
    }
}
