using System;

namespace IS_XNA_Shooter
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static public float scale = 1;

        static void Main(string[] args)
        {
            using (SuperGame game = new SuperGame())
            {
                game.Run();
            }
        }
    }
#endif
}

