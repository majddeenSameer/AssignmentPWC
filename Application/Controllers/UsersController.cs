using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Constants;
using Domain.DomainModels;
using Domain.Interfaces;
using DTO;
using Filters;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAsyncRepository<UserApplication> _userApplicationRepository;
        private readonly IConfiguration _configuration;
        public UsersController(UserManager<User> userManager,
            IAsyncRepository<UserApplication> userApplicationRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userApplicationRepository = userApplicationRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(UserDto dto)
        {
            Log.Information(JsonConvert.SerializeObject(dto));
            try
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);

                IdentityResult result;

                if (user == null)
                {
                    user = new User();
                    user.UserName = dto.UserName;
                    user.FirstNameEn = dto.FirstNameEn;
                    user.LastNameEn = dto.LastNameEn;
                    user.EmailAddress = dto.EmailAddress;
                    user.Email = dto.EmailAddress;
                    user.Password = dto.Password;
                    user.IDN = dto.IDN;
                    user.DateOfBirth = dto.DateOfBirth;
                    user.Gender = dto.Gender;
                    user.Street = dto.Street;
                    user.City = dto.City;
                    user.CreatedBy = dto.CreatedBy;
                    user.CreatedDate = dto.CreatedDate;
                    user.ModifiedBy = dto.ModifiedBy;
                    user.ModifiedDate = dto.ModifiedDate;
                    user.IsDeleted = dto.IsDeleted;

                    result = await _userManager.CreateAsync(user, dto.Password);
                }
                else
                {
                    user.FirstNameEn = dto.FirstNameEn;
                    user.LastNameEn = dto.LastNameEn;
                    user.EmailAddress = dto.EmailAddress;
                    user.Email = dto.EmailAddress;
                    user.Password = dto.Password;
                    user.IDN = dto.IDN;
                    user.DateOfBirth = dto.DateOfBirth;
                    user.Gender = dto.Gender;
                    user.Street = dto.Street;
                    user.City = dto.City;
                    user.CreatedBy = dto.CreatedBy;
                    user.CreatedDate = dto.CreatedDate;
                    user.ModifiedBy = dto.ModifiedBy;
                    user.ModifiedDate = dto.ModifiedDate;
                    user.IsDeleted = dto.IsDeleted;
                    result = await _userManager.UpdateAsync(user);
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteUser")]
        public virtual async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userId);
                user.IsDeleted = true;
                IdentityResult result;
                result = await _userManager.UpdateAsync(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userName}")]
        public virtual async Task<UserDto> Get(string userName)
        {
            var user = _userManager.Users.Where(x => x.UserName == userName).Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstNameEn = x.FirstNameEn,
                LastNameEn = x.LastNameEn,
                EmailAddress = x.EmailAddress,
                Password = x.Password,
                IDN = x.IDN,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Street = x.Street,
                City = x.City,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsDeleted = x.IsDeleted,
                Roles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
                UserRoles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
            }).FirstOrDefault();

            return user;
        }

        [HttpPost("addRole")]
        public virtual async Task<ActionResult<IdentityResult>> AddRole(UserRoleDto dto)
        {
            var identityUser = await _userManager.FindByNameAsync(dto.username);
            return await _userManager.AddToRoleAsync(identityUser, dto.roleName);
        }

        [HttpPost("addApplication")]
        public virtual async Task<ActionResult<IdentityResult>> AddApplication(UserdApplicationDto dto)
        {
            var identityUser = await _userManager.FindByNameAsync(dto.username);
            await _userApplicationRepository.AddAsync(new UserApplication { ApplicationId = dto.applicationId, UserId = identityUser.Id });
            return Ok();
        }

        [HttpPost("addApplications")]
        public virtual async Task<ActionResult<IdentityResult>> AddApplications(UserApplicationsDto userApplicationsDto)
        {
            var identityUser = await _userManager.FindByNameAsync(userApplicationsDto.UserName);
            foreach (var appId in userApplicationsDto.ApplicationIds)
            {
                await _userApplicationRepository.AddAsync(new UserApplication { ApplicationId = appId, UserId = identityUser.Id });
            }
            return Ok();
        }

        [HttpPost("addRoles")]
        public virtual async Task<ActionResult> AddRoles(UserRolesDto userRolesDto)
        {
            var identityUser = await _userManager.FindByNameAsync(userRolesDto.UserName);
            var userRoles = await _userManager.GetRolesAsync(identityUser);
            await _userManager.RemoveFromRolesAsync(identityUser, userRoles);
            foreach (var roleName in userRolesDto.RolesName)
            {
                await _userManager.AddToRoleAsync(identityUser, roleName);
            }
            return Ok();
        }

        [HttpGet]
        [Route("getUsersForAutoComplete")]
        public async Task<ActionResult<List<UserDto>>> GetUsersForAutoComplete(string text, long applicationId)
        {
            var query = _userManager.Users.Where(e => (string.IsNullOrEmpty(text) || e.UserName.Contains(text)) &&
            e.UserApplications.Select(ua => ua.ApplicationId).Contains(applicationId));

            var users = await query.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstNameEn = x.FirstNameEn,
                LastNameEn = x.LastNameEn,
                EmailAddress = x.EmailAddress,
                Password = x.Password,
                IDN = x.IDN,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Street = x.Street,
                City = x.City,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsDeleted = x.IsDeleted,
                Roles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
                UserRoles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
            }).Take(50).ToListAsync();


            return Ok(users);
        }

        [HttpGet]
        [Route("getUsersByRoleForAutoComplete")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRoleForAutoComplete(string text, string role)
        {
            var result = await _userManager.Users.Where(e => (string.IsNullOrEmpty(text) || e.UserName.Contains(text)) &&
            e.UserRoles.Any(x => x.Role.Name == role))
            .Take(50).ToListAsync();
            return Ok(result.Adapt<IEnumerable<UserDto>>());
        }

        [HttpGet("getUserRoles/{userName}")]
        public virtual async Task<IList<string>> GetUserRoles(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            var listOfRoles = await _userManager.GetRolesAsync(identityUser);
            return listOfRoles;

        }

        [HttpPost]
        [Route("search")]
        public virtual async Task<ActionResult<PagedData<UserDto>>> Search(SearchPageDto<UsersFilterDto> searchPageDto)
        {
            var query = _userManager.Users
                .Where(x => (string.IsNullOrEmpty(searchPageDto.Criteria.UserName) || x.UserName.ToUpper().Contains(searchPageDto.Criteria.UserName.ToUpper()))
                && (string.IsNullOrEmpty(searchPageDto.Criteria.FirstNameEn) || x.FirstNameEn.ToUpper().Contains(searchPageDto.Criteria.FirstNameEn.ToUpper()))
                && (searchPageDto.Criteria.Role == null || x.UserRoles.Select(r => r.RoleId).Contains(searchPageDto.Criteria.Role.Id))
                && (searchPageDto.Criteria.Application == null || x.UserApplications.Select(r => r.ApplicationId).Contains(searchPageDto.Criteria.Application.Id))
                && (x.IsDeleted == false));

            var users = await query.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstNameEn = x.FirstNameEn,
                LastNameEn = x.LastNameEn,
                EmailAddress = x.EmailAddress,
                Password = x.Password,
                IDN = x.IDN,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Street = x.Street,
                City = x.City,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsDeleted = x.IsDeleted,
                Roles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Applications = string.Join(", ", x.UserApplications.Select(ur => ur.Application.Code))
            })
            .Skip(searchPageDto.PageIndex * searchPageDto.PageSize).Take(searchPageDto.PageSize).ToListAsync();

            var result = new PagedData<UserDto>();

            result.Data = users.Adapt<IList<UserDto>>();
            result.TotalCount = await query.CountAsync();

            return result;
        }

    }
}