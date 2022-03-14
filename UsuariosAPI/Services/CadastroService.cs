using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<CustomIdentityUser> userManager,
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            Task<IdentityResult> resultadoIdentity = 
                _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                _userManager.AddToRoleAsync(usuarioIdentity, "regular");

                string codigoAtivacao = 
                    _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                string codigoAtivacaoEncoded = HttpUtility.UrlEncode(codigoAtivacao);

                _emailService.EnviarEmail(
                    new[] {usuarioIdentity.Email }, 
                    "Link de ativação",
                    usuarioIdentity.Id, 
                    codigoAtivacao);

                return Result.Ok().WithSuccess(codigoAtivacaoEncoded);
            }

            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result AtivarContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users.FirstOrDefault(usuario => usuario.Id == request.UsuarioId);

            var identityResult = _userManager.ConfirmEmailAsync(identityUser, 
                request.CodigoAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta do usuário");
        }
    }
}
