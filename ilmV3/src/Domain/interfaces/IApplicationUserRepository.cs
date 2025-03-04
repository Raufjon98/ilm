using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface IApplicationUserRepository 
{
    Task<bool> SaveAsync();
}
