using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public UsersController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u =>
            {
                return new UserDto()
                {
                    UserName = u.UserName,
                    Token = _tokenService.CreateToken(u),
                };
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUsers(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
}
