using Microsoft.EntityFrameworkCore;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPodaci.Kontekst;

namespace ZakazivanjeTerminaPodaci.Repozitorijum;

public class VrstaDokumentaRepo : IVrstaDokumentaRepo
{
    private readonly AppDbContext _context;

    public VrstaDokumentaRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task Dodaj(VrstaDokumenta dokument)
    {
        if (dokument is null)
        {
            throw new ArgumentException("Dodavanje nije moguce");
        }

        await _context.VrstaDokumenata.AddAsync(dokument);
    }

    public async Task<VrstaDokumenta?> Izmeni(VrstaDokumenta dokument, int id)
    {
        var stariDokument = await _context.VrstaDokumenata
            .FirstOrDefaultAsync(x => x.Id == id);

        if (stariDokument is null)
        {
            return null;
        }

        stariDokument.Naziv = dokument.Naziv;
        stariDokument.Opis = dokument.Opis;
        stariDokument.Obavezno = dokument.Obavezno;
        stariDokument.Dokumentacija = dokument.Dokumentacija;
        stariDokument.DatumIzmene = DateTime.Now;

        return stariDokument;
    }

    public async Task<bool> Obrisi(int id)
    {
        var dokument = await _context.VrstaDokumenata
            .FirstOrDefaultAsync(x => x.Id == id);

        if (dokument is null)
        {
            return false;
        }

        _context.VrstaDokumenata.Remove(dokument);
        return true;
    }

    public async Task<VrstaDokumenta?> VratiPoIdu(int id)
    {
        return await _context.VrstaDokumenata
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<VrstaDokumenta>> VratiSve()
    {
        return await _context.VrstaDokumenata
            .ToListAsync();
    }
}