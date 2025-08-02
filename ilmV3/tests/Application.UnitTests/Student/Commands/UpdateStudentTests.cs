using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.UpdateAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Commands.UpdateStudent;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Student.Commands;

public class UpdateStudentTests
{
    private readonly IMediator  _mediator;
    private readonly IStudentRepository _studentRepository;
    private readonly IIdentityService  _identityService;
    private readonly ServiceProvider _provider;

    public UpdateStudentTests()
    {
        var services = new ServiceCollection();
        _studentRepository = A.Fake<IStudentRepository>();
        _identityService = A.Fake<IIdentityService>();
        services.AddSingleton(_identityService);
        services.AddSingleton(_studentRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(UpdateAbsentCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveStudent()
    {
        //Arrange
        var studentId = 8;
        var userId = "TheUserId";
        StudentDto studentDto = new StudentDto
        {
            Name = "Jackie",
        };
        StudentEntity student = new StudentEntity { Id = studentId, Name = "Jackie" };
        ApplicationUser user = new ApplicationUser { Id =  userId , ExternalUserId = studentId, UserName = "Jackie"};
        var command = new UpdateStudentCommand(userId, studentDto);
        A.CallTo(()=> _studentRepository.GetStudentByIdAsync(studentId)).Returns(student);
        A.CallTo(()=> _studentRepository.UpdateStudentAsync(student, CancellationToken.None)).Returns(student);
        A.CallTo(()=> _identityService.GetUserByIdAsync(userId)).Returns(user);
        A.CallTo(()=> _identityService.UpdateUserAsync(user)).Returns(user);
        
        //Act
        var result = await _mediator.Send(command);

        //Assert
        
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentVM>();
        result!.Id.Should().Be(student.Id);
        result.Name.Should().Be(student.Name);


    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
