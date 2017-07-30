using System;

namespace CoE.em8.Core
{
    public static class RuntimePlatform
    {
        public static bool IsUnix
        {
            get;
            private set;
        }

        public static PlatformID PlatformID
        {
            get;
            private set;
        }

        public static string UserHomeDirectory
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); }
        }

        static RuntimePlatform()
        {
            PlatformID = Environment.OSVersion.Platform;

            /**
             *   4 ... Unix
             *   6 ... OS X
             * 128 ... Unix (old versions of Mono)
             */
            int p = (int)PlatformID;
            IsUnix = ((p == 4) || (p == 6) || (p == 128));
        }
    }
}
