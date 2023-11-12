using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class Utilizador
    {
        protected int UtiId { get; set; }
        protected string NomeUti { get; set; }
        protected string Email { get; set; }
        protected string Password { get; set; }
        protected DateOnly DataNascimento { get; set; }
        protected string Morada { get; set; }
        protected string CodigoPostal { get; set; }
        protected string Localidade { get; set; }
        protected string ContactoTelefone { get; set; }
        protected string DocIdentificacao { get; set; }
        protected string IBAN { get; set; }
        protected string TipoUtilizador { get; set; }
        protected bool Estado { get; set; }
        protected DateTime DataRegisto { get; set; }    
    }
}
