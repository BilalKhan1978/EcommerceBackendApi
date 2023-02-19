using AutoMapper;
using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;

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
            var user =  _mapper.Map<User>(addUserRequestDto);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUsers(List<AddUserRequestDto> addUserRequestDto)
        {
            var users = _mapper.Map<List<User>>(addUserRequestDto);
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LoginUser(LoginUserRequestDto loginUserRequestDto)
        {
            throw new Exception();
        }
       
        public async Task<User> GetLoginUser()
        {
            throw new NotImplementedException();
        }

        
    }
}
