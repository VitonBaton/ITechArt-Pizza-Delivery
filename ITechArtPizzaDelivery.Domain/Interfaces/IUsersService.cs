using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IUsersService
    {
        Task Register(User user, string password);
        Task<string> Login(string username, string password, string secretKey);
    }
}