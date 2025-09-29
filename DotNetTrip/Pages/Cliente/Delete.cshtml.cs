using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.Cliente
{
    public class DeleteModel : PageModel
    {
        private readonly DotNetTripDbContext _context;

        public DeleteModel(DotNetTripDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DotNetTrip.Models.Cliente Cliente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                Cliente = cliente;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // ===== BUSCAR O CLIENTE (pode usar FindAsync ou FirstOrDefaultAsync) =====
            var cliente = await _context.Clientes
                .IgnoreQueryFilters() // Permite buscar mesmo que já esteja excluído
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Verificar se já está excluído
            if (cliente.IsDeleted)
            {
                TempData["ErrorMessage"] = $"O cliente '{cliente.Nome}' já foi excluído anteriormente.";
                return RedirectToPage("./Index");
            }

            // ===== EXCLUSÃO LÓGICA: Marcar como excluído em vez de remover =====
            cliente.IsDeleted = true;
            cliente.DeletedAt = DateTime.Now;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            // Log da exclusão lógica
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - EXCLUSÃO LÓGICA: Cliente '{cliente.Nome}' (ID: {cliente.Id}, CPF: {cliente.Cpf}) marcado como excluído. DeletedAt: {cliente.DeletedAt:dd/MM/yyyy HH:mm:ss}");

            TempData["SuccessMessage"] = $"Cliente '{cliente.Nome}' excluído com sucesso (exclusão lógica).";

            // ===== CÓDIGO ANTIGO (EXCLUSÃO FÍSICA) - COMENTADO =====
            // _context.Clientes.Remove(cliente);
            // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}