using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.UpdateAbsent;
using ilmV3.Application.Admin.Commands.UpdateAdmin;
using ilmV3.Application.Admin.Queries;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Admin.Commands
{
    public class UpdateAdminTests
    {
        private readonly IMediator _mediator;
        private readonly ServiceProvider _provider;
        private readonly IAdminRepository _adminRepository;
        private readonly IIdentityService _identityService;

        public UpdateAdminTests()
        {
            var services = new ServiceCollection();
            _identityService = A.Fake<IIdentityService>();
            _adminRepository = A.Fake<IAdminRepository>();
            services.AddSingleton(_adminRepository);
            services.AddSingleton(_identityService);            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateAbsentCommandHandler).Assembly));
            _provider = services.BuildServiceProvider();
            _mediator = _provider.GetRequiredService<IMediator>();
        }

        [Test]
        public async Task ShouldUpdateAndRetrieveAdminVM()
        {
            //Arrange
            var adminId = "admin`s-userId";
            AdminDto admin = new AdminDto
            {
                Name = "Jackie",
            };
            A.CallTo(()=> _identityService.GetUserByIdAsync(adminId));
            A.CallTo(() => _identityService.UpdateUserAsync(A<ApplicationUser>.Ignored));
            var command = new UpdateAdminCommand(adminId, admin);

            //Act
            var result = await _mediator.Send(command);

            //Assert
            result.Should().NotBeNull();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _provider?.Dispose();
        }
    }
}
