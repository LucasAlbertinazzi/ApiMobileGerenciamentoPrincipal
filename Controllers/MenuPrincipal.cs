using API_AppPousada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AppPousada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPrincipal : ControllerBase
    {
        private readonly PousadaTesteContext _dbContext;

        public MenuPrincipal(PousadaTesteContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("menu-principal")]
        public async Task<List<TblAppPousadaPeNaTerraMenuPrincipal>> ItensMenuPrincipal(string func)
        {
            try
            {
                if (!string.IsNullOrEmpty(func))
                {
                    return await _dbContext.TblAppPousadaPeNaTerraMenuPrincipals
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
