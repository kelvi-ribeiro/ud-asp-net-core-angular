using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;
using ProAgil.WebAPI.DTOs;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IConfiguration _config;
    public readonly SignInManager<User> _signInManager;
    public readonly IMapper _mapper;

    public readonly UserManager<User> _userManager;

    public UserController(
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper
            )
    {
      _config = config;
      _userManager = userManager;
      _signInManager = signInManager;
      _mapper = mapper;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(UserDto userDto)
    {
      return Ok(userDto);
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserDto userDto)
    {
      try
      {
        var user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        var userToReturn = _mapper.Map<UserDto>(userDto);
        if (result.Succeeded)
        {
          return Created("GetUser", userToReturn);
        }

        return BadRequest(result.Errors);
      }
      catch (System.Exception)
      {
        return this
          .StatusCode(
            StatusCodes.Status500InternalServerError,
            "Banco de Dados Falhou"
            );
      }
    }
  }
}
