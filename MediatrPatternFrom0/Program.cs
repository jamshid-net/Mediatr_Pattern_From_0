using MediatrPatternFrom0.RepositoryPattern;
using Pattern;

using System.Reflection;

namespace MediatrPatternFrom0;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        

        builder.Services.AddCustomMediatrSingleton(Assembly.GetExecutingAssembly());

        builder.Services.AddSingleton<IRepository, Repository>();

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

       
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
