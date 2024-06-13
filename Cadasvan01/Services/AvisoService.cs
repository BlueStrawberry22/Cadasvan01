using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.EntityFrameworkCore;

public class AvisoService : IAvisoService
{
    private readonly ApplicationDbContext _context;

    public AvisoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> ContarAvisosNaoLidosAsync(string alunoId)
    {
        var aluno = await _context.Usuarios.FindAsync(alunoId);
        if (aluno == null || aluno.MotoristaId == null)
        {
            return 0;
        }

        return await _context.Avisos
            .Where(a => a.MotoristaId == aluno.MotoristaId && !a.Lido)
            .CountAsync();
    }

    public async Task<List<Aviso>> ObterAvisosNaoLidosAsync(string alunoId)
    {
        var aluno = await _context.Usuarios.FindAsync(alunoId);
        if (aluno == null || aluno.MotoristaId == null)
        {
            return new List<Aviso>();
        }

        return await _context.Avisos
            .Where(a => a.MotoristaId == aluno.MotoristaId && !a.Lido)
            .ToListAsync();
    }
}
