using AcademyOnline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Contracts
{
    public interface IJwtGenerator
    {
        string GenerateToken(User user);
    }
}
