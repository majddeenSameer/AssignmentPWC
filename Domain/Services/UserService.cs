//using Microsoft.Extensions.Configuration;
//using QRCoder;
//using System;
//using System.Drawing;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.DomainModels;
//using Domain.Interfaces;

//namespace Domain.Services
//{
//    public class UserService : GenericServiceBase<User>, IUserService
//    {

//        protected ISpecification<User> _Specification;
//        private readonly IConfiguration _configuration;

//        public UserService(IAsyncRepository<User> repository, IIdentityService identityService,
//                                         ISpecificationFactory specificationFactory,IConfiguration configuration)
//            : base(repository, identityService)
//        {
//            _configuration = configuration;
//            _Specification = specificationFactory.Create<User>();
//        }

//        public override async Task<User> AddAsync(User entity)
//        {
//            return await _repository.AddAsync(entity);
//        }


//        public override async Task UpdateAsync(User entity)
//        {
//            await _repository.UpdateAsync(entity);
//        }

//    }
//}
