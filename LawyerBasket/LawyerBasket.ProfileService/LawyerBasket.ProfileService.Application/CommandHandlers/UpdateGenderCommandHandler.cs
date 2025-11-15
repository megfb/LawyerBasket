using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, ApiResult<GenderDto>>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateGenderCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public UpdateGenderCommandHandler(
            IGenderRepository genderRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<UpdateGenderCommandHandler> logger, 
            IMapper mapper)
        {
            _genderRepository = genderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<GenderDto>> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateGenderCommandHandler Handle method invoked for Id: {Id}", request.Id);
            try
            {
                var gender = await _genderRepository.GetByIdAsync(request.Id);
                if (gender == null)
                {
                    _logger.LogWarning("Gender with Id: {GenderId} not found.", request.Id);
                    return ApiResult<GenderDto>.Fail("Gender not found", System.Net.HttpStatusCode.NotFound);
                }
                
                gender.Name = request.Name;
                gender.Description = request.Description;
                gender.UpdatedAt = request.UpdatedAt;
                
                _logger.LogInformation("Updating gender with Id: {GenderId}.", request.Id);
                _genderRepository.Update(gender);
                
                _logger.LogInformation("Saving changes to the database for GenderId: {GenderId}.", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Gender with Id: {GenderId} updated successfully.", request.Id);
                return ApiResult<GenderDto>.Success(_mapper.Map<GenderDto>(gender));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating gender with Id: {GenderId}.", request.Id);
                return ApiResult<GenderDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

