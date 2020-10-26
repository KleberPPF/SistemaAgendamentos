using System;

namespace agendamento.Models
{
    public enum VagaEnum
    {
        A, B, C
    }
    public class Agendamento
    {
        public int Id { get; set; }
        public int IdFornecedor { get; set; }
        public DateTime Data { get; set; }
        public VagaEnum Vaga { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}