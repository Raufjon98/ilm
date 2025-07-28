using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Queries;

public class GetSubjectsTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetSubjectsTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetSubjectsQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllSubjects()
    {
        //Arrange 
        _context.Subjects.AddRange(new List<SubjectEntity>()
        {
            new SubjectEntity { Name = "ListOfSubjects1", TeacherId = 5 },
            new SubjectEntity { Name = "ListOfSubjects5", TeacherId = 3 },
            new SubjectEntity { Name = "ListOfSubjects4", TeacherId = 4 },
        });
        await _context.SaveChangesAsync();
        var query = new GetSubjectsQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<SubjectVM> subjectVms = result.ToList();
        subjectVms.Should().NotBeNull();
        subjectVms.Should().BeOfType<List<SubjectVM>>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
