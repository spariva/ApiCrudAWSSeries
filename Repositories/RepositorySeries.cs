using ApiCrudAWSSeries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCrudAWSSeries.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrudAWSSeries.Repositories
{
    public class RepositorySeries
    {
        private SeriesContext context;

        public RepositorySeries(SeriesContext context)
        {
            this.context = context;
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await context.Series.ToListAsync();
        }

        public async Task<int> GetMaxId()
        {
            return await context.Series.MaxAsync(s => s.IdSerie);
        }

        public async Task<Serie> GetSerieById(int id)
        {
            return await context.Series.FirstOrDefaultAsync(s => s.IdSerie == id);
        }

        public async Task CreateSerieAsync(string nombre, string imagen, int anyo)
        {
            Serie serie = new Serie
            {
                IdSerie = await GetMaxId() + 1,
                Nombre = nombre,
                Imagen = imagen,
                Anyo = anyo
            };
            context.Series.Add(serie);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync(int id, string nombre, string imagen, int anyo)
        {
            Serie serie = await GetSerieById(id);
            if(serie != null)
            {
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Anyo = anyo;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSerieAsync(int id)
        {
            Serie serie = await GetSerieById(id);
            if(serie != null)
            {
                context.Series.Remove(serie);
                await context.SaveChangesAsync();
            }
        }

    }
}
