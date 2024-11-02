using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Session
{
    public class SessionManager
    {
        public static int? CurrentUserId { get; private set; }

        public static void SetCurrentUser(int userId)
        {
            CurrentUserId = userId;
        }

        public static void ClearCurrentUser()
        {
            CurrentUserId = -1;
        }
    }
}
