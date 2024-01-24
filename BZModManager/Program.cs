using System.Data.SqlTypes;
using System.Diagnostics;


namespace BZModManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            mod.updateMods();



            Application.Run(new Form1());

        }
    }
}