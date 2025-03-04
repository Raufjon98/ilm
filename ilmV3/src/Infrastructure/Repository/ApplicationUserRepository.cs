using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.interfaces;
using Microsoft.AspNetCore.Identity;

namespace ilmV3.Infrastructure.Repository;
public class ApplicationUserRepository : IApplicationUserRepository
{
    public Task<bool> SaveAsync()
    {
        throw new NotImplementedException();
    }
}
