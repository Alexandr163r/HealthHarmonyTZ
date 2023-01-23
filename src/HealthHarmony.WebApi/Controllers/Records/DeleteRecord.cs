using HealthHarmony.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthHarmony.Controllers.Records;

public class DeleteRecord : RecordBase
{
    private readonly IRecordService _service;
    private readonly IRecordServiceValidator _validator;

    public DeleteRecord(IRecordService service, IRecordServiceValidator validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpDelete("[area]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isValidId = await _validator.IsValidIdAsync(id);

        if (!isValidId)
        {
            return BadRequest("Запись с таким Id не найдена");
        }

        await _service.DeleteRecordAsync(id);

        return Ok();
    }
}