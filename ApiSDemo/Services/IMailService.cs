using System.Threading.Tasks;

namespace ApiSDemo.Services
{
	public interface IMailService
	{
		Task SendEmailAsync(MailRequest mailRequest);
	}
}
