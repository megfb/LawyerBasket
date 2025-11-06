using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommand, ApiResult>
    {
        private readonly ILogger<RemoveAddressCommandHandler> _logger;
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveAddressCommandHandler(ILogger<RemoveAddressCommandHandler> logger, IAddressRepository addressRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RemoveAddressCommand for address id {AddressId}.", request.Id);

            try
            {
                var address = await _addressRepository.GetByIdAsync(request.Id);

                if (address is null)
                {
                    _logger.LogWarning("Address with id {AddressId} not found.", request.Id);
                    return ApiResult.Fail($"Address with id {request.Id} not found.", System.Net.HttpStatusCode.NotFound);
                }

                _logger.LogInformation("Removing address with id {AddressId}.", request.Id);
                _addressRepository.Delete(address);

                _logger.LogInformation("Address with id {AddressId} removed.", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ApiResult.Success(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing address with id {AddressId}.", request.Id);
                return ApiResult.Fail("An error occurred while removing the address.", System.Net.HttpStatusCode.InternalServerError);
            }


        }
    }
}
