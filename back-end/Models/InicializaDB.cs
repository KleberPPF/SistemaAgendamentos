using System.Linq;

namespace agendamento.Models
{
    public class InicializaDB
    {
        public static void Initialize(AgendamentoContexto context)
        {
            context.Database.EnsureCreated();

            //Procura por fornecedores
            if (context.Fornecedor.Any())
            {
                return; //o BD foi alimentado
            }

            var fornecedores = new Fornecedor[]{
                new Fornecedor{Nome="AB Distribuidora", CNPJ="78942994000152"},
                new Fornecedor{Nome="CD Distribuidora", CNPJ="38704574000113"},
                new Fornecedor{Nome="EF Distribuidora", CNPJ="33612392000107"},
                new Fornecedor{Nome="GH Distribuidora", CNPJ="25889859000124"},
                new Fornecedor{Nome="IJ Distribuidora", CNPJ="65525002000140"},
                new Fornecedor{Nome="KL Distribuidora", CNPJ="40634320000118"},
                new Fornecedor{Nome="MN Distribuidora", CNPJ="86384707000164"},
            };

            foreach (Fornecedor f in fornecedores)
            {
                context.Fornecedor.Add(f);
            }

            context.SaveChanges();
        }
    }
}