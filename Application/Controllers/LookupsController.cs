using Mapster;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Domain.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Lookups;

namespace Application.Controllers
{
    public class LookupsController : ControllerBase
    {
        private ILookupsRepository _repository { get; set; }
        private IIdentityService _identityService { get; set; }
        private ISpecificationFactory _specificationFactory;

        public LookupsController(ILookupsRepository repository,
                                 IIdentityService identityService,
                                 ISpecificationFactory specificationFactory)
        {
            _repository = repository;
            _identityService = identityService;
            _specificationFactory = specificationFactory;
        }



        // GET: api/[controller]
        [HttpGet("api/[controller]/{lookupName}/{lang}")]
        //[ResponseCache(CacheProfileName = "Default")]
        public virtual IEnumerable<LookupDto> Get(string lookupName, string lang)

        {
            var user = _identityService.GetAuthorizationDetails();
            var lookupType = typeof(LookupBase).Assembly.GetExportedTypes().SingleOrDefault(x => x.IsSubclassOf(typeof(LookupBase)) && x.Name.ToUpper() == lookupName.ToUpper());

            MethodInfo method = typeof(ILookupsRepository).GetMethod("ListAll");
            MethodInfo generic = method.MakeGenericMethod(lookupType);
            var data = generic.Invoke(_repository, null);
            return data.Adapt<List<LookupDto>>();
        }

        // GET: api/[controller]
        [HttpGet("api/[controller]/search/{lookupName}")]
        public virtual IEnumerable<LookupDto> Search(string lookupName, string text)

        {
            var user = _identityService.GetAuthorizationDetails();
            var lookupType = typeof(LookupBase).Assembly.GetExportedTypes().SingleOrDefault(x => x.IsSubclassOf(typeof(LookupBase)) && x.Name.ToUpper() == lookupName.ToUpper());

            MethodInfo method = typeof(ILookupsRepository).GetMethod("Search");
            MethodInfo generic = method.MakeGenericMethod(lookupType);
            var data = generic.Invoke(_repository, new object[] { text });
            return data.Adapt<List<LookupDto>>();
        }

    }
}
