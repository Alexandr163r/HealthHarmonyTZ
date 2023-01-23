using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using HealthHarmony.Controllers.Records;
using HealthHarmony.Domain.Interfaces;
using HealthHarmony.Models;
using Microsoft.AspNetCore.Mvc;
using Record = HealthHarmony.Domain.Entities.Record;

namespace HealthHarmony.Tests.Controllers;

public class RecordControllerTests
{
    private readonly IRecordService _service;
    private readonly IMapper _mapper;
    private readonly IRecordServiceValidator _validator;

    public RecordControllerTests()
    {
        _service = A.Fake<IRecordService>();
        _mapper = A.Fake<IMapper>();
        _validator = A.Fake<IRecordServiceValidator>();
    }

    [Fact]
    public async void RecordController_Records_ReturnOk()
    {
        //Arrange
        var records = A.Fake<List<Record>>();
        var recordsModel = A.Fake<List<RecordReqeustModel>>();
        A.CallTo(() => _mapper.Map<List<RecordReqeustModel>>(records)).Returns(recordsModel);

        //Act
        var controller = new GetAllRecords(_service, _mapper);
        var result = await controller.GetAll();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public async void RecordController_CreateRecords_ReturnActionResult()
    {
        //Arrange
        var record = A.Fake<Record>();
        var recordsModel = A.Fake<RecordResponseModel>();
        A.CallTo(() => _mapper.Map<Record>(recordsModel)).Returns(record);
        A.CallTo(() => _service.CreateRecordAsync(record)).Returns(record);
        A.CallTo(() => _validator.IsValidCreateAsync(record)).Returns(true);

        //Act
        
        var controller = new CreateRecord(_service, _validator, _mapper);
        var result = await controller.Create(recordsModel);
        //Assert
        
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkResult));
    }
    
    [Fact]
    public async void RecordController_RecordById_ReturnOk()
    {
        //Arrange
        
        var record = A.Fake<Record>();
        var recordModel = A.Fake<RecordReqeustModel>();
        A.CallTo(() => _mapper.Map<RecordReqeustModel>(record)).Returns(recordModel);
        A.CallTo(() => _validator.IsValidIdAsync(record.Id)).Returns(true);
        //Act
        
        var controller = new GetRecordById(_service,_validator, _mapper);
        var result = await controller.GetById(record.Id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
    
    [Fact]
    public async void RecordController_DeleteRecord_ReturnOk()
    {
        //Arrange

        var id = Guid.NewGuid();
        A.CallTo(() => _validator.IsValidIdAsync(id)).Returns(true);
        A.CallTo(() => _service.DeleteRecordAsync(id)).Returns(true);
        
        //Act

        var controller = new DeleteRecord(_service, _validator);
        var result = await controller.Delete(id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkResult));
    }
    
    [Fact]
    public async void RecordController_UpdateRecord_ReturnOk()
    {
        //Arrange

        var record = A.Fake<Record>();
        var recordsModel = A.Fake<RecordResponseModel>();
        A.CallTo(() => _mapper.Map<Record>(recordsModel)).Returns(record);
        A.CallTo(() => _service.UpdateRecordAsync(record.Id, record)).Returns(true);
        A.CallTo(() => _validator.IsValidIdAsync(record.Id)).Returns(true);
        
        //Act

        var controller = new UpdateRecord(_service, _validator, _mapper);
        var result = await controller.Update(recordsModel, record.Id);

        //Assert
        
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkResult));
    }
}