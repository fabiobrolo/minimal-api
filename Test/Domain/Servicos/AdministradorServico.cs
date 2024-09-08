using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }


    [TestMethod]
    public void Incluir()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);

        // Assert
        Assert.AreEqual(1, administradorServico.Todos(1).Count());
    }

    [TestMethod]
    public void BuscarPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.AreEqual(1, admDoBanco?.Id);
    }
    [TestMethod]
    public void Atualizar()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "0";
        var administradorServico = new AdministradorServico(context);
        administradorServico.Incluir(adm);

        // Act
        adm.Email = "administrador@teste.com";
        adm.Senha = "123456";
        adm.Perfil = "1";
        administradorServico.Atualizar(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        //Assert
        Assert.AreEqual("administrador@teste.com",admDoBanco?.Email);
        Assert.AreEqual("123456",admDoBanco?.Senha);
        Assert.AreEqual("1",admDoBanco?.Perfil);
    }
        [TestMethod]
    public void Apagar()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "0";
        var administradorServico = new AdministradorServico(context);
        administradorServico.Incluir(adm);

        //Act      
        administradorServico.Apagar(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        //Assert
        Assert.AreEqual(null,admDoBanco?.Id);
    }
}