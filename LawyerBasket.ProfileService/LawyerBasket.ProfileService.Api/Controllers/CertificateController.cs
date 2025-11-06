using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CertificateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateCertificate")]
        public async Task<IActionResult> CreateCertificate(CreateCertificateCommand createCertificateCommand)
        {
            return Ok(await _mediator.Send(createCertificateCommand));
        }

        [HttpPut("UpdateCertificate/{id}")]
        public async Task<IActionResult> UpdateCertificate(string id, UpdateCertificateCommand updateCertificateCommand)
        {
            return Ok(await _mediator.Send(updateCertificateCommand));

        }
        [HttpGet("GetCertificate/{id}")]
        public async Task<IActionResult> GetCertificateById(string id)
        {
            return Ok(await _mediator.Send(new GetCertificateQuery { Id = id }));
        }
        [HttpGet("GetCertificates/{id}")]
        public async Task<IActionResult> GetCertificates(string id)
        {
            return Ok(await _mediator.Send(new GetCertificatesQuery { LawyerProfileId = id }));
        }
        [HttpDelete("RemoveCertificate/{id}")]
        public async Task<IActionResult> RemoveCertificate(string id)
        {
            return Ok(await _mediator.Send(new RemoveCertificateCommand { Id = id }));
        }
    }
}
