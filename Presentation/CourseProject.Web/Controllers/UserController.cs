using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Commands;
using CourseProject.Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseProject.Web.Controllers
{
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
        {
            var users = await _mediator.Send(new GetUsersQuery(page, pageSize, name));

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tariffPlan = await _mediator.Send(new GetTariffPlanByIdQuery(id));

            if (tariffPlan is null)
            {
                return NotFound($"TariffPlan with id {id} is not found.");
            }

            return Ok(tariffPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TariffPlanForCreationDto? tariffPlan)
        {
            if (tariffPlan is null)
            {
                return BadRequest("Object for creation is null");
            }

            await _mediator.Send(new CreateTariffPlanCommand(tariffPlan));

            return CreatedAtAction(nameof(Create), tariffPlan);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TariffPlanForUpdateDto? tariffPlan)
        {
            if (tariffPlan is null)
            {
                return BadRequest("Object for update is null");
            }

            var isEntityFound = await _mediator.Send(new UpdateTariffPlanCommand(tariffPlan));

            if (!isEntityFound)
            {
                return NotFound($"TariffPlan with id {id} is not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isEntityFound = await _mediator.Send(new DeleteTariffPlanCommand(id));

            if (!isEntityFound)
            {
                return NotFound($"TariffPlan with id {id} is not found.");
            }

            return NoContent();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string? userName = "", [FromQuery] string? password = "")
        {
            var users = await _mediator.Send(new GetUsersAllQuery(userName));
            if (users.Count() == 0)
            {
                return NotFound($"Неверный логин");
            }
            var user = users.FirstOrDefault();
            // Проверяем пароль
            if (!BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                return Unauthorized("Неверный пароль");
            }

            // Генерируем JWT-токен
            var token = GenerateJwtToken(user.UserName, user.Role);

            return Ok(new { Token = token, Role = user.Role, UserName = user.UserName });
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserForCreationDto? register)
        {
            var users = await _mediator.Send(new GetUsersAllQuery(register.UserName));
            if (users.Count() != 0)
            {
                return BadRequest("Пользователь с таким именем уже существует");
            }
            if (register is null)
            {
                return BadRequest("Нет данных");
            }
            // Хэшируем пароль
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.HashedPassword);
            // Создаём объект пользователя
            var newuser = new UserForCreationDto
            {
                UserName = register.UserName,
                Name = register.Name,
                HashedPassword = hashedPassword,
                Role = "user"
            };
            await _mediator.Send(new CreateUserCommand(newuser));

            return CreatedAtAction(nameof(Register), newuser);
        }
        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
