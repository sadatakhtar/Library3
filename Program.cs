using FluentValidation;
using library_v3.Data;
using library_v3.Features.Authors.CreateAuthor;
using library_v3.Features.Authors.DeleteAuthor;
using library_v3.Features.Authors.GetAllAuthors;
using library_v3.Features.Authors.GetAuthorById;
using library_v3.Features.Authors.GetAuthorDetails;
using library_v3.Features.Authors.GetAuthorExists;
using library_v3.Features.Authors.GetAuthorStats;
using library_v3.Features.Authors.GetAuthorWithBooks;
using library_v3.Features.Authors.GetSearchAuthor;
using library_v3.Features.Authors.GetSortedAuthors;
using library_v3.Features.Authors.UpdateAuthor;
using library_v3.Features.Books.CreateBook;
using library_v3.Features.Books.DeleteBook;
using library_v3.Features.Books.GetAllBooks;
using library_v3.Features.Books.GetBookById;
using library_v3.Features.Books.UpdateBook;
using library_v3.Providers;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// MediatR
builder.Services.AddMediatR(
    (
        cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
        }
    )
);

// Serialization Fix
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Fluent Validator
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Author Providers
builder.Services.AddScoped<IBookProvider, BookProvider>();
builder.Services.AddScoped<IAuthorProvider, AuthorProvider>();
builder.Services.AddScoped<GetAuthorDetailsHandler>();
builder.Services.AddScoped<GetAuthorWithBooksHandler>();
builder.Services.AddScoped<GetAuthorExistsHandler>();
builder.Services.AddScoped<GetSortedAuthorsHandler>();
builder.Services.AddScoped<GetAuthorStatsHandler>();
builder.Services.AddScoped<GetSearchAuthorHandler>();
builder.Services.AddScoped<GetAllAuthorsHandler>();
builder.Services.AddScoped<GetAuthorByIdHandler>();
builder.Services.AddScoped<CreateAuthorHandler>();
builder.Services.AddScoped<UpdateAuthorHandler>();
builder.Services.AddScoped<DeleteAuthorHandler>();

// Book Providers
builder.Services.AddScoped<GetAllBooksHandler>();
builder.Services.AddScoped<GetBookByIdHandler>();
builder.Services.AddScoped<CreateBookHandler>();
builder.Services.AddScoped<UpdateBookHandler>();
builder.Services.AddScoped<DeleteBookHandler>();

builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
