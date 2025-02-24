using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories;
public interface IUserRepository
{
    Task Add(User user);
    Task<bool> ExistsEmail(string email);
}
