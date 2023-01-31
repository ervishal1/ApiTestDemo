using System.ComponentModel.DataAnnotations;

namespace ApiSDemo.Models.ViewModels
{
	public class ChangePasswordVm
	{
		public string OldPassword { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirmation Password is required.")]
		[Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password must match.")]
		public string ConfirmPassword { get; set; }
	}
}
