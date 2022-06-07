namespace WujuStyleBot
{
    public static class BoolHelpers
    {
        public static bool IsDebug ( )
        {
#if DEBUG
            return true;
#else
                return false;
#endif
        }
    }
}

