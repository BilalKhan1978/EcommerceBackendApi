using AutoMapper;
using Azure.Core;
using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceBackendApi.Services.Implementations
{
    public class UserService : IUserService
    {
        // Inject the services
        public readonly EcommerceDbContext _dbContext;
        IMapper _mapper;
        public UserService(EcommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddUser(AddUserRequestDto addUserRequestDto)
        {
            
            var emailValidation = await _dbContext.Users.Where(x => x.Email == addUserRequestDto.Email).FirstOrDefaultAsync();
            if (emailValidation != null) throw new Exception("This email has already been taken. kindly try another one");
            CreatePasswordHash(addUserRequestDto.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);
            var user = new User
            {
                Role = addUserRequestDto.Role,
                Email = addUserRequestDto.Email,
                PassHash = passwordHash,
                PassSalt = passwordSalt,
                UniqueStoreId=addUserRequestDto.UniqueStoreId,
                Password="newpassword"
            };
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GetUserRequestDto>> GetAllUsersData()
        {
            var users = await _dbContext.Users.ToListAsync();
            var allUsers = _mapper.Map<List<GetUserRequestDto>>(users);
            return allUsers;
        }

        public async Task DeleteUserById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == id);
            if (user == null) throw new Exception("No user found");
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
        public async Task<User> VerifyUser(string email, string password)
        {
            var user = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("user does not exist"); ;
            // if user is not null
            if (!VerifyPinHash(password, user.PassHash, user.PassSalt))
                throw new Exception("You have entered incorrect Password");

            // Authentication verified
            return user;

        }


        //public async Task AddUsers(List<AddUserRequestDto> addUserRequestDto)
        //{
        //    var users = _mapper.Map<List<User>>(addUserRequestDto);
        //    await _dbContext.Users.AddRangeAsync(users);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task LoginUser(LoginUserRequestDto loginUserRequestDto)
        //{
        //    throw new Exception();
        //}

        //public async Task<User> GetLoginUser()
        //{
        //    throw new NotImplementedException();
        //}

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPinHash(string Pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(Pin)) throw new Exception("Pin");
            // verify pin
            using (var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedPinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != pinHash[i]) return false;
                }
            }
            return true;
        }

    }
}
