using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AED
{
    public class Funcionario
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("funcao")]
        public string Funcao { get; set; }

        [JsonProperty("salario")]
        public double Salario { get; set; }

        [JsonProperty("codigo")]
        public int Codigo { get; set; }


        public Funcionario(string nome, string funcao, double salario, int codigo)
        {
            Nome = nome;
            Funcao = funcao;
            Salario = salario;
            Codigo = codigo;            
        }
        public override string ToString()
        {
            return $"Código: {Codigo} \nNome: {Nome}\nFunção: {Funcao}\nSalário: R${Salario}\n\n";
        }
    }
}
