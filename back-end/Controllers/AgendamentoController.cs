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
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendamentoContexto _context;

        public AgendamentoController(AgendamentoContexto context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DateTime dataAgendamento, int idFornecedor, VagaEnum vaga)
        {
            var agendamentoNovo = new Agendamento();
            agendamentoNovo.Data = dataAgendamento;
            agendamentoNovo.IdFornecedor = idFornecedor;
            agendamentoNovo.Vaga = vaga;

            string mensagemErro = ValidarRegrasAgendamento(agendamentoNovo);
            if (!string.IsNullOrEmpty(mensagemErro))
            {
                return NotFound(mensagemErro);
            }

            await _context.Agendamento.AddAsync(agendamentoNovo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public ActionResult Put(int idAgendamento, DateTime dataAgendamento, int idFornecedor, VagaEnum vaga)
        {
            try
            {
                var agendamentoNovo = new Agendamento();

                agendamentoNovo.Id = idAgendamento;
                agendamentoNovo.Data = dataAgendamento;
                agendamentoNovo.IdFornecedor = idFornecedor;
                agendamentoNovo.Vaga = vaga;

                ValidarRegrasAgendamento(agendamentoNovo);

                _context.Agendamento.Add(agendamentoNovo);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int idAgendamento)
        {
            Agendamento agendamento = _context.Agendamento.Where(x => x.Id.Equals(idAgendamento)).FirstOrDefault();

            _context.Remove(agendamento);
            _context.SaveChanges();

            return Ok();
        }

         [HttpGet]
        public IActionResult Get()
        {
            List<Agendamento> agendamentos = _context.Agendamento.Select(a => a).OrderBy(a => a.Data).ToList();
            List<AgendamentoDTO> agendamentoDTO = new List<AgendamentoDTO>();

            foreach (var item in agendamentos)
            {
                var fornecedor = _context.Fornecedor.Where(x => x.Id.Equals(item.IdFornecedor)).Select(x => x.Nome).FirstOrDefault();
                
                var agendamentoNovo = new AgendamentoDTO();
                agendamentoNovo.IdAgendamento = item.Id;
                agendamentoNovo.Data = item.Data;
                agendamentoNovo.Fornecedor = fornecedor;
                agendamentoNovo.Vaga = item.Vaga;

                agendamentoDTO.Add(agendamentoNovo);
            }

            return Ok(agendamentoDTO);
        }

        public string ValidarRegrasAgendamento(Agendamento agendamentoNovo)
        {
            string mensagemErro = "";

            List<Agendamento> agendamentosDia = _context.Agendamento.Where(a => a.Data.Year.Equals(agendamentoNovo.Data.Year)
                                                                && a.Data.Month.Equals(agendamentoNovo.Data.Month)
                                                                && a.Data.Day.Equals(agendamentoNovo.Data.Day)).ToList();

            //os agendamentosDia do dia podem ser no máximo 12
            if (agendamentosDia.Count() == 12)
            {
                mensagemErro = "A data selecionada atingiu o limite de agendamentos.";
                return mensagemErro;
            }

            //o agendamento não pode ser fora do horario
            if (agendamentoNovo.Data.Hour < 8 || agendamentoNovo.Data.Hour == 13 || agendamentoNovo.Data.Hour > 18
            || (agendamentoNovo.Data.Hour == 12 && agendamentoNovo.Data.Minute > 0)
            || (agendamentoNovo.Data.Hour == 18 && agendamentoNovo.Data.Minute > 0))
            {
                mensagemErro = "Não é possível agendar neste horário.";
                return mensagemErro;
            }

            //a hora selecionada não pode ter mais de 3 agendamentos
            int agendamentosNaDataSelecionada = agendamentosDia.Where(a => a.Data == agendamentoNovo.Data).Count();
            if (agendamentosNaDataSelecionada == 3)
            {
                mensagemErro = "A hora selecionada excedeu o limite de agendamentos.";
                return mensagemErro;
            }

            //não é possível ocupar uma vaga no mesmo horário
            List<VagaEnum> vagasNaDataSelecionada = agendamentosDia.Where(x => x.Data == agendamentoNovo.Data).Select(x => x.Vaga).ToList();
            if (vagasNaDataSelecionada.Contains(agendamentoNovo.Vaga))
            {
                mensagemErro = "A vaga selecionada está agendada neste horário.";
                return mensagemErro;
            }

            //o término do último fornecimento na vaga selecionada deve ser maior que 30 minutos
            bool intervaloEntrePeriodoUltimoFornecimentoRespeitado = agendamentosDia.Any(x => x.Vaga == agendamentoNovo.Vaga && x.Data.Hour == agendamentoNovo.Data.Hour + 1
             && x.Data.Minute - agendamentoNovo.Data.Minute < 30);
            if (intervaloEntrePeriodoUltimoFornecimentoRespeitado)
            {
                mensagemErro = "Deve ser respeitado um intervalo de 30 minutos para a vaga selecionada nesta data.";
                return mensagemErro;
            }

            //na hora marcada o fornecedor só pode ter 2 vagas reservadas
            int vagasReservadasFornecedor = agendamentosDia.Select(x => x.Data == agendamentoNovo.Data && x.IdFornecedor == agendamentoNovo.IdFornecedor).Count();
            if (vagasReservadasFornecedor == 2)
            {
                mensagemErro = "O fornecedor selecionado excedeu o limite de vagas reservadas neste horário.";
                return mensagemErro;
            }

            return mensagemErro;
        }
    }
}
