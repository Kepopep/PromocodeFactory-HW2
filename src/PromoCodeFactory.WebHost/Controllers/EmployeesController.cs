using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
       {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }
    
        /// <summary>
        /// Создание новой сущности сотрудника
        /// </summary>
        [HttpPost("new")]
        public async Task<IActionResult> CreateEmployee(EmployeePost data)
        {
            var newEmployee = new Employee()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,

                Email = data.Email,
                Roles = data.Roles.Select(x => new Role() 
                { 
                    Name = x.Name, 
                    Description = x.Description 
                }).ToList()
            };

            await _employeeRepository.AddAsync(newEmployee);

            return Ok();
        }

        /// <summary>
        /// Обновление информации о сотруднике
        /// </summary>
        [HttpPatch("upd/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(EmployeePost data, Guid id)
        {
            var updateEmployee = new Employee()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,

                Email = data.Email,
                Roles = data.Roles.Select(x => new Role() 
                { 
                    Name = x.Name, 
                    Description = x.Description 
                }).ToList()
            };

            var employee = await _employeeRepository.UpdateAsync(id, updateEmployee);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Удаление сущности из коллекции 
        /// </summary>
        [HttpDelete("rm/{id:guid}")]
        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            return await Task.FromResult(Ok(_employeeRepository.RemoveAsync(id)));
        }
        
    }
}