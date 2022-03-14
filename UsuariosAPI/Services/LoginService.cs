using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signinManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signinManager, TokenService tokenService)
        {
            _signinManager = signinManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity =
                _signinManager.PasswordSignInAsync(
                    request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signinManager
                    .UserManager.Users.FirstOrDefault(
                        usuario => usuario.NormalizedUserName == request.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }
    }
}
