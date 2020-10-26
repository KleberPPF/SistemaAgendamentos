using System;

namespace agendamento.Models
{
    public enum VagaEnumDTO
    {
        A, B, C
    }
    public class AgendamentoDTO
    {
        public int IdAgendamento { get; set; }
        public DateTime Data { get; set; }
        public VagaEnum Vaga { get; set; }
        public string Fornecedor { get; set; }
    }
}