using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using agendamento.Models;
using Microsoft.AspNetCore.Cors;

namespace agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public FornecedorController(AgendamentoContexto context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Fornecedor.ToList());
        }
    }
}
