using API_AppGerenciamento.Suporte;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_AppGerenciamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviaArquivos : ControllerBase
    {
        private string path = "\\\\192.168.85.3\\sistema_gerenciamento\\IMAGENS\\ImgAppEstoque\\";

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }
            else
            {
                var file = imageFile;

                string[] parts = file.FileName.Split('_');

                if (parts.Length > 1)
                {
                    string folderName = parts[1];
                    string folderPath = Path.Combine(path, folderName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Path.Combine(folderPath, file.FileName);
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok("Imagem enviada e salva com sucesso.");
        }

        [HttpGet("localiza/{folderName}/{itemName}")]
        public async Task<ActionResult<List<string>>> LocalizaImages(string folderName, string itemName)
        {
            try
            {
                string directoryPath = Path.Combine(path, folderName);

                if (Directory.Exists(directoryPath))
                {
                    List<string> base64Images = new List<string>();

                    string[] imageFiles = Directory.GetFiles(directoryPath, $"*{itemName}*.png");

                    foreach (string imageFile in imageFiles)
                    {
                        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(imageFile);
                        string base64File = Convert.ToBase64String(fileBytes);
                        base64Images.Add(base64File);
                    }

                    return base64Images;
                }
                else
                {
                    return NotFound(); // Retorna 404 se a pasta não for encontrada
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna erro interno do servidor
            }
        }

        [HttpGet("delete/{folderName}")]
        public async Task<IActionResult> DeletaPasta(string folderName)
        {
            try
            {
                string directoryPath = Path.Combine(path, folderName);

                if (Directory.Exists(directoryPath))
                {
                    await Task.Run(() => Directory.Delete(directoryPath, true));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna erro interno do servidor
            }
        }
    }
}
