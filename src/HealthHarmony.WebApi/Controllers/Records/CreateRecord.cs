using AutoMapper;
using HealthHarmony.Domain.Entities;
using HealthHarmony.Domain.Interfaces;
using HealthHarmony.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthHarmony.Controllers.Records;

public class CreateRecord : RecordBase
{
    private readonly IRecordService _service;
    private readonly IRecordServiceValidator _validator;
    private readonly IMapper _mapper;

    public CreateRecord(IRecordService service, IRecordServiceValidator validator, IMapper mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpPost("[area]")]
    public async Task<IActionResult> Create([FromBody] RecordResponseModel responseModel)
    {
        var record = _mapper.Map<Record>(responseModel);

        var isValid = await _validator.IsValidCreateAsync(record);

        if (!isValid)
        {
            return BadRequest("Запись с таким названием уже существует");
        }

        await _service.CreateRecordAsync(record);
        
        return Ok();
    }
}
