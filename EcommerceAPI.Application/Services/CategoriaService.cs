using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository; 
    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
        return await _categoriaRepository.ObterCategorias();
    }
    public async Task<Categoria?> ObterCategoriaPorId(int id)
    {
        var categoria = await _categoriaRepository.ObterCategoriaPorId(id);

        if (categoria is null)
        {
            throw new KeyNotFoundException("Categoria não encontrado.");
        }

        return categoria;
    }
    public async Task<Categoria> CriarCategoria(Categoria categoria)
    {
        var categoriaExistente = await _categoriaRepository.ObterCategoriaPorNome(categoria.Nome);
        if (categoriaExistente != null)
        {
            throw new InvalidOperationException("Já existe uma categoria com esse nome.");
        }

        return await _categoriaRepository.AdicionarCategoria(categoria);
    }
    public async Task<bool> EditarCategoria(Categoria categoria)
    {
        var categoriaExistente = await _categoriaRepository.ObterCategoriaPorId(categoria.Id);

        if (categoriaExistente is null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        categoriaExistente.Id = categoria.Id;
        categoriaExistente.Nome = categoria.Nome;


        return await _categoriaRepository.SalvarAlteracoesCategoria(categoriaExistente);
    }
    public async Task<bool> ExcluirCategoriaPorId(int id)
    {
        return await _categoriaRepository.RemoverCategoriaPorId(id);
    }


}
