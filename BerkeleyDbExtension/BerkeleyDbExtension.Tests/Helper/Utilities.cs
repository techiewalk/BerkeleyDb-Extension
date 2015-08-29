using System;


namespace BerkeleyDbExtension.Tests.Helper
{
    public class Utilities
    {
        public static bool IsSuccess(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool OfException<T>(Action action) where T: Exception
        {
            try
            {
                action();
                return false;
            }
            catch(Exception e)
            {
                return typeof (T) == e.GetType();
            }
        }
    }
}
