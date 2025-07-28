using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.StudentGroup.Commands.CreateStudentGroup;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Commands;

public class CreateStudentGroupTests
{
    private readonly IMediator  _mediator;
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly ServiceProvider _provider;

    public CreateStudentGroupTests()
    {
        var services = new ServiceCollection();
        _studentGroupRepository = A.Fake<IStudentGroupRepository>();
        services.AddSingleton(_studentGroupRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateStudentGroupCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveStudentGroup()
    {
        //Arrange
        StudentGroupDto studentGroupDto = new StudentGroupDto
        {
            Name = "Group creation",
            CodeName = "12-55ta",
            TeacherId = 1,
            SubjectId = 1
        };
        var command = new CreateStudentGroupCommand(studentGroupDto);
        StudentGroupEntity studentGroup= new StudentGroupEntity
        {
            Name = "Group creation",
            CodeName = "12-55ta",
            TeacherId = 1,
            SubjectId = 1
        };
        A.CallTo(() => _studentGroupRepository.CreateStudentGroupAsync(A<StudentGroupEntity>._, A<CancellationToken>._)).Returns(studentGroup);

        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentGroupVM>();
        result.Name.Should().Be(studentGroupDto.Name);
        result.CodeName.Should().Be(studentGroupDto.CodeName);
        A.CallTo(()=> _studentGroupRepository.CreateStudentGroupAsync(A<StudentGroupEntity>.That.Matches(g=> 
            g.Name == studentGroup.Name &&
            g.CodeName == studentGroup.CodeName &&
            g.SubjectId == studentGroup.SubjectId), CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
