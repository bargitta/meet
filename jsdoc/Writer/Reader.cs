using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace jsdoc.Writer
{
    public class Reader : IReader
    {
        private static string FILE_PATH = HttpContext.Current.Server.MapPath("~/App_Data/members.txt");
        private static string FILE_PATH_Meeting = HttpContext.Current.Server.MapPath("~/App_Data/meeting.txt");
        public bool AddMember(string member)
        {
            StreamWriter file = null;
            try
            {
                file = new StreamWriter(FILE_PATH, true);
                file.WriteLine(member);
            }catch(Exception)
            {
                return false;
            }
            finally
            {
                file.Close();
            }
            return true;
        }
        public bool UpdateSessionInfo(string info)
        {
            CreateFile(FILE_PATH_Meeting);
            try
            {
                using (var file = new StreamWriter(FILE_PATH_Meeting, true))
                {
                    file.WriteLine(info);                  
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool UpdateSessionInfo(string topic, string timeplace)   
        {
            CreateFile(FILE_PATH_Meeting);
            try
            {
                using (var file = new StreamWriter(FILE_PATH_Meeting, true))
                {
                    file.WriteLine(topic);
                    file.WriteLine(timeplace);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public string ReadSessionInfo()
        {
            var sessionInfo = new StringBuilder();
            try
            {
                using (var file = new StreamReader(FILE_PATH_Meeting))
                {
                    string oneline;
                    while ((oneline = file.ReadLine()) != null)
                    {
                        sessionInfo.Append(oneline);

                    }
                }
            }
            catch (Exception)
            {
            }
            return sessionInfo.ToString();
           
        }
        public string[] ReadMembers()
        {
            var members = new List<string>();          
            try
            {
                using (var file = new StreamReader(FILE_PATH))
                {
                    string oneline;
                    while ((oneline = file.ReadLine()) != null)
                    {
                        members.Add(oneline);
                    }
                }
            }
            catch (Exception)
            {
            }
            return members.ToArray();
        }

        public bool ClearAll()
        {
            return CreateFile(FILE_PATH);
        }
        internal bool CreateFile(string fileName)
        {
            try
            {
                using (FileStream fs = File.Create(fileName))
                {}
            }
            catch (Exception)
            {
                return false;
            }
            finally
            { }            
            return true;
        }
    }
}