using Autofac.Extensions.DependencyInjection;
using PollBack.Web.IoC;
using PollBack.Web.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.RegisterFluentValidation();
builder.RegisterSwagger();

builder.RegisterCors();
builder.RegisterDbContexts();

builder.RegisterConfiguration();
builder.Services.AddHttpContextAccessor();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.RegisterAutofac();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();
