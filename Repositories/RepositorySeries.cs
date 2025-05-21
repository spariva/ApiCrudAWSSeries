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



    }
}
