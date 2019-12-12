using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {

        IUsersRepository Users { get; }
        IDistanceRepository Distances { get; }
        int Complete();


    }
}
