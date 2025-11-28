using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// REQUIRED FOR RENDER
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

string filePath = "message.txt";

// POST /send?msg=Hello
app.MapPost("/send", async (string msg) =>
{
    await File.WriteAllTextAsync(filePath, msg);
    return Results.Ok("Message saved.");
});

// GET /read
app.MapGet("/read", async () =>
{
    if (!File.Exists(filePath))
        return Results.Ok("NO_MESSAGE");

    string msg = await File.ReadAllTextAsync(filePath);

    if (string.IsNullOrWhiteSpace(msg))
        return Results.Ok("NO_MESSAGE");

    return Results.Ok(msg);
});

app.Run();
