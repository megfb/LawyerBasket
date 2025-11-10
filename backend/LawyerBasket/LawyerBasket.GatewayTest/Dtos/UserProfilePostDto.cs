using LawyerBasket.GatewayTest.Dtos.PostService;
using LawyerBasket.GatewayTest.Dtos.ProfileService;

namespace LawyerBasket.GatewayTest.Dtos
{
    public class UserProfilePostDto
    {
        public IEnumerable<PostDto> PostDto { get; set; }
        public UserProfileDto UserProfileDto { get; set; }
    }
}
