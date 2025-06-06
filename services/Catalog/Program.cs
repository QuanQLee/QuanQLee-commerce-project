using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Catalog.Api.Infrastructure; // ��� DbContext ���������ռ�
using System;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Configuration;

// ���� using ...


var builder = WebApplication.CreateBuilder(args);

// ǿ�Ƽ��� 0.0.0.0:80 �˿ڣ����� Docker
builder.WebHost.UseUrls("http://0.0.0.0:80");

// ...���±��ֲ���
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.EnableAnnotations();
    o.SwaggerDoc("v1", new() { Title = "Catalog API", Version = "v1" });
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("CatalogDb");
    options.UseNpgsql(cs);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
