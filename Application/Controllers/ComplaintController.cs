using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.DomainModels;
using Domain.Interfaces;
using DTO;
using Infrastructure;

namespace Application.Controllers
{/// <summary>
/// 
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ComplaintController : GenericControllerBase<Complaint, ComplaintDto, ComplaintDto>
    {
        private readonly ILookupsRepository _lookupsRepository;
        private IReportService _reportService;
        private ISpecificationFactory _specificationFactory;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repsitory"></param>
        /// <param name="specificationFactory"></param>
        /// <param name="identityService"></param>
        /// <param name="domainService"></param>
        /// <param name="lookupsRepository"></param>
        /// <param name="reportService"></param>
        /// <param name="configuration"></param>
        public ComplaintController(IReadAsyncRepository<Complaint> repsitory,
                                ISpecificationFactory specificationFactory,
                                IIdentityService identityService,IDomainService<Complaint> domainService,
                                ILookupsRepository lookupsRepository, IReportService reportService,
                                IConfiguration configuration) : base(repsitory, domainService, specificationFactory, identityService)
        {
            _lookupsRepository = lookupsRepository;
            _reportService = reportService;
            _specificationFactory = specificationFactory;
            _configuration = configuration;
        }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="searchPageDto"></param>
  /// <returns></returns>
        [HttpPost]
        [Route("UserComplaints")]
        public virtual async Task<ActionResult<Domain.PagedData<ComplaintDto>>> UserComplaints(SearchPageDto<ComplaintDto> searchPageDto)
        {
            _searchSpecification = _searchSpecification
                .And(s =>
                 (searchPageDto.Criteria.NoAuthUser ==  null || searchPageDto.Criteria.NoAuthUser.Id == s.NoAuthUser.Id));

            var result = await _repository.PageAsync<ComplaintDto>(_searchSpecification, searchPageDto.PageIndex, searchPageDto.PageSize, searchPageDto.SortBy, OrderDirectionEnum.OrderByDescending);

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchPageDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApproveComplaint")]
        public virtual async Task<IActionResult> UserComplaints(ComplaintDto searchPageDto)
        {
            var entity = await _repository.GetByIdAsync(searchPageDto.Id.Value);
            searchPageDto.Adapt(entity);
            await _domainService.UpdateAsync(entity);
            return Ok();
        }
    }
}
