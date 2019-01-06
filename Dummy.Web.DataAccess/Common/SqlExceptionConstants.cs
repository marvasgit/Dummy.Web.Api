namespace Dummy.Web.DataAccess.Common
{
    public static class SqlExceptionsConstants
    {
        public static int CanNotObtainLockExceptionCode { get { return 1204; } }
        public static int DeadlockExceptionCode { get { return 1205; } }
    }
}
