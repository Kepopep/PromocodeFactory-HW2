using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.WebHost.Models;
using PromoCodeFactory.WebHost.Models.Post;

namespace PromoCodeFactory.WebHost.Controllers;

/// <summary>
/// Клиенты
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IRepository<Customer> _customerRepository;
    
    public CustomerController(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>
    /// Получить данные всех покупателей
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Возвращает все элементы</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<CustomerResponse>> GetCustomersAsync()
    {
        var customers =  await _customerRepository.GetAllAsync();

        var count1 = customers.ToList();
        var count2 = customers.ToList().Count();
        
        var customersResponseList = customers.ToList().Select(x =>
            new CustomerResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                
                Preferences = x.Preferences?.Select(y => new PreferenceResponse()
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList() ?? new(),
                
                PromoCodes = x.PromoCodes?.Select(y => new PromoCodeResponse
                {
                    Id = y.Id,
                    Code = y.Code,
                    ServiceInfo = y.ServiceInfo,
                    BeginDate = y.BeginDate,
                    EndDate = y.EndDate
                }).ToList() ?? new(),

            }).ToList();
        
        return customersResponseList;
    }
    
    
    /// <summary>
    /// Получить данные покупателя по Id
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Возвращает элемент по GuiD</response>
    /// <response code="400">В случае отсутствия элемента</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerResponse>> GetCutomerByIdAsync(Guid id)
    {
        try
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            var customerResponce = new CustomerResponse()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                
                Preferences = customer.Preferences.Select(y => new PreferenceResponse()
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList(),
                
                PromoCodes = customer.PromoCodes.Select(y => new PromoCodeResponse
                {
                    Id = y.Id,
                    Code = y.Code,
                    ServiceInfo = y.ServiceInfo,
                    BeginDate = y.BeginDate,
                    EndDate = y.EndDate
                }).ToList(),

            };
                
            return Ok(customerResponce);
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
    public async Task<IActionResult> CreateCustomer(CustomerPost data)
    {
        var newCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            
            Preferences = data.Preferences.Select(x => new Preference()
            {
                Name = x
            }).ToList(),
            
            PromoCodes = new List<PromoCode>()
        };
    
        await _customerRepository.AddAsync(newCustomer);
    
        return NoContent();
    }
    
    
    /// <summary>
    /// Обновление информации о покупателе
    /// </summary>
    /// <response code="200">Обновленные данные об элементе</response>
    /// <response code="400">В случае отсутствия элемента</response>
    [HttpPatch("upd/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomer(CustomerPost data, Guid id)
    {
        try
        {
            var updateCustomer = new Customer()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,

                Email = data.Email,
                Preferences = data.Preferences.Select(x => new Preference() 
                { 
                    Name = x, 
                }).ToList()
            };

            return Ok(await _customerRepository.UpdateAsync(id, updateCustomer));
        }
        catch
        {
            return BadRequest($"No element, id={id}");
        }
    }
}