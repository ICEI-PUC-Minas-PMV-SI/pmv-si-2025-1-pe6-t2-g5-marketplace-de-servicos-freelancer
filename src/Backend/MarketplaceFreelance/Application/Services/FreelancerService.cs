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
		return await freelancerRepository.EditarFreelancer(freelancer, id);
	}

	public async Task ExcluirFreelancer(int id)
	{
		await freelancerRepository.ExcluirFreelancer(id);
	}

    protected internal string GerarHashSenha(string usuarioSenha)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytesSenha = Encoding.UTF8.GetBytes(usuarioSenha);
			byte[] bytesHashSenha = sha256.ComputeHash(bytesSenha);
			return BitConverter.ToString(bytesHashSenha).Replace("-", "").ToLower();
		}
	}
}