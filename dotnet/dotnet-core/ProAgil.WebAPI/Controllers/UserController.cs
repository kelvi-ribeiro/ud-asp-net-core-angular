using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
      try
      {
          var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
          var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
          if(result.Succeeded)
          {
            var appUser = await _userManager.Users
              .FirstOrDefaultAsync(u => u.NormalizedUserName == userLoginDto.UserName.ToUpper());
              var userToReturn = _mapper.Map<UserLoginDto>(appUser);
              return Ok(new {
                token = GenerateJWToken(appUser).Result,
                user = userToReturn
              });
          }
          return Unauthorized();
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

    private async Task<string> GenerateJWToken(User user)
    {
      
    }
  }
}
