using SpaUserControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data;

namespace SpaUserControl.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository //Para implementar a interface pressionar Ctrl + .
    {
        private AppDataContext _context = new AppDataContext(); //Conexão com o banco

        public User Get(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault(); //Se o objeto(registro) não existir o resultado será nulo. Se for .First() haverá uma exceção
        }

        public void Create(User user)
        {
            _context.Users.Add(user); //Adiciona usuário somente na sessão
            _context.SaveChanges(); //Salva no banco
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified; //Avisa ao ORM (EntityFramework) que o registro/objeto foi alterado
            _context.SaveChanges(); //Salva no banco
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose(); //Garante que a conexão será fechada
        }
    }
}
