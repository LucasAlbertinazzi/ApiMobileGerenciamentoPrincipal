using API_AppPousada.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_AppPousada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Log : ControllerBase
    {
        private readonly PousadaTesteContext _dbContext;

        public Log(PousadaTesteContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("erro")]
        public async Task<IActionResult> SalvaLogErro(TblAppPousadaPeNaTerraLog log)
        {
            try
            {
                var logerro = new TblAppPousadaPeNaTerraLog
                {
                    Metodo = log.Metodo,
                    Erro = log.Erro,
                    TelaClasse = log.TelaClasse,
                    Dispositivo = log.Dispositivo,
                    Plataforma = log.Plataforma,
                    Versao = log.Versao,
                    Data = log.Data
                };

                _dbContext.TblAppPousadaPeNaTerraLogs.Add(logerro);
                await _dbContext.SaveChangesAsync();

                return Ok(logerro);
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
