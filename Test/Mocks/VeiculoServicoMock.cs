using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Nome = "Uno Mile",
            Marca = "FIAT",
            Ano = 1997
        },
        new Veiculo{
            Id = 2,
            Nome = "Fusion",
            Marca = "FORD",
            Ano = 2013
        }
    };

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(a => a.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
        veiculo.Id = veiculos.Count() + 1;
        veiculos.Add(veiculo);

    }

    public void Apagar(Veiculo veiculo)
    {
        veiculos.Remove(veiculo);
    }

     public void Atualizar(Veiculo veiculo)
    {
        var vei = veiculos.Find(a => a.Id == veiculo.Id);
        vei.Nome = veiculo.Nome;
        vei.Marca = veiculo.Marca;
        vei.Ano = veiculo.Ano;
    }

    public List<Veiculo> Todos(int? pagina, string? marca, string? modelo)
    {
        return veiculos;
    }
}