﻿using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdInfrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private AuthorizationDetails _authorizationDetails;

        public AuthorizationDetails GetAuthorizationDetails()
        {
            if (_authorizationDetails != null)
                return _authorizationDetails;

            var user = _httpContextAccessor.HttpContext.User;

            _authorizationDetails = new AuthorizationDetails(user.Claims);

            return _authorizationDetails;

        }
    }
}
