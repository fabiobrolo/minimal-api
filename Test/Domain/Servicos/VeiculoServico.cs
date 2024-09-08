using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicoTest
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
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "FIT";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2009;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }

    [TestMethod]
    public void BuscarPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");
        var veiculo = new Veiculo();
        veiculo.Nome = "FIT";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2009;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual(1, veiculoDoBanco?.Id);
    }

    [TestMethod]
    public void Atualizar()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");
        var veiculo = new Veiculo();
        veiculo.Nome = "FIT";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2009;
        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        // Act
        veiculo.Nome = "Fusca";
        veiculo.Marca = "VW";
        veiculo.Ano = 1981;
        veiculoServico.Atualizar(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual("Fusca", veiculoDoBanco?.Nome);
        Assert.AreEqual("VW", veiculoDoBanco?.Marca);
        Assert.AreEqual(1981, veiculoDoBanco?.Ano);    
        }

    [TestMethod]
    public void Apagar()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");
        var veiculo = new Veiculo();
        veiculo.Nome = "FIT";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2009;
        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        //Act      
        veiculoServico.Apagar(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        //Assert
        Assert.AreEqual(null,veiculoDoBanco?.Id);
        }
}