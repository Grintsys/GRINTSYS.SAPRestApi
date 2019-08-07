using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRINTSYS.SAPRestApi.Persistance.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SAPRestApiContext _context;

        public BaseRepository(SAPRestApiContext context)
        {
            _context = context;
        }
    }
}