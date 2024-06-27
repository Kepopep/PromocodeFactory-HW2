using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        /// <response code="200">Возвращает все элементы</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="200">Возвращает элемент по GuiD</response>
        /// <response code="400">В случае отсутствия элемента</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

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
                
                return Ok(employeeModel);
            }
            catch
            {
                return BadRequest($"No element, id={id}");
            }
        }
    
        /// <summary>
        /// Создание новой сущности сотрудника
        /// </summary>
        /// <response code="204">Успешное создание элемента</response>
        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateEmployee(EmployeePost data)
        {
            var newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
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

            return NoContent();
        }

        /// <summary>
        /// Обновление информации о сотруднике
        /// </summary>
        /// <response code="200">Обновленные данные об элементе</response>
        /// <response code="400">В случае отсутствия элемента</response>
        [HttpPatch("upd/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(EmployeePost data, Guid id)
        {
            try
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

                return Ok(employee);
            }
            catch
            {
                return BadRequest($"No element, id={id}");
            }
        }

        /// <summary>
        /// Удаление сущности из коллекции 
        /// </summary>
        /// <response code="204">Элемент удален успешно</response>
        /// <response code="400">В случае отсутствия элемента</response>
        [HttpDelete("rm/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            try
            {
                await _employeeRepository.RemoveAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest($"No element, id={id}");
            }
        }
    }
}