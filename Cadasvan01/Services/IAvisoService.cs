using System.Collections.Generic;
using System.Threading.Tasks;
using Cadasvan01.Models;

public interface IAvisoService
{
    Task<int> ContarAvisosNaoLidosAsync(string alunoId);
    Task<List<Aviso>> ObterAvisosNaoLidosAsync(string alunoId);
}
