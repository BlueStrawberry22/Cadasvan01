/* voltar a modificar dps
 * 
using System;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.ViewModel;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoRequerimentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarPdf(AlunoRequerimentoViewModel model)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph($"REQUERIMENTO PARA AUXILIO TRANSPORTE {DateTime.Now.Year}"));
                document.Add(new Paragraph($"Eu, {model.Nome},"));
                document.Add(new Paragraph($"RG nº {model.RG}, CPF Nº {model.CPF} residente a"));
                document.Add(new Paragraph($"{model.Endereco} nº {model.Numero},"));
                document.Add(new Paragraph($"Bairro {model.Bairro} neste Município de {model.Municipio}, matriculado no Curso/Instituição"));
                document.Add(new Paragraph($"{model.InstituicaoCurso} no Município de"));
                document.Add(new Paragraph($"{model.Municipio}. Venho requerer minha INSCRIÇÃO para a Concessão do"));
                document.Add(new Paragraph($"Auxilio Transporte, de acordo com a lei 769/2014.."));
                document.Add(new Paragraph($"Telefone: {model.Telefone}"));
                document.Add(new Paragraph($"( ) Passagem Intermunicipal ( ) Veiculos Fretados – Nome do motorista {model.NomeDoMotorista}"));
                document.Add(new Paragraph($"Piracaia, {DateTime.Now.Day} de {DateTime.Now:MMMM} de {DateTime.Now.Year}."));
                document.Add(new Paragraph("______________________________"));
                document.Add(new Paragraph("ASSINATURA DO ALUNO"));

                document.Close();
                var file = memoryStream.ToArray();
                return File(file, "application/pdf", "RequerimentoAuxilioTransporte.pdf");
            }
        }
    }
}


*/
