using API_AppGerenciamento.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_AppGerenciamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Log : ControllerBase
    {
        private readonly GerenciamentoContext _dbContext;

        public Log(GerenciamentoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("erro")]
        public async Task<IActionResult> SalvaLogErro(TblAppgerenciamentoPeNaTerraLog log)
        {
            try
            {
                var logerro = new TblAppgerenciamentoPeNaTerraLog
                {
                    Metodo = log.Metodo,
                    Erro = log.Erro,
                    TelaClasse = log.TelaClasse,
                    Dispositivo = log.Dispositivo,
                    Plataforma = log.Plataforma,
                    Versao = log.Versao,
                    Data = log.Data
                };

                _dbContext.TblAppgerenciamentoPeNaTerraLogs.Add(logerro);
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

        [HttpPost]
        [Route("erro-file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var logFolderApp = "FolderLogAppgerenciamento"; // Pasta para guardar o log
            var logFileName = "FileLogAppgerenciamento.txt"; // Nome do arquivo de log
            var logPath = Path.Combine(logFolderApp, logFileName); // Caminho completo para o arquivo de log

            // Verificar se o diretório de log existe, se não, criar
            if (!Directory.Exists(logFolderApp))
            {
                Directory.CreateDirectory(logFolderApp);
            }

            string newFileContent;
            using (var newFileStream = new MemoryStream())
            {
                await file.CopyToAsync(newFileStream);
                newFileContent = Encoding.UTF8.GetString(newFileStream.ToArray());
            }

            // Preparar a entrada do log com a data/hora atual, o nome do arquivo e seu conteúdo
            var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - Arquivo recebido: {file.FileName}\n{newFileContent}\n\n\n\n\n\n\n\n\n";


            // Adicionar a entrada ao arquivo de log de forma que as entradas mais recentes apareçam no topo
            if (System.IO.File.Exists(logPath))
            {
                // Ler as entradas existentes
                var existingEntries = await System.IO.File.ReadAllTextAsync(logPath);
                // Preparar a nova entrada com a entrada mais recente no topo
                var newLogContent = logEntry + "\n" + existingEntries;
                // Escrever de volta ao arquivo de log
                await System.IO.File.WriteAllTextAsync(logPath, newLogContent);
            }
            else
            {
                // Simplesmente criar o arquivo de log com a primeira entrada se ele não existir
                await System.IO.File.WriteAllTextAsync(logPath, logEntry);
            }

            // Removida a parte de salvar o arquivo recebido separadamente para não confundir com o log
            return Ok("Arquivo recebido com sucesso.");
        }
    }
}
