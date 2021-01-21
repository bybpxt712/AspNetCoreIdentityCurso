using AspNetCoreIdentityCurso.Extensions;
using AspNetCoreIdentityCurso.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityCurso.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Usuário acessou a Home");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Excluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Permissoes", "Escrever")]
        public IActionResult SecretClaimHandler()
        {
            return View("Secret3");
        }

        [ClaimsAuthorize("Permissoes", "Editar")]
        public IActionResult ClaimsCustom()
        {
            return View();
        }

        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel
            {
                ErroCode = id
            };

            switch (id)
            {
                case 500:
                    modelError.Mensagem = "Tente novamente mais tarde ou contate nosso suporte.";
                    modelError.Titulo = "Ocorreu um erro";
                    break;
                case 404:
                    modelError.Mensagem = "A página que você está procurando não existe.<br/>Em caso de dúvidas, contate nosso suporte.";
                    modelError.Titulo = "Ops! Página não encontrada";
                    break;
                case 403:
                    modelError.Mensagem = "Você não tem permissão para fazer isso.";
                    modelError.Titulo = "Acesso negado";
                    break;
                default:
                    return StatusCode(404);

            }

            _logger.LogError(modelError.Mensagem);

            return View("Error", modelError);
        }
    }
}
