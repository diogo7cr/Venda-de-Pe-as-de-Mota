using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;

namespace MotoPartsShop.Pages.Admin
{
    [Authorize(Roles = "Admin,Gestor")]
    public class ExportarRelatorioModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExportarRelatorioModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(DateTime? dataInicio, DateTime? dataFim)
        {
            // Datas padrão: últimos 30 dias
            var inicio = dataInicio ?? DateTime.Now.AddDays(-30);
            var fim = dataFim ?? DateTime.Now;

            // Buscar pedidos no período
            var pedidos = await _context.Pedidos
                .Where(p => p.DataPedido >= inicio && p.DataPedido <= fim)
                .OrderBy(p => p.DataPedido)
                .ToListAsync();

            // Buscar itens dos pedidos
            var pedidoIds = pedidos.Select(p => p.Id).ToList();
            var itens = await _context.PedidoItens
                .Where(pi => pedidoIds.Contains(pi.PedidoId))
                .ToListAsync();

            // Criar workbook
            using var workbook = new XLWorkbook();

            // ============================================
            // SHEET 1: RESUMO
            // ============================================
            var sheetResumo = workbook.Worksheets.Add("Resumo");

            // Título
            sheetResumo.Cell(1, 1).Value = "RELATÓRIO DE VENDAS";
            sheetResumo.Range(1, 1, 1, 4).Merge();
            sheetResumo.Cell(1, 1).Style
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Font.SetBold(true)
                .Font.SetFontSize(16)
                .Font.SetFontColor(XLColor.White)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Período
            sheetResumo.Cell(2, 1).Value = "Período:";
            sheetResumo.Cell(2, 2).Value = $"{inicio:dd/MM/yyyy} a {fim:dd/MM/yyyy}";
            sheetResumo.Cell(2, 1).Style.Font.SetBold(true);

            // Estatísticas
            int row = 4;
            sheetResumo.Cell(row, 1).Value = "ESTATÍSTICAS GERAIS";
            sheetResumo.Cell(row, 1).Style.Font.SetBold(true).Font.SetFontSize(14);
            row += 2;

            sheetResumo.Cell(row, 1).Value = "Total de Pedidos:";
            sheetResumo.Cell(row, 2).Value = pedidos.Count;
            row++;

            sheetResumo.Cell(row, 1).Value = "Pedidos Entregues:";
            sheetResumo.Cell(row, 2).Value = pedidos.Count(p => p.Estado == "Entregue");
            row++;

            sheetResumo.Cell(row, 1).Value = "Pedidos Pendentes:";
            sheetResumo.Cell(row, 2).Value = pedidos.Count(p => p.Estado == "Pendente" || p.Estado == "Processando");
            row++;

            sheetResumo.Cell(row, 1).Value = "Pedidos Cancelados:";
            sheetResumo.Cell(row, 2).Value = pedidos.Count(p => p.Estado == "Cancelado");
            row += 2;

            sheetResumo.Cell(row, 1).Value = "RECEITAS";
            sheetResumo.Cell(row, 1).Style.Font.SetBold(true).Font.SetFontSize(14);
            row += 2;

            var receitaTotal = pedidos.Where(p => p.Estado != "Cancelado").Sum(p => p.Total);
            var receitaMedia = pedidos.Count > 0 ? receitaTotal / pedidos.Count : 0;

            sheetResumo.Cell(row, 1).Value = "Receita Total:";
            sheetResumo.Cell(row, 2).Value = receitaTotal;
            sheetResumo.Cell(row, 2).Style.NumberFormat.Format = "€#,##0.00";
            sheetResumo.Cell(row, 2).Style.Font.SetBold(true).Font.SetFontColor(XLColor.Green);
            row++;

            sheetResumo.Cell(row, 1).Value = "Ticket Médio:";
            sheetResumo.Cell(row, 2).Value = receitaMedia;
            sheetResumo.Cell(row, 2).Style.NumberFormat.Format = "€#,##0.00";
            row++;

            // Ajustar colunas
            sheetResumo.Columns().AdjustToContents();

            // ============================================
            // SHEET 2: PEDIDOS DETALHADOS
            // ============================================
            var sheetPedidos = workbook.Worksheets.Add("Pedidos");

            // Header
            sheetPedidos.Range("A1:H1").Style
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Font.SetBold(true)
                .Font.SetFontColor(XLColor.White)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheetPedidos.Cell(1, 1).Value = "ID";
            sheetPedidos.Cell(1, 2).Value = "Data";
            sheetPedidos.Cell(1, 3).Value = "Cliente";
            sheetPedidos.Cell(1, 4).Value = "Email";
            sheetPedidos.Cell(1, 5).Value = "Telefone";
            sheetPedidos.Cell(1, 6).Value = "Total (€)";
            sheetPedidos.Cell(1, 7).Value = "Estado";
            sheetPedidos.Cell(1, 8).Value = "Cidade";

