using System.Security.Cryptography;
using System.Text;
using Core.DTO.Contratante;
using Core.DTO.Freelancer;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class ContratanteService(IContratanteRepository contratanteRepository)
{
	public async Task<Contratante> CadastrarContratante(Contratante contratante)
	{
		contratante.Senha = GerarHashSenha(contratante.Senha);
		contratante.TipoUsuario = "C";
		return await contratanteRepository.InserirContratante(contratante);
	}

	public async Task<Contratante> BuscarContratantePorId(int id)
	{
		return await contratanteRepository.BuscarContratantePorId(id);
	}

	public async Task<FreelancerNomeTelefoneResponseDTO?> BuscarNomeTelefoneFreelancerPorId(int id)
	{
		return await contratanteRepository.BuscarNomeTelefoneFreelancerPorId(id);
	}

	public async Task<Contratante> BuscarContratantePorCPF(string cpf)
	{
		return await contratanteRepository.BuscarContratantePorCPF(cpf);
	}

	public async Task<Contratante> BuscarContratantePorEmail(string email)
	{
		return await contratanteRepository.BuscarContratantePorEmail(email);
	}

	public async Task<IEnumerable<Contratante>> ListarContratantes()
	{
		return await contratanteRepository.ListarContratantes();
	}

	public async Task<Contratante> AtualizarContratante(ContratanteUpdateDTO contratante, int id)
	{
		contratante.Senha = GerarHashSenha(contratante.Senha);
		return await contratanteRepository.EditarContratante(contratante, id);
	}

	public async Task ExcluirContratante(int id)
	{
		await contratanteRepository.ExcluirContratante(id);
	}

	protected internal string GerarHashSenha(string usuarioSenha)
	{
		using (var sha256 = SHA256.Create())
		{
			var bytesSenha = Encoding.UTF8.GetBytes(usuarioSenha);
			var bytesHashSenha = sha256.ComputeHash(bytesSenha);
			return BitConverter.ToString(bytesHashSenha).Replace("-", "").ToLower();
		}
	}
}