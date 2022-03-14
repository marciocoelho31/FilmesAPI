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
        private readonly SignInManager<CustomIdentityUser> _signinManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signinManager, TokenService tokenService)
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
                Token token = _tokenService
                    .CreateToken(identityUser, 
                    _signinManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result SolicitarResetSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            if (identityUser != null)
            {
                string codigoRecuperacao = _signinManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(codigoRecuperacao);
            }
            return Result.Fail("Falha ao solicitar redefinição");
        }

        public Result EfetuarResetSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            IdentityResult resultadoIdentity = _signinManager.UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

            if (resultadoIdentity.Succeeded)
            {
                return Result.Ok().WithSuccess("Senha redefinida com sucesso");
            }
            return Result.Fail("Falha ao tentar redefinir senha");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signinManager.UserManager.Users
                .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }

    }
}
