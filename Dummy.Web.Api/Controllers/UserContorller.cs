
namespace Dummy.Web.Api.Controllers
{
    using System.Collections.Generic;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.User;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("users")]
    public class UserContorller : Controller
    {
        private readonly IUserLogic _userLogic;
        public UserContorller(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<UserModelSimplified>), 200)]
        public ActionResult GetAvailableUsers()
        {
            return Ok(_userLogic.GetAvailableUsers());
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public ActionResult AddUser([FromBody]UserCreateModel user)
        {
            return Ok(_userLogic.AddUser(user));
        }
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public ActionResult UpdateUser([FromBody]UserUpdateModel updateModel)
        {
            return Ok(_userLogic.UpdateUser(updateModel));
        }

    }
}
