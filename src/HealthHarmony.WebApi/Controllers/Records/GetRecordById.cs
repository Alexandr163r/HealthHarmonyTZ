using AutoMapper;
using HealthHarmony.Domain.Interfaces;
using HealthHarmony.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthHarmony.Controllers.Records;

public class GetRecordById : RecordBase
{
    private readonly IRecordService _service;
    private readonly IRecordServiceValidator _validator;
    private readonly IMapper _mapper;

    public GetRecordById(IRecordService service, IRecordServiceValidator validator, IMapper mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpGet("[area]/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var isValidId = await _validator.IsValidIdAsync(id);
        
        if (!isValidId)
        {
            return BadRequest("Запись с таким Id не найдена");
        }

        var record = await _service.GetRecordByIdAsync(id);

        var recordReqestModel = _mapper.Map<RecordReqeustModel>(record);

        return Ok(recordReqestModel);
    }
}