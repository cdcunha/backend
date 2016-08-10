using SpaUserControl.Domain.Models;
using System;

namespace SpaUserControl.Domain.Contracts.Repositories
{
    public interface IUserRepository : IDisposable //IDisposable é importante para poder fechar a conexão com o banco, sem precisar esperar o garbage collection
    {
        User Get(string email);
        User Get(Guid id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
    }
}
