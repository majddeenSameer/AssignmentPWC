using Mapster;
using Domain;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application
{
    public class MyRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTo("[name]ResultDto", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ApplyDefaultRule();

            config.AdaptFrom("[name]Dto", MapType.Map)
                .ApplyDefaultRule();
        }
    }

    static class RegisterExtensions
    {
        public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
        {
            return builder
                .ForAllTypesInNamespace(typeof(Domain._IAssemblyMark).Assembly, "Domain.DomainModels")

                .ExcludeTypes(type => type.IsEnum)
                .AlterType(type => type.IsAssignableFrom(typeof(LookupBase)) == true, typeof(LookupDto))
                .ShallowCopyForSameType(true).IgnoreNullValues(true);
        }
    }
}
