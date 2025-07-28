using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Grade.Commands.CreateGrade;
using ilmV3.Application.Grade.Commands.UpdateGrade;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Grade.Commands;

public class UpdateGradeTests
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;

    public UpdateGradeTests()
    {
        var services = new ServiceCollection();
        _gradeRepository = A.Fake<IGradeRepository>();
        services.AddSingleton(_gradeRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateGradeCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();    
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveGrade()
    {
        //Arrange
        var gradeId = 2;
        GradeDto updatedGrade = new GradeDto
        {
            StudentId = 1,
            TeacherId = 2,
            SubjectId = 3,
            ClassDay = "updateDay",
            Date = DateOnly.MaxValue,
        };
        GradeEntity grade = new GradeEntity
        {
            StudentId = 1,
            TeacherId = 2,
            SubjectId = 3,
            ClassDay = "updateDay",
            Date = DateOnly.MaxValue,
        };
        var command = new UpdateGradeCommand(updatedGrade, gradeId);
        A.CallTo(()=> _gradeRepository.GetGradeByIdAsync(gradeId)).Returns(grade);
        A.CallTo(()=> _gradeRepository.UpdateGradeAsync(grade, CancellationToken.None)).Returns(grade);

        //Act
        var result = await _mediator.Send(command);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GradeVM>();
        A.CallTo(()=> _gradeRepository.GetGradeByIdAsync(gradeId)).MustHaveHappened();
        A.CallTo(()=> _gradeRepository.UpdateGradeAsync(A<GradeEntity>.That.Matches(g=>
            g.Id == grade.Id &&
            g.StudentId == grade.StudentId&& 
            g.TeacherId == grade.TeacherId), CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
