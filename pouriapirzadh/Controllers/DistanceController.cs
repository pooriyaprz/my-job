using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core;
using Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistanceController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;

        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public DistanceController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, IMapper mapper, IUnitOfWork unitOfWork)
        {

            _userManager = userManager;
            _signinManager = signinManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Distance
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault().Value;
            if (userId != null)
            {
                var allRequests =await _unitOfWork.Distances.GetAllDistanceReq(userId);
                return Ok(allRequests);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/Distance
        [HttpPost]
        public async Task<IActionResult> Post(CalculateDistanceDto cDistanceDto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault().Value;
            if (userId != null)
            {
                //var currentUser = await _userManager.FindByNameAsync(userName);
                var distance = _unitOfWork.Distances.CalculateDistance(cDistanceDto.FirstLat, cDistanceDto.FirstLon, cDistanceDto.SecondLat, cDistanceDto.SecondLon, cDistanceDto.Type.ToUpper());
                var res = _mapper.Map<Distance>(cDistanceDto);
                res.UsersId = userId;
                res.FinalDistance = distance;


                _unitOfWork.Distances.Add(res);

                _unitOfWork.Complete();

                return Ok(distance);
            }
            else
            {
                return Unauthorized();
            }




        }

        // PUT: api/Distance/5

    }
}
