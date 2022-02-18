using ConfiguracaoArquitetura.api.Business.Entities;
using ConfiguracaoArquitetura.api.Business.Repositories;
using ConfiguracaoArquitetura.api.Configuration;
using ConfiguracaoArquitetura.api.Filters;
using ConfiguracaoArquitetura.api.Infrastructure.Data;
using ConfiguracaoArquitetura.api.Models;
using ConfiguracaoArquitetura.api.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ConfiguracaoArquitetura.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuarioRepository usuarioRepository,
            IAuthenticationService authenticationService)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar o usuario", Type = typeof(LoginViewModelOutput))]
        [SwaggerResponse(statusCode: 400, description: "Sucesso ao autenticar o usuario", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Sucesso ao autenticar o usuario", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

                if (usuario == null)
                {
                    return BadRequest("Houve um erro ao tentar acessar.");
                }

                //if (usuario.Senha != loginViewModel.Senha.GerarSenhaCriptografada())
                //{
                //    return BadRequest("Houve um erro ao tentar acessar.");
                //}

                var usuarioViewModelOutput = new UsuarioViewModelOutput()
                {
                    Codigo = usuario.Codigo,
                    Login = loginViewModelInput.Login,
                    Email = usuario.Email
                };

                var token = _authenticationService.GerarToken(usuarioViewModelOutput);

                return Ok(new LoginViewModelOutput
                {
                    Token = token,
                    Usuario = usuarioViewModelOutput
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }


        /// <summary>
        /// Este serviço permite cadastrar um usuário cadastrado não existente.
        /// </summary>
        /// <param name="loginViewModelInput">View model do registro de login</param>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao registrar o usuario", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Registrar(RegistrarViewModelInput registrarViewModelInput)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterUsuarioAsync(registrarViewModelInput.Login);

                if (usuario != null)
                {
                    return BadRequest("Usuário já cadastrado");
                }

                usuario = new Usuario
                {
                    Login = registrarViewModelInput.Login,
                    Senha = registrarViewModelInput.Senha,
                    Email = registrarViewModelInput.Email
                };
                _usuarioRepository.Adicionar(usuario);
                _usuarioRepository.Commit();

                return Created("", registrarViewModelInput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }
    }
}
