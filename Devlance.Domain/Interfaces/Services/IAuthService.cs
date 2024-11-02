using Devlance.Domain.DTOs.User;
using Devlance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Response<AuthModel>> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(TokenRequestModel model);
        Task<string> AssignUserToRoleAsync(AssignUserToRoleModel model);
    }
}
