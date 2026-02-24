using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Asegura que |DataDirectory| apunte a la carpeta App_Data dentro del proyecto
AppDomain.CurrentDomain.SetData("DataDirectory",
    Path.Combine(builder.Environment.ContentRootPath, "App_Data"));

builder.Services.AddRazorPages();

var app = builder.Build();
// ...