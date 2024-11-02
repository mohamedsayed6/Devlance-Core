using Devlance.Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Mappers
{
    public static class AuthMapper
    {

        public static AuthModel ToAuthModel(this RegisterModel model)
        {
            return new AuthModel
            {
                Email = model.Email,
            };
        }





    }
}
