using ComandaEletrônica.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/listarprodutos", async () =>
{
    using SqlConnection conn = new SqlConnection(builder.Configuration["ConnectionStrings:ConnString"]);
    conn.Open();
    var Query = await conn.QueryAsync("SELECT * FROM comandaeletronica.dbo.PRODUTO ORDER BY Idproduto");


    var result = Query.ToString();
    conn.Close();


    return result.Any() == false ? Results.NotFound("Nenhum produto cadastrado.") : Results.Ok(result);
});

app.MapGet("/pegaproduto/{code}", async (string code) =>
{
    using SqlConnection conn = new SqlConnection(builder.Configuration["ConnectionStrings:ConnString"]);

    var query = await conn.QueryAsync($"select * from comandaeletronica.dbo.PRODUTO where Idproduto = {code}");

    var result = query.ToList();

    conn.Close();
    return result.Count == 0 ? Results.NotFound("Produto não encontrado.") : Results.Ok(result);
});

app.MapPost("/adicionaproduto", async (PRODUTO produto) =>
{
    using SqlConnection conn = new SqlConnection(builder.Configuration["ConnectionStrings:ConnString"]);

    var produtoadiciona = new PRODUTO(produto.Nome, produto.Quantidade, produto.Preco)
    {
        Nome = produto.Nome,
        Quantidade = produto.Quantidade,
        Preco = produto.Preco,
    };


    var query = await conn.QueryAsync($"select * from comandaeletronica.dbo.PRODUTO where Idproduto = {produto.Idproduto}");
    var validation = query.ToList();
    if (validation.Count == 0)
    {
        var result = await conn.ExecuteAsync($"INSERT INTO comandaeletronica.dbo.PRODUTO (Nome,Quantidade,Preco) VALUES ('{produtoadiciona.Nome}','{produtoadiciona.Quantidade}','{produtoadiciona.Preco}')");
        return Results.Ok("Produto adicionado com sucesso!");
    }
    conn.Close();
    return Results.BadRequest("Produto já existente.");
});

app.MapPost("/registracomanda", async (COMANDA com) =>
{
    using SqlConnection conn = new SqlConnection(builder.Configuration["ConnectionStrings:ConnString"]);

    var produtoadiciona = new COMANDA(com.Datadacompra, com.Valortotal)
    {
        Datadacompra = com.Datadacompra,
        Valortotal = com.Valortotal
    };


    var query = await conn.QueryAsync($"select * from comandaeletronica.dbo.COMANDA where Idcomanda = {com.Idcomanda}");
    var validation = query.ToList();
    if (validation.Count == 0)
    {
        var result = await conn.ExecuteAsync($"INSERT INTO comandaeletronica.dbo.COMANDA (Datadacompra,Valortotal) VALUES ('{com.Datadacompra}','{com.Valortotal}')");
        return Results.Ok("Comanda adicionada com sucesso!");
    }
    conn.Close();
    return Results.BadRequest("Comanda já existente.");
});

app.MapPost("/itemscomanda", async (ItemsComanda items) =>
{
    using SqlConnection conn = new SqlConnection(builder.Configuration["ConnectionStrings:ConnString"]);

    var query = await conn.QueryAsync($"select * from comandaeletronica.dbo.COMANDA where Idcomanda = {items.Idcomanda}");

    foreach (PRODUTO item in items.Items)
    { 
        var query2 = await conn.QueryAsync($"INSERT INTO comandaeletronica.dbo.COMANDA_PRODUTO (Idcomanda,Idproduto,Datacompra)" +
            $" VALUES ('{items.Idcomanda}','{item.Idproduto}','{items.Datacompra}')");
    }

    conn.Close();
    //return Results.BadRequest("Comanda já existente.");
});

app.Run();
