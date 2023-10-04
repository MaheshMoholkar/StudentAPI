using ERPLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IUserRepository _repository;

    public HomeController(IUserRepository repository)
    {
        _repository = repository;
    }
    // GET api/<HomeController>/5
    [HttpPost("students/{id}")]
    public async Task<IActionResult> GetUserInfo(int id)
    {
        var data = await _repository.GetUserInfo(id, "student");

        return Ok(data);
    }

    [HttpGet("connection")]
    public IActionResult ChecKConnection()
    {
        return BadRequest();
    }

}
