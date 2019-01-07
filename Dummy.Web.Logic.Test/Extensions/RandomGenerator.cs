namespace Dummy.Web.Logic.Test.Extensions
{
    using System;
    using System.Linq;

    public static class RandomGenerator
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomEmailAddress(int maxAddrLength)
        {
            return $"{RandomString(random.Next(maxAddrLength))}@{RandomString(random.Next(maxAddrLength))}.com";
        }
    }
}
