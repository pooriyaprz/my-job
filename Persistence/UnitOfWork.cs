using Core;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;


        public IUsersRepository Users { get; private set; }
        public IDistanceRepository Distances { get; private set; }
        public UnitOfWork(MyContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Distances = new DistanceRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