            // Dados
            row = 2;
            foreach (var pedido in pedidos)
            {
                sheetPedidos.Cell(row, 1).Value = pedido.Id;
                sheetPedidos.Cell(row, 2).Value = pedido.DataPedido.ToString("dd/MM/yyyy HH:mm");
                sheetPedidos.Cell(row, 3).Value = pedido.NomeCliente;
                sheetPedidos.Cell(row, 4).Value = pedido.Email;
                sheetPedidos.Cell(row, 5).Value = pedido.Telefone;
                sheetPedidos.Cell(row, 6).Value = pedido.Total;
                sheetPedidos.Cell(row, 7).Value = pedido.Estado;
                sheetPedidos.Cell(row, 8).Value = pedido.Cidade;

                sheetPedidos.Cell(row, 6).Style.NumberFormat.Format = "€#,##0.00";

                // Colorir por estado
                var estadoCell = sheetPedidos.Cell(row, 7);
                switch (pedido.Estado)
                {
                    case "Entregue":
                        estadoCell.Style.Fill.SetBackgroundColor(XLColor.LightGreen);
                        break;
                    case "Cancelado":
                        estadoCell.Style.Fill.SetBackgroundColor(XLColor.LightPink);
                        break;
                    case "Pendente":
                        estadoCell.Style.Fill.SetBackgroundColor(XLColor.LightYellow);
                        break;
                }

                row++;
            }

            sheetPedidos.Columns().AdjustToContents();
            sheetPedidos.SheetView.FreezeRows(1);

            // ============================================
            // SHEET 3: PRODUTOS MAIS VENDIDOS
            // ============================================
            var sheetProdutos = workbook.Worksheets.Add("Produtos Vendidos");

            // Header
            sheetProdutos.Range("A1:D1").Style
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Font.SetBold(true)
                .Font.SetFontColor(XLColor.White)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheetProdutos.Cell(1, 1).Value = "Produto";
            sheetProdutos.Cell(1, 2).Value = "Quantidade Vendida";
            sheetProdutos.Cell(1, 3).Value = "Receita Total (€)";
            sheetProdutos.Cell(1, 4).Value = "Preço Médio (€)";

            // Agrupar produtos
            var produtosVendidos = itens
                .GroupBy(i => i.NomePeca)
                .Select(g => new
                {
                    Nome = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    Receita = g.Sum(i => i.PrecoUnitario * i.Quantidade),
                    PrecoMedio = g.Average(i => i.PrecoUnitario)
                })
                .OrderByDescending(p => p.Quantidade)
                .ToList();

            row = 2;
            foreach (var produto in produtosVendidos)
            {
                sheetProdutos.Cell(row, 1).Value = produto.Nome;
                sheetProdutos.Cell(row, 2).Value = produto.Quantidade;
                sheetProdutos.Cell(row, 3).Value = produto.Receita;
                sheetProdutos.Cell(row, 4).Value = produto.PrecoMedio;

                sheetProdutos.Cell(row, 3).Style.NumberFormat.Format = "€#,##0.00";
                sheetProdutos.Cell(row, 4).Style.NumberFormat.Format = "€#,##0.00";

                row++;
            }

            sheetProdutos.Columns().AdjustToContents();
            sheetProdutos.SheetView.FreezeRows(1);

            // ============================================
            // SHEET 4: VENDAS POR DIA
            // ============================================
            var sheetDiario = workbook.Worksheets.Add("Vendas por Dia");

            // Header
            sheetDiario.Range("A1:C1").Style
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Font.SetBold(true)
                .Font.SetFontColor(XLColor.White)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheetDiario.Cell(1, 1).Value = "Data";
            sheetDiario.Cell(1, 2).Value = "Número de Pedidos";
            sheetDiario.Cell(1, 3).Value = "Receita (€)";

            // Agrupar por dia
            var vendasDiarias = pedidos
                .Where(p => p.Estado != "Cancelado")
                .GroupBy(p => p.DataPedido.Date)
                .Select(g => new
                {
                    Data = g.Key,
                    Pedidos = g.Count(),
                    Receita = g.Sum(p => p.Total)
                })
                .OrderBy(v => v.Data)
                .ToList();

            row = 2;
            foreach (var dia in vendasDiarias)
            {
                sheetDiario.Cell(row, 1).Value = dia.Data.ToString("dd/MM/yyyy");
                sheetDiario.Cell(row, 2).Value = dia.Pedidos;
                sheetDiario.Cell(row, 3).Value = dia.Receita;

                sheetDiario.Cell(row, 3).Style.NumberFormat.Format = "€#,##0.00";

                row++;
            }

            sheetDiario.Columns().AdjustToContents();
            sheetDiario.SheetView.FreezeRows(1);

            // Gerar ficheiro
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            var fileName = $"Relatorio_Vendas_{inicio:yyyyMMdd}_a_{fim:yyyyMMdd}.xlsx";
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}