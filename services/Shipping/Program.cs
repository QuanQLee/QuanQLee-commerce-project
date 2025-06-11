using Microsoft.EntityFrameworkCore;
using Shipping.Api.Infrastructure;
using MassTransit;
using Hangfire;
// �� �����У������ڴ�洢��
using Hangfire.MemoryStorage;                     // <-- �޸�
// using Hangfire.PostgreSql;                    // �� ��һ�п���ɾ����ע��
using Shipping.Api.Jobs;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.EnableAnnotations();
    o.SwaggerDoc("v1", new() { Title = "Shipping API", Version = "v1" });
});

// �� �ĳ� In-Memory DB
builder.Services.AddDbContext<ShippingDbContext>(options =>
{
    options.UseInMemoryDatabase("ShippingTest");  // <-- �޸�
});

// �� Hangfire Ҳ�����ڴ�洢
builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();                    // <-- �޸�
});
builder.Services.AddHangfireServer();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
});

// schedule recurring job
builder.Services.AddScoped<CheckPendingShipmentsJob>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseHangfireDashboard();
app.MapControllers();

// �� ��ʱ�����ճ�����
RecurringJob.AddOrUpdate<CheckPendingShipmentsJob>(
    "check-pending",
    job => job.Run(),
    Cron.Minutely);

app.Run();
