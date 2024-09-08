using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void GetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2011;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2011, veiculo.Ano);
    }
}