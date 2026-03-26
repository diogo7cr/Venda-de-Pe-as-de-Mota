using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotoPartsShop.Models;
using System.Text.Json;

namespace MotoPartsShop.Pages
{
    public class CarrinhoModel : PageModel
    {
        public List<CarrinhoItem> Itens { get; set; } = new();
        public decimal Total => Itens.Sum(i => i.Subtotal);

        public void OnGet()
        {
            CarregarCarrinho();
        }

        public IActionResult OnPostAdicionar(int pecaId, string nome, decimal preco, string? imagemUrl)
        {
            CarregarCarrinho();
            
            var itemExistente = Itens.FirstOrDefault(i => i.PecaId == pecaId);
            if (itemExistente != null)
            {
                itemExistente.Quantidade++;
            }
            else
            {
                Itens.Add(new CarrinhoItem
                {
                    PecaId = pecaId,
                    Nome = nome,
                    Preco = preco,
                    Quantidade = 1,
                    ImagemUrl = imagemUrl
                });
            }
            
            SalvarCarrinho();
            return RedirectToPage();
        }

        public IActionResult OnPostRemover(int pecaId)
        {
            CarregarCarrinho();
            Itens.RemoveAll(i => i.PecaId == pecaId);
            SalvarCarrinho();
            return RedirectToPage();
        }

        public IActionResult OnPostAtualizarQuantidade(int pecaId, int quantidade)
        {
            CarregarCarrinho();
            var item = Itens.FirstOrDefault(i => i.PecaId == pecaId);
            if (item != null)
            {
                if (quantidade > 0)
                    item.Quantidade = quantidade;
                else
                    Itens.Remove(item);
            }
            SalvarCarrinho();
            return RedirectToPage();
        }

        public IActionResult OnPostLimpar()
        {
            HttpContext.Session.Remove("Carrinho");
            return RedirectToPage();
        }

        private void CarregarCarrinho()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                Itens = JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinhoJson) ?? new();
            }
        }

        private void SalvarCarrinho()
        {
            var carrinhoJson = JsonSerializer.Serialize(Itens);
            HttpContext.Session.SetString("Carrinho", carrinhoJson);
        }
    }
}