using AutoMapper;
using HealthHarmony.Domain.Interfaces;
using HealthHarmony.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthHarmony.Controllers.Records;

public class GetAllRecords : RecordBase
{
    private readonly IRecordService _service;
    private readonly IMapper _mapper;

    public GetAllRecords(IRecordService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet("[area]/")]
    public async Task<IActionResult> GetAll()
    {
        var records = await _service.GetAllAsync();
        
        var response = _mapper.Map<List<RecordReqeustModel>>(records);

        return Ok(response);
    }
}