using System.Collections.Generic;

namespace agendamento.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
    }
}