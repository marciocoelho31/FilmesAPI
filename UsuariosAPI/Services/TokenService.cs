﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario, string role)
        {
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdafd0fdsa0fsad0fdsafa9sdfasdfsaf0sadf"));

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
