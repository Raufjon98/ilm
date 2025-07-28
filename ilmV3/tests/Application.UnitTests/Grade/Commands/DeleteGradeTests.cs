using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.DeleteAbsent;
using ilmV3.Application.Grade.Commands.DeleteGrade;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Grade.Commands;

public class DeleteGradeTests
{
    private readonly IMediator  _mediator;
    private readonly IGradeRepository _gradeRepository;
    private readonly ServiceProvider _provider;

    public DeleteGradeTests()
    {
        var services = new ServiceCollection();
        _gradeRepository = A.Fake<IGradeRepository>();
        services.AddSingleton(_gradeRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(DeleteAbsentCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShoulDeteleGradeAndReturnTrue()
    {
        //Arrange
        var gradeId = 87;
        GradeEntity deleteGrade = new GradeEntity
        {
            Id = gradeId,
            StudentId = 1,
            TeacherId = 1,
            SubjectId = 1,
            Grade = 8,
            ClassDay = "DeleteTest",
            Date = DateOnly.MaxValue
        };
        var command = new DeleteGradeCommand(gradeId);
        A.CallTo(() => _gradeRepository.GetGradeByIdAsync(gradeId)).Returns(deleteGrade);
        A.CallTo(()=> _gradeRepository.DeleteGradeAsync(deleteGrade, CancellationToken.None)).Returns(true);
        
        //Acr
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(()=> _gradeRepository.GetGradeByIdAsync(gradeId)).MustHaveHappened();
        A.CallTo(()=> _gradeRepository.DeleteGradeAsync(deleteGrade, CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
