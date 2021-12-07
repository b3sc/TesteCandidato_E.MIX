using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplicationCEP.Data;
using WebApplicationCEP.Models;

namespace WebApplicationCEP.Controllers
{
    public class HomeController : Controller
    {
        private readonly CepContext _context;

        public HomeController(CepContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuscarCEP(string cep)
        {
            var cepObject = Cep.Busca(cep);

            if (!ValidarCEP(cepObject))
                TempData["result"] = "ERROR";
            else
                TempData["result"] = "OK";

            return View("Index", cepObject);
        }

        private bool ValidarCEP(object cep)
        {
            foreach (var item in cep.GetType().GetProperties())
            {
                if (item.PropertyType == typeof(string))
                {
                    string value = (string)item.GetValue(cep);
                    if (!string.IsNullOrEmpty(value))
                        return true;
                }

            }
            return false;
        }

        [HttpPost]
        public async Task<IActionResult> Salvar_Endereco(string cep, string logradouro, string bairro, string localidade, string complemento, string uf, string siafi, string gia, string ibge)
        {
            TempData["result"] = "SAVE";

            Cep obj = new Cep()
            {
                CEP = cep,
                Logradouro = logradouro,
                Bairro = bairro,
                Localidade = localidade,
                Complemento = complemento == null ? "" : complemento,
                UF = uf,
                GIA = gia,
                SIAFI = siafi,
                IBGE = ibge
            };

            if (existeCep(obj.CEP))
            {
                TempData["result"] = "REPEAT";
                return View("Index", obj);

            }

            _context.Ceps.Add(obj);
            await _context.SaveChangesAsync();


            return View("Index");
        }

        private bool existeCep(string cep)
        {
            var pesquisa_Cep = _context.Ceps.Where(o => o.CEP == cep).Count();

            if (pesquisa_Cep != 0)
                return true;
            return false;
        }

        public async Task<IActionResult> ListEnderecos()
        {
            var enderecos = await _context.Ceps.ToListAsync();

            return View(enderecos);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}