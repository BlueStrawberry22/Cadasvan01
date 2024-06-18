using System.Collections.Generic;
using Cadasvan01.Models;

namespace Cadasvan01.ViewModel
{
    public class MotoristaViagemViewModel
    {
        public IEnumerable<Viagem> Viagens { get; set; }
        public string VanSelecionada { get; set; }
    }
}
