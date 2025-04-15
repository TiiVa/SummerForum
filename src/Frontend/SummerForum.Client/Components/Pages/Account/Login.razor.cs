using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SummerForum.Client.Services;
using SummerForum.Client.Services.ServiceInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Components.Pages.Account;

public partial class Login
{
	[CascadingParameter] public HttpContext? HttpContext { get; set; }

	[Inject] private IUserService UserService { get; set; }

	[SupplyParameterFromForm] public UserDto UserDto { get; set; } = new();
	private List<UserDto> Users { get; set; } = new();
	private string? errorMessage;

	private async Task Authenticate()
	{
		var users = await UserService.GetManyAsync();

		Users = users.ToList();

		var userAccount = Users.FirstOrDefault(u => u.UserName == UserDto.UserName);

		if (userAccount is null || userAccount.Password != UserDto.Password)
		{
			errorMessage = "Invalid username or password";
			return;
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, UserDto.UserName),
			new Claim(ClaimTypes.Role, userAccount.Role)
		};

		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);
		await HttpContext.SignInAsync(principal);
		navigationManager.NavigateTo("/");
	}
}