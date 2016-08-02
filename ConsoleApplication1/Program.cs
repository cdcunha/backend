using SpaUserControl.Domain.Models;
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

                Console.WriteLine("User....: " + user.Name);
                Console.WriteLine("E-mail..: " + user.Email);
                Console.WriteLine("Password: " + user.Password);

                Console.WriteLine("New Pass: " + user.ResetPassword() + "  (Non Encrypted)");
                Console.WriteLine("New Pass: " + user.Password);

                user.ChangeName("Charles");
                Console.WriteLine("User ren: " + user.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
