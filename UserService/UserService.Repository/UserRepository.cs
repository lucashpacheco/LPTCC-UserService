using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Peek.Framework.Common.Errors;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Commands;
using Peek.Framework.UserService.Consults;
using Peek.Framework.UserService.Domain;
using UserService.Repository.Queries;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private string connectionSql;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserRepository(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IConfiguration _config)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            connectionSql = _config.GetConnectionString("cnSql");

        }

        #region Commands
        public async Task<bool> Create(CreateUserCommand createUserCommand)
        {
            var identityUser = new IdentityUser
            {
                Email = createUserCommand.Email,
                EmailConfirmed = true,
                UserName = createUserCommand.Email
            };

            var user = new User(identityUser.Id, createUserCommand.Name, identityUser.Email, createUserCommand.BirthDate, createUserCommand.ProfilePhoto);

            var identityResult = await userManager.CreateAsync(identityUser, createUserCommand.Password);

            if (!identityResult.Succeeded)
            {
                return identityResult.Succeeded;
            }

            var result = await CreateAsync(user);

            if (!result)
            {
                await userManager.DeleteAsync(identityUser);
                return result;
            }

            return result;
        }

        public async Task<bool> Create(FollowCommand followCommand)
        {

            return await CreateAsync(followCommand);
        }

        public async Task<bool> Delete(UnfollowCommand followCommand)
        {

            return await DeleteAsync(followCommand);
        }

        public async Task<SignInResult> Login(LoginCommand loginCommand)
        {
            var result = await signInManager.PasswordSignInAsync(loginCommand.Email, loginCommand.Password, false, false);
            return result;
        }

        #endregion

        #region Consults

        public async Task<IdentityUser> FindIdentityUserByEmail(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<User> FindById(GetUserByIdRequest user)
        {
            var result = await FindUserById(user.UserId);
            return result;
        }

        public async Task<PagedResult<User>> Find(GetUsersRequest filters)
        {
            var users = await FindUsers(filters);
            var totalItems = await CountUsers();
            return new PagedResult<User>()
            {
                Result = users,
                TotalItems = totalItems
            };
        }

        public async Task<PagedResult<User>> Find(GetFollowedUsersRequest filters)
        {
            var users = await FindUsers(filters);
            var totalItems = await CountFollowedUsers(filters.UserId);
            return new PagedResult<User>()
            {
                Result = users,
                TotalItems = totalItems
            };
        }

        #endregion
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

        public async Task<ResponseBase<string>> RefreshToken(string authToken)
        {
            var response = new ResponseBase<string>(success: true, errors: new List<string>(), data: null);
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
                user = await FindIdentityUserByEmail(email);

                response.Data = await GenerateJwt(user);

                return response;
            }
            catch (Exception ex)
            {
                var error = new UnauthorizedError();
                response.Errors.Add($"{error.Code}:{error.Message.ToString()}-{error.Level}");
                return response;
            }
        }

        #region  Private
        private async Task<IdentityUser> FindIdentityUserById(Guid userId)
        {
            var result = await userManager.FindByIdAsync(userId.ToString());
            return result;
        }

        private async Task<User> FindUserById(Guid? userId)
        {
            User user;
            using (var sqlConnection = new SqlConnection(connectionSql))
            {
                sqlConnection.Open();

                var parameters = new
                {
                    Id = userId.ToString()
                };

                user = await sqlConnection.QueryFirstAsync<User>(UserQueries.Consults.FindUserById, parameters);

                sqlConnection.Close();
            }
            return user;
        }

        private async Task<List<User>> FindUsers(GetUsersRequest filters)
        {
            IEnumerable<User> users;
            using (var sqlConnection = new SqlConnection(connectionSql))
            {
                sqlConnection.Open();

                if (filters.UsersIds != null && filters.UsersIds.Count > 1)
                {
                    var parameters = new
                    {
                        Offset = filters.PageInformation.Offset,
                        PageSize = filters.PageInformation.PageSize,
                        Ids = filters.UsersIds.ToArray()
                    };

                    users = await sqlConnection.QueryAsync<User>(UserQueries.Consults.FindUsersBatch, parameters);
                }
                else
                {
                    var parameters = new
                    {
                        Offset = filters.PageInformation.Offset,
                        PageSize = filters.PageInformation.PageSize
                    };

                    users = await sqlConnection.QueryAsync<User>(UserQueries.Consults.FindUsers, parameters);
                }

                sqlConnection.Close();
            }
            return users.ToList();
        }

        private async Task<List<User>> FindUsers(GetFollowedUsersRequest filters)
        {
            IEnumerable<User> users;
            using (var sqlConnection = new SqlConnection(connectionSql))
            {
                sqlConnection.Open();

                var parameters = new
                {
                    Id = filters.UserId.ToString(),
                    Offset = filters.PageInformation.Offset,
                    PageSize = filters.PageInformation.PageSize
                };

                users = await sqlConnection.QueryAsync<User>(UserQueries.Consults.FindFollowedUsers, parameters);

                sqlConnection.Close();
            }
            return users.ToList();
        }

        private async Task<int> CountUsers()
        {
            int count;
            using (var sqlConnection = new SqlConnection(connectionSql))
            {
                sqlConnection.Open();

                count = await sqlConnection.QueryFirstAsync<int>(UserQueries.Consults.CountUsers);

                sqlConnection.Close();
            }
            return count;
        }

        private async Task<int> CountFollowedUsers(Guid? userId)
        {
            int count;
            using (var sqlConnection = new SqlConnection(connectionSql))
            {
                sqlConnection.Open();

                var parameters = new
                {
                    Id = userId.ToString(),
                };

                count = await sqlConnection.QueryFirstAsync<int>(UserQueries.Consults.CountFollowedUsers, parameters);

                sqlConnection.Close();
            }
            return count;
        }

        private async Task<bool> CreateAsync(User user)
        {
            try
            {
                int rowsAffected = 0;
                using (var sqlConnection = new SqlConnection(connectionSql))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandText = UserQueries.Commands.CreateUserCommand;
                        command.Parameters.Add(new SqlParameter("@Id", user.Id));
                        command.Parameters.Add(new SqlParameter("@Name", user.Name));
                        command.Parameters.Add(new SqlParameter("@Birthdate", user.BirthDate));
                        command.Parameters.Add(new SqlParameter("@ProfilePhoto", user.ProfilePhoto));

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    sqlConnection.Close();
                }

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<bool> CreateAsync(FollowCommand followCommand)
        {
            try
            {
                int rowsAffected = 0;
                using (var sqlConnection = new SqlConnection(connectionSql))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandText = UserQueries.Commands.FollowCommand;
                        command.Parameters.Add(new SqlParameter("@UserId", followCommand.UserId.ToString()));
                        command.Parameters.Add(new SqlParameter("@FollowedUserId", followCommand.FollowedUserId.ToString()));
                        command.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.UtcNow));

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    sqlConnection.Close();
                }

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteAsync(UnfollowCommand followCommand)
        {
            try
            {
                int rowsAffected = 0;
                using (var sqlConnection = new SqlConnection(connectionSql))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandText = UserQueries.Commands.UnfollowCommand;
                        command.Parameters.Add(new SqlParameter("@UserId", followCommand.UserId.ToString()));
                        command.Parameters.Add(new SqlParameter("@FollowedUserId", followCommand.FollowedUserId.ToString()));

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    sqlConnection.Close();
                }

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        #endregion

    }
}
