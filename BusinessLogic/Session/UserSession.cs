using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Session
{
    public class UserSession
    {
        public int UserId { get; private set; }
        //public string UserName { get; private set; }
        
        public void SetUserSession(int userId)
        {
            UserId = userId;
            //UserName = userName;
        }

        public void ClearSession()
        {
            UserId = -1;
            //UserName = string.Empty;
        }
    }
}
