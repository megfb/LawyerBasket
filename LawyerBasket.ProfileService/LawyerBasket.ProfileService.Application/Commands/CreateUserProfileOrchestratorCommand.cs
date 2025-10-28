using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateUserProfileOrchestratorCommand : IRequest<ApiResult<string>>
  {
    public CreateUserProfileCommand User { get; set; }
    public CreateAddressCommand? Address { get; set; }
    public CreateLawyerProfileCommand? LawyerProfile { get; set; }
    public List<CreateContactCommand>? Contacts { get; set; }
    public List<CreateExperienceCommand>? Experiences { get; set; }
    public List<CreateAcademyCommand>? Academies { get; set; }
    public List<CreateCertificateCommand>? Certificates { get; set; }
    public List<CreateExpertisementToLawyerCommand>? Expertisements { get; set; }
  }
}


