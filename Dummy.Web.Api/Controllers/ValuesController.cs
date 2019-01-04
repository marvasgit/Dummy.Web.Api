﻿namespace Dummy.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ValuesController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// The Get
        /// </summary>
        /// <returns>The <see cref="ActionResult{IEnumerable{string}}"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                return new string[] { "value1", "value2" };
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                throw new System.ArgumentException("test exceptions");
            }
        }

        // GET api/values/5
        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult{string}"/></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="value">The value<see cref="string"/></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="value">The value<see cref="string"/></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
