using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var app = WebApplication.Create(args);

// Stores the most recent received message
string lastMessage = "";

// Laptop or device sends a message
// Example: POST /send?msg=HelloWorld
app.MapPost("/send", (string msg) =>
{
    lastMessage = msg;
    return Results.Ok("Message saved.");
});

// Laptop or device reads the most recent message
// Example: GET /read
app.MapGet("/read", () =>
{
    if (string.IsNullOrWhiteSpace(lastMessage))
        return Results.Ok("NO_MESSAGE");

    return Results.Ok(lastMessage);
});

app.Run();
