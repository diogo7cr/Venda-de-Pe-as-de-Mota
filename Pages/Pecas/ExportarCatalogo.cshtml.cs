using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;

namespace MotoPartsShop.Pages.Pecas
{
    [Authorize(Roles = "Admin,Gestor")]
    public class ExportarCatalogoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExportarCatalogoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Buscar todas as peças com relações
            var pecas = await _context.Pecas
                .Include(p => p.Categoria)
                .Include(p => p.Modelo)
                .OrderBy(p => p.Categoria!.Nome)
                .ThenBy(p => p.Nome)
                .ToListAsync();

            // Criar workbook
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Catálogo de Peças");

            // HEADER - Estilo
            worksheet.Range("A1:I1").Style
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Font.SetBold(true)
                .Font.SetFontColor(XLColor.White)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // HEADER - Títulos
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Referência";
            worksheet.Cell(1, 3).Value = "Nome";
            worksheet.Cell(1, 4).Value = "Descrição";
            worksheet.Cell(1, 5).Value = "Categoria";
            worksheet.Cell(1, 6).Value = "Modelo";
            worksheet.Cell(1, 7).Value = "Preço (€)";
            worksheet.Cell(1, 8).Value = "Stock";
            worksheet.Cell(1, 9).Value = "Estado";

            // DADOS
            int row = 2;
            foreach (var peca in pecas)
            {
                worksheet.Cell(row, 1).Value = peca.Id;
                worksheet.Cell(row, 2).Value = peca.Referencia;
                worksheet.Cell(row, 3).Value = peca.Nome;
                worksheet.Cell(row, 4).Value = peca.Descricao;
                worksheet.Cell(row, 5).Value = peca.Categoria?.Nome ?? "N/A";
                worksheet.Cell(row, 6).Value = peca.Modelo?.Nome ?? "N/A";
                worksheet.Cell(row, 7).Value = peca.Preco;
                worksheet.Cell(row, 8).Value = peca.Stock;
                worksheet.Cell(row, 9).Value = peca.Stock > 0 ? "Disponível" : "Esgotado";

                // Formatar preço
                worksheet.Cell(row, 7).Style.NumberFormat.Format = "€#,##0.00";

                // Colorir linha se stock baixo
                if (peca.Stock == 0)
                {
                    worksheet.Range(row, 1, row, 9).Style.Fill.SetBackgroundColor(XLColor.LightPink);
                }
                else if (peca.Stock < 10)
                {
                    worksheet.Range(row, 1, row, 9).Style.Fill.SetBackgroundColor(XLColor.LightYellow);
                }

                row++;
            }

            // ESTATÍSTICAS no final
            row += 2;
            worksheet.Cell(row, 1).Value = "ESTATÍSTICAS";
            worksheet.Cell(row, 1).Style.Font.SetBold(true);
            row++;
            worksheet.Cell(row, 1).Value = "Total de Peças:";
            worksheet.Cell(row, 2).Value = pecas.Count;
            row++;
            worksheet.Cell(row, 1).Value = "Peças em Stock:";
            worksheet.Cell(row, 2).Value = pecas.Count(p => p.Stock > 0);
            row++;
            worksheet.Cell(row, 1).Value = "Peças Esgotadas:";
            worksheet.Cell(row, 2).Value = pecas.Count(p => p.Stock == 0);
            row++;
            worksheet.Cell(row, 1).Value = "Valor Total em Stock:";
            worksheet.Cell(row, 2).Value = pecas.Sum(p => p.Preco * p.Stock);
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "€#,##0.00";

            // Ajustar larguras
            worksheet.Columns().AdjustToContents();

            // Congelar primeira linha
            worksheet.SheetView.FreezeRows(1);

            // Gerar ficheiro
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            var fileName = $"Catalogo_Pecas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}