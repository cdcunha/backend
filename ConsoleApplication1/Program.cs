using SpaUserControl.Domain;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Repositories;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {   
                User user = new User("Charlão", "charlao@gmail.com");
                user.SetPassword("123456", "123456");
                user.Validate();
                
                using (IUserRepository userRep = new UserRepository())
                {
                    userRep.Create(user);
                }

                using (IUserRepository userRep = new UserRepository())
                {
                    var usr = userRep.Get("charlao@gmail.com");

                    Console.WriteLine(usr.Email);
                }

                /*
                Console.WriteLine("User....: " + user.Name);
                Console.WriteLine("E-mail..: " + user.Email);
                Console.WriteLine("Password: " + user.Password);

                Console.WriteLine("New Pass: " + user.ResetPassword() + "  (Non Encrypted)");
                Console.WriteLine("New Pass: " + user.Password);

                user.ChangeName("Charles");
                Console.WriteLine("User ren: " + user.Name);
                */

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ": \n" + ex.InnerException.InnerException.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
