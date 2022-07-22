using Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication
{
    public interface IJwtHandler
    {
        JwtResponse CreateToken(JwtCustomClaims claims);
        JwtCustomClaims ReadFullInfomation(string token);
        bool ValidateToken(string token);
        JwtCustomClaims ReadFullHisTory(string token);
    }
}