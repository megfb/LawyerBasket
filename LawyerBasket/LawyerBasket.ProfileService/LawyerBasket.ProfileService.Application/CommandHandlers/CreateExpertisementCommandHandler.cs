using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class CreateExpertisementCommandHandler : IRequestHandler<CreateExpertisementCommand, ApiResult<ExpertisementDto>>
    {
        private readonly IExpertisemenetRepository _expertisementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateExpertisementCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public CreateExpertisementCommandHandler(
            IExpertisemenetRepository expertisementRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<CreateExpertisementCommandHandler> logger, 
            IMapper mapper)
        {
            _expertisementRepository = expertisementRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<ExpertisementDto>> Handle(CreateExpertisementCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateExpertisement started. Name: {Name}", request.Name);
            try
            {
                var entity = new Expertisement
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                };
                
                _logger.LogInformation("Creating expertisement entity with Name: {Name}", request.Name);
                await _expertisementRepository.CreateAsync(entity);

                _logger.LogInformation("Saving changes to the database for expertisement with Name: {Name}", request.Name);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Expertisement created successfully with Id: {ExpertisementId}", entity.Id);
                return ApiResult<ExpertisementDto>.Success(_mapper.Map<ExpertisementDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating expertisement with Name: {Name}", request.Name);
                return ApiResult<ExpertisementDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

