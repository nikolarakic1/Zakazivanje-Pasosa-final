using Microsoft.EntityFrameworkCore;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPodaci.Kontekst;
using ZakazivanjeTerminaPodaci.Repozitorijum;
using ZakazivanjeTerminaServisniSloj.Interfejsi;
using ZakazivanjeTerminaServisniSloj.Servisi;
using ZakazivanjeTerminaServisniSloj.CuvanjePromena;
using ZakazivanjeTerminaPoslovnaLogika.Pravila;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repozitorijumi
builder.Services.AddScoped<IVrstaDokumentaRepo, VrstaDokumentaRepo>();
builder.Services.AddScoped<IDokumentacijaRepo, DokumentacijaRepo>();
builder.Services.AddScoped<IPodnosilacZahtevaRepo, PodnosilacZahtevaRepo>();
builder.Services.AddScoped<IZahtevZaPasosRepo, ZahtevZaPasosRepo>();

// Cuvanje promena / UnitOfWork
builder.Services.AddScoped<ICuvanjePromena, CuvanjeSaveChanges>();
builder.Services.AddScoped<IZahtevZaPasosServis, ZahtevZaPasosServis>();
builder.Services.AddScoped<IPodnosilacZahtevaServis, PodnosilacZahtevaServis>();
builder.Services.AddScoped<IDokumentacijaServis, DokumentacijaServis>();
builder.Services.AddScoped<ILoginServis, LoginServis>();

// Servisi
builder.Services.AddScoped<IVrstaDokumentaServis, VrstaDokumentaServis>();
builder.Services.AddScoped<IPraviloObradeZahteva, PraviloObradeZahteva>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();