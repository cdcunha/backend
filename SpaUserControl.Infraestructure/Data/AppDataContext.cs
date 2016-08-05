using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data.Map;
using System.Data.Entity;

namespace SpaUserControl.Infraestructure.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("AppConnectionString")
        {
            Configuration.ProxyCreationEnabled = false; //Se True, haverá problema para serializar para JSON
            Configuration.LazyLoadingEnabled = false; 
            /*
             * LazyLoadingEnabled = Se True, carrega os dados por partes e se houver perfis, para que os perfis sejam carregados será 
             *                      necessário executar o comando Users.Profiles
             * Obs.: Neste caso pode ser carregado tudo pois já se sabe o que precisa ser trazido e não tenho Repository Generics
             */
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * Sempre que o modelo for gerado (banco for criado) a tabela User será mapeada conforme UserMap.cs
             */
            modelBuilder.Configurations.Add(new UserMap()); //Vincula o objecto Users à tabela no banco de dados (Ver UserMap.cs)

            /*
             * Para instalar o EntityFramework:
             * 1) Abrir o menu Tools -> Nuget Package Manager -> Package Manager Console
             * 2) Escolher o projeto desejado (Default project), no caso SpaUserControl.Infraestructure 
             * 3) Digitar: Install-Package EntityFramework -IncludePrerelease 
             *    Obs.: o parâmetro -IncludePrerelease é para evitar erro no próximo comando 
             * 4) Após instalado o EntityFramework, digitar: Enable-Migrations
             *    Obs.: Toda modificação no BD será feita pelo Migrations. Executar este comando após a criação das classes de acesso ao BD e tabelas
             * 5) Abrir Configuration.cs e alterar AutomaticMigrationsEnabled = true;
             *    Caso contrário será necessário Digitar: Add-migration informarUmNomeAqui
             *    Obs.: Somente para "avisar" que será feita um Migration. First-Migration é o nome dado à Migration, pode ser qquer um
             * 6) Digitar: Update-Database
             *    Obs.: Cria as tabelas definidas nas classes. Se quiser ver os comandos SQL sendo executados, acrescente o parâmetro -verbose
             */
        }
    }
}
