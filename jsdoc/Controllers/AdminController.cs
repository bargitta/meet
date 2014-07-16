using jsdoc.Writer;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace jsdoc.Controllers
{
    public class AdminController : ApiController
    {
        [Dependency]
        public IReader FileReader { set; get; }
        private bool IsValid(string member)
        {
            var inputString = member.ToLower();
            return !inputString.Contains("script") && !inputString.Contains("<");
        }

        public string Get()
        {
            return FileReader.ReadSessionInfo();
        }
        public IHttpActionResult Post([FromBody]JObject value)
        {
            try
            {
                if (value != null)
                {
                    FileReader.UpdateSessionInfo(value.ToString());
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
