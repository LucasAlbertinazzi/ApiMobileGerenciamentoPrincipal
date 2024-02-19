using API_AppGerenciamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppGerenciamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppVersion : ControllerBase
    {
        private readonly GerenciamentoContext _dbContext;

        public AppVersion(GerenciamentoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("verifica-versao")]
        public async Task<IActionResult> VerificaVersao()
        {
            var ultimaLinha = await _dbContext.TblAppGerenciamentoVersaos
                                                .OrderByDescending(x => x.Id)
                                                .FirstOrDefaultAsync();

            if (ultimaLinha == null)
            {
                // Tratar o caso em que a tabela está vazia
                return NotFound();
            }

            return Ok(ultimaLinha);
        }

        [HttpPost]
        [Route("inserir-versao")]
        public async Task<IActionResult> InserirVersao(TblAppGerenciamentoVersao versionInfo)
        {
            try
            {
                var novaVersao = new TblAppGerenciamentoVersao
                {
                    Versao = versionInfo.Versao,
                    Data = versionInfo.Data
                };

                _dbContext.TblAppGerenciamentoVersaos.Add(novaVersao);
                await _dbContext.SaveChangesAsync();

                return Ok(novaVersao);
            }
            catch (Exception ex)
            {
                // Obtém a exceção interna, se houver
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"Erro ao salvar as alterações: {innerExceptionMessage}");
            }
        }
    }
}
