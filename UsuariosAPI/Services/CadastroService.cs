using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            Task<IdentityResult> resultadoIdentity = 
                _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                string codigoAtivacao = 
                    _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return Result.Ok().WithSuccess(codigoAtivacao);
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
