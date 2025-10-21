using Fernanda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Prova A2");

//POST /api/consumo/cadastrar

app.MapPost("/api/consumo/cadastrar", ([FromBody] Consumo consumo, [FromServices] AppDataContext ctx) =>
{


    if (consumo.m3Consumidos <= 10)

        consumo.tarifa = 2.50;
        
    if (consumo.m3Consumidos <= 20)

        consumo.tarifa = 3.50;

    if (consumo.m3Consumidos <= 50)

        consumo.tarifa = 5.0;
    
    else 

        consumo.tarifa = 6.0;


    if (consumo.consumoFaturado < 10)
        consumo.consumoFaturado = 10;

    else
        consumo.consumoFaturado = consumo.m3Consumidos;


    consumo.valorAgua = consumo.consumoFaturado * consumo.tarifa;
    

    if (consumo.bandeira == "Verde")

        consumo.adicinionalBandeira = 0;

    if (consumo.bandeira =="Vermelha")

        consumo.adicinionalBandeira = consumo.valorAgua * .020;

    if (consumo.bandeira =="Amarela")

        consumo.adicinionalBandeira = consumo.valorAgua * .010;

    if (consumo.possuiEsgoto == true)

        consumo.taxaEsgoto = (consumo.valorAgua + consumo.adicinionalBandeira) * 0.80;

    else
        consumo.taxaEsgoto = 0;

    consumo.total = consumo.valorAgua + consumo.adicinionalBandeira + consumo.taxaEsgoto;

    ctx.Consumos.Add(consumo);
    ctx.SaveChanges();
    return Results.Created($"/consumo/{consumo.Id}", consumo);
});



//LISTAR 

app.MapGet("/api/consumo/listar", ([FromServices] AppDataContext ctx) =>
{
       return Results.Ok(ctx.Consumos.ToList());
});

//busca por CPF

app.MapGet("/api/consumo/buscar/{cpf}/{mes}/{ano}", ([FromServices] AppDataContext ctx, [FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano) =>

{
    Consumo? consumo = ctx.Consumos.FirstOrDefault(x => x.CPF == cpf && x.mes == mes && x.ano == ano);
    if (consumo is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(consumo);
});

//deletar

app.MapDelete("/api/consumo/remover/{cpf}/{mes}/{ano}",
    ([FromServices] AppDataContext ctx, [FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano) =>
{
    Consumo? resultado = ctx.Consumos.FirstOrDefault(x => x.CPF == cpf && x.mes == mes && x.ano == ano);
    if (resultado is null)
    {
        return Results.NotFound("Consumo não encotrado!");
    }
    ctx.Consumos.Remove(resultado);
    ctx.SaveChanges();

    return Results.Ok(resultado);


});

//Soma total

app.MapGet("/api/consumo/total-geral", ([FromServices] AppDataContext ctx) =>
{
    if (!ctx.Consumos.Any())
        return Results.NotFound();

    double totalGeral = ctx.Consumos.Sum(t => t.total);

        return Results.Ok(totalGeral);
});










app.Run();
