#region Declarations & Usings

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using PandaAssetLabelPrintingUtility_API.BAL;
using PandaAssetLabelPrintingUtility_API.Model;
using PandaAssetLabelPrintingUtility_API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#endregion

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Cross origin Access

builder.Services.AddCors(options =>
{
    options.AddPolicy("defaultcorspolicy",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

#endregion

#region NewtonSoft JSON

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

#endregion

builder.Services.AddMvc();

#region Swagger Documentation & JWT Authentication

builder.Services.AddSwaggerGen(SwaggerGen =>
{

    #region JWT Bearer Token In Swagger

    SwaggerGen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    SwaggerGen.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
               Reference = new OpenApiReference
               {
                   Type=ReferenceType.SecurityScheme,
                   Id = "Bearer"
               }
            },
            new string[]{}
        }
    });

    #endregion

    #region Swagger Documentation

    SwaggerGen.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Panda Asset Label Printing Utility API Project",
            Description = "Asset Label Printing Utility for Panda",
            Version = "v1",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Abdul Kabeer Bhatti",
                Email = "abdul.kabeer@zultec.com",
            },
        });

    SwaggerGen.ResolveConflictingActions(a => a.FirstOrDefault());

    SwaggerGen.OperationFilter<RemoveVersionFromParameter>();
    SwaggerGen.DocumentFilter<ReplaceVersionWithExactValueInPath>();

    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

    //Error throughing due to no data in xml file
    SwaggerGen.IncludeXmlComments(filePath);

    #endregion

});

#region Token Expiration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = JWTBuilder.GetValidationParameters();
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token=Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

#region Swagger URL Change

app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "Panda Asset Label Printing Utility API Project";
    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Panda Asset Label Printing Utility API Project");
    //c.SwaggerEndpoint($"/swagger/v2/swagger.json", "OTS Web API Project"); 
    c.RoutePrefix = string.Empty;

});

#endregion

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("defaultcorspolicy");

#region JWT Authentication & Authorization

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllerRoute(name:"default",pattern:"");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllers();
});

app.Run();
