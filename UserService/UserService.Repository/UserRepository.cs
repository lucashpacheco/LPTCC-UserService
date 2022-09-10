using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserService.Model.Commands;
using UserService.Model.Domain;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public UserRepository(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<IdentityResult> Create(CreateUserCommand createUserCommand)
        {
            var user = new IdentityUser
            {
                Email = createUserCommand.Email,
                EmailConfirmed = true,
                UserName = createUserCommand.Email
            };

            var result = await userManager.CreateAsync(user, createUserCommand.Password);
            
            return result;
        }
        public async Task<SignInResult> Login(LoginCommand loginCommand)
        {
            var result = await signInManager.PasswordSignInAsync(loginCommand.Email, loginCommand.Password, false, false);
            return result;
        }

        public async Task<IdentityUser> FindByEmail(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<string> GenerateJwt(IdentityUser user)
        {
            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            //IList<string> userRoles = await ConsultarRoles(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            //foreach (string item in userRoles)
            //{
            //    claims.Add(new Claim("role", item));
            //}

            ClaimsIdentity identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("12312903892013210938");

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                //Issuer = Configuracao.Issuer,
                //Audience = Configuracao.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);          
        }

        public async Task<string> RefreshToken(string authToken)
        {
            var token = "X";
            if (authToken != null)
            {
                token = authToken.Replace("Bearer ", "").Replace("bearer ", "");
            }
            else
            {
                throw new Exception();
            }
            IdentityUser user;

            // byte[] key = Encoding.ASCII.GetBytes(Crosscuting.Identity.Configuracao.Secret);
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("12312903892013210938"));
            //var myIssuer = Configuracao.Issuer;
            //var myAudience = Configuracao.Audience;
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = myIssuer,
                    //ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                var email = ((JwtSecurityToken)validatedToken).Payload.Where(m => m.Key == "email").FirstOrDefault().Value.ToString();
                user = await FindByEmail(email);

                var response = await GenerateJwt(user);

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //private LoginDetailResponse BootstrapResult()
        //{
        //    LoginDetailResponse response = new LoginDetailResponse
        //    {
        //        AccessToken = encodedToken,
        //        ExpiraEm = TimeSpan.FromHours(Configuracao.ExpiracaoHoras).TotalSeconds,
        //        Claims = claims.Select(c => new ClaimResponse { Type = c.Type, Value = c.Value }),
        //        Email = user.Email,
        //        Id = user.Id,
        //        DataExpiracao = DateTime.UtcNow.AddHours(Configuracao.ExpiracaoHoras),
        //    };

        //    return response;
        //}

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
