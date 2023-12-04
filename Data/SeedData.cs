using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcCongresoTIC.Data;
using MvcCongresoTIC.Models;
using System;
using System.Linq;

namespace MvcCongresoTIC.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcCongresoTICContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcCongresoTICContext>>()))
            {                
                if (context.Conferencia.Any())
                {
                    return;
                }
                context.Conferencia.AddRange(
                    new Conferencia
                    {
                        Titulo = "Ciencia, Innovación y Tecnologías de la Información y las Comunicaciones",
                        Horario = "10:30 A.M. - 12:00 P.M.",
                        NombreConferencista = "Juan Pérez"                        
                    },
                    new Conferencia
                    {
                        Titulo = "Informática para estudiantes internacionales",
                        Horario = "07:00 A.M. - 09:30 A.M.",
                        NombreConferencista = "Jason Silva"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}