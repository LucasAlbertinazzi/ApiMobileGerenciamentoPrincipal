using API_AppGerenciamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppGerenciamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPrincipal : ControllerBase
    {
        private readonly GerenciamentoContext _dbContext;

        public MenuPrincipal(GerenciamentoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("menu-principal")]
        public async Task<List<TblAppgerenciamentoPeNaTerraMenuPrincipal>> ItensMenuPrincipal(string func)
        {
            try
            {
                if (!string.IsNullOrEmpty(func))
                {
                    return await _dbContext.TblAppgerenciamentoPeNaTerraMenuPrincipals
                                 .Where(x => x.BtnAtivo == true && (x.DepPermitidos.Contains(func) || x.DepPermitidos.Contains("0")))
                                 .ToListAsync();
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
