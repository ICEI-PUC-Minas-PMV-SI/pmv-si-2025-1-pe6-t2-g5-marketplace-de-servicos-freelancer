using System.Security.Cryptography;
using System.Text;
using Core.DTO.Freelancer;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class FreelancerService(IFreelancerRepository freelancerRepository)
{
	public async Task<Freelancer> CadastrarFreelancer(Freelancer freelancer)
	{
		freelancer.Senha = GerarHashSenha(freelancer.Senha);
		freelancer.TipoUsuario = "F";
		return await freelancerRepository.InserirFreelancer(freelancer);
	}

	public async Task<Freelancer> BuscarFreelancerPorId(long id)
	{
		return await freelancerRepository.BuscarFreelancerPorId(id);
	}

	public async Task<Freelancer> BuscarFreelancerPorEmail(string email)
	{
		return await freelancerRepository.BuscarFreelancerPorEmail(email);
	}

	public async Task<IEnumerable<Freelancer>> ListarFreelancers()
	{
		return await freelancerRepository.ListarFreelancers();
	}

	public async Task<Freelancer> EditarFreelancer(FreelancerUpdateDTO freelancer, int id)
	{
		freelancer.Senha = GerarHashSenha(freelancer.Senha);
		return await freelancerRepository.EditarFreelancer(freelancer, id);
	}

	public async Task ExcluirFreelancer(int id)
	{
		await freelancerRepository.ExcluirFreelancer(id);
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