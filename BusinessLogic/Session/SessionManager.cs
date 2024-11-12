namespace BusinessLogic.Session
{
    public class SessionManager
    {
        public static int? CurrentUserId { get; private set; }
        public static int? CurrentAccountId { get; private set; }

        public static void SetCurrentUser(int userId)
        {
            CurrentUserId = userId;
        }

        public static void ClearCurrentUser()
        {
            CurrentUserId = -1;
        }

        public static void SetCurrentAccount(int accountId)
        {
            CurrentAccountId = accountId;
        }

        public static void ClearCurrentAccount()
        {
            CurrentAccountId = -1;
        }
    }
}
