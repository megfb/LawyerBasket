using LawyerBasket.GatewayTest.Aggregators;
using LawyerBasket.GatewayTest.Contracts;
using LawyerBasket.GatewayTest.Dtos;

namespace LawyerBasket.GatewayTest.Composes
{
  public class UserProfilePostCompose
  {
    private readonly PostAggregator _postAggregator;
    private readonly UserProfileAggregator _userProfileAggregator;

    public UserProfilePostCompose(PostAggregator postAggregator, UserProfileAggregator userProfileAggregator)
    {
      _postAggregator = postAggregator;
      _userProfileAggregator = userProfileAggregator;
    }

    public async Task<UserProfilePostDto> AggregateAsync(string id)
    {

      var userTask = await _userProfileAggregator.AggregateAsync(id);
      var postTask = await _postAggregator.AggregateAsync(id);


      return new UserProfilePostDto
      {
        UserProfileDto =  userTask,
        PostDto =  postTask
      };
    }
  }
}
