using jsdoc.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using jsdoc.Writer;
using Microsoft.Practices.Unity;

namespace jsdoc.Controllers
{
    public class MembersController : ApiController
    {

        [Dependency]
        public IReader FileReader { get; set; }
        // GET api/members
        public JArray Get()
        {
            return new JArray(FileReader.ReadMembers());
        }

       
        // GET api/members/5
        public string Get(int id)
        {
            if (IsValidId(id))
                return FileReader.ReadMembers()[id];
            return null;
        }

        private bool IsValidId(int id)
        {
            return id > -1 && id < FileReader.ReadMembers().Length;
        }

        // POST api/members
        public void Post([FromBody]JObject value)
        {

            if (value != null)
            {
                var rawValue = value.GetValue("name").ToString();
                if (!IsValid(rawValue))
                    return;
                if (rawValue.Length > 20)
                    rawValue = rawValue.Substring(0, 20);
                var member = HttpContext.Current.Server.HtmlEncode(rawValue);
                var members = FileReader.ReadMembers();
                if (!members.Contains(member))
                {
                    FileReader.AddMember(member);
                }
            }
        }
        [HttpPost]
        [Route("api/members/clear")]
        public IHttpActionResult Clear()
        {
            if (FileReader.ClearAll())
                return StatusCode(HttpStatusCode.NoContent);
            return BadRequest();
        }
        private bool IsValid(string member)
        {
            var inputString = member.ToLower();
            return !inputString.Contains("script") && !inputString.Contains("<");
        }

        private bool IsValidString(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
