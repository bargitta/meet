using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jsdoc.Writer
{
    public interface IReader
    {
        bool AddMember(string member);
        bool UpdateSessionInfo(string info);
        bool UpdateSessionInfo(string topic, string timeplace);
        string ReadSessionInfo();
        string[] ReadMembers();
        bool ClearAll();
    }
}