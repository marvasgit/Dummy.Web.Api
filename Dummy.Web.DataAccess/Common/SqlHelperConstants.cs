namespace Dummy.Web.DataAccess.Common
{
    public static class SqlHelperConstants
    {
        public static int MaxTriesToConnect { get { return 4; } }
        public static int MillisecondsToWait { get { return 1000; } }
        public static int TimeOutInSeconds { get { return 6000; } }
    }
}
