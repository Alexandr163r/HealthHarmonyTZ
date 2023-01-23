using AutoMapper;
using HealthHarmony.Domain.Entities;
using HealthHarmony.Domain.Interfaces;
using HealthHarmony.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthHarmony.Controllers.Records;

public class UpdateRecord : RecordBase
{
    private readonly IRecordService _service;
    private readonly IRecordServiceValidator _validator;
    private readonly IMapper _mapper;

    public UpdateRecord(IRecordService service, IRecordServiceValidator validator, IMapper mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpPut("[area]/{id:guid}")]
    public async Task<IActionResult> Update([FromBody] RecordResponseModel responseModel, Guid id)
    {
        var isValidId = await _validator.IsValidIdAsync(id);
        
        if (!isValidId)
        {
            return BadRequest("Запись с таким Id не найдена");
        }

        var record = _mapper.Map<Record>(responseModel);
        
        await _service.UpdateRecordAsync(id, record);

        return Ok();
    }
}