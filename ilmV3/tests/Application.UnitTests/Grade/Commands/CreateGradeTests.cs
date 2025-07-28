using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Grade.Commands.CreateGrade;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Grade.Commands;

public class CreateGradeTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;
    private readonly IGradeRepository _gradeRepository;

    public CreateGradeTests()
    {
        var services = new ServiceCollection();
        _gradeRepository = A.Fake<IGradeRepository>();
        services.AddSingleton(_gradeRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateGradeCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveGrade()
    {
        //Arrange
        GradeDto gradeCreat = new GradeDto
        {
            SubjectId = 1,
            StudentId = 1,
            TeacherId = 1,
            ClassDay = "testDay",
            Date = DateOnly.MaxValue
        };  
        GradeEntity grade = new GradeEntity()
        {
            SubjectId = 1,
            StudentId = 1,
            TeacherId = 1,
            ClassDay = "testDay",
            Date = DateOnly.MaxValue
        };
        var command = new CreateGradeCommand(gradeCreat);
        A.CallTo(()=> _gradeRepository.CreateGradeAsync(A<GradeEntity>.Ignored, default)).Returns(grade);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GradeVM>();
        result.StudentId.Should().Be(grade.StudentId);
        result.SubjectId.Should().Be(grade.SubjectId);
        A.CallTo(()=> _gradeRepository.CreateGradeAsync(A<GradeEntity>.That.Matches(g=>
            g.ClassDay == grade.ClassDay &&
            g.StudentId == grade.StudentId &&
            g.TeacherId == grade.TeacherId &&
            g.SubjectId == grade.SubjectId), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
