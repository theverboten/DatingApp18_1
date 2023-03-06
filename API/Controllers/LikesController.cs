using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _ouw;
        public LikesController(IUnitOfWork uow)
        {
            _ouw = uow;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult>AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _ouw.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _ouw.LikesRepository.GetUserWithLikes(sourceUserId);

            if(likedUser ==null) return NotFound();

            if(sourceUser.UserName == username) return BadRequest("You cannot give like to yourself");

            var userLike = await _ouw.LikesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if(userLike != null) return BadRequest("You already liked this user");

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = likedUser.Id
            };
            sourceUser.LikedUsers.Add(userLike);

            if(await _ouw.Complete()) return Ok();
            return BadRequest("Failed to like user");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
        {  likesParams.UserId = User.GetUserId();


            var users = await _ouw.LikesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }

       
    }
}