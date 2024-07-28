using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PreferenceController
{
    private readonly IRepository<Preference> _preferenceRepository;
    
    public PreferenceController(IRepository<Preference> repository)
    {
        _preferenceRepository = repository;
    }
    
    /// <summary>
    /// Получить данные всех предпочтений
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Возвращает все элементы</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<PreferenceResponse>> GetAllPreference()
    {
        var preferences = await _preferenceRepository.GetAllAsync();

        var response = preferences.Select(x =>
            new PreferenceResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        
        return response;
    }
}