using LawyerBasket.PostService.Application;
using LawyerBasket.PostService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LawyerBasket.PostService.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IClass1 _class1;
    public WeatherForecastController(IClass1 class1)
    {
      _class1 = class1;
    }
    [HttpPost("addtodb")]
    public async Task<ObjectResult> Add()
    {
      Post post = new Post
      {
        Id = Guid.NewGuid().ToString(),
        Content = "Hello World2",
        Likes = new List<string>(),
        Comments = new List<Comment>
        {
          new Comment
          {
            UserId = "user1",
            Text = "Great post!",
          }
        },
        CreatedAt = DateTime.UtcNow,
      };

      post.Likes.Add("user2");
      return Ok(await _class1.AddToDb(post));
    }
  }
}
