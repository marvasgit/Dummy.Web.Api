namespace Dummy.Web.Api.Controllers
{
    using System.Collections.Generic;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.User;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="UserContorller" />
    /// </summary>
    [Produces("application/json")]
    [Route("users")]
    public class UserContorller : Controller
    {
        /// <summary>
        /// Defines the _userLogic
        /// </summary>
        private readonly IUserLogic _userLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContorller"/> class.
        /// </summary>
        /// <param name="userLogic">The userLogic<see cref="IUserLogic"/></param>
        public UserContorller(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        /// <summary>
        /// The GetAvailableUsers
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<UserModelSimplified>), 200)]
        public ActionResult GetAvailableUsers()
        {
            return Ok(_userLogic.GetAvailableUsers());
        }

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="user">The user<see cref="UserCreateModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public ActionResult AddUser([FromBody]UserCreateModel user)
        {
            return Ok(_userLogic.AddUser(user));
        }

        /// <summary>
        /// The UpdateUser
        /// </summary>
        /// <param name="updateModel">The updateModel<see cref="UserUpdateModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
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
