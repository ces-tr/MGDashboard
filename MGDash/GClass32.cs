namespace MGDash
{
    
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public sealed class CloseMethods 
    {
        public static DashBoard dashBoard;

        public static bool exit()
        {
            dashBoard.Exit();
            return true;
        }

        public static bool logout()
        {
            dashBoard.settings.HttpConnection.DeleteCookie();
            dashBoard.Exit();
            return true;
        }

        public static bool smethod_2()
        {
            Application.Run(new Login());
            return true;
        }

        public static bool shutdown()
        {
            Process.Start("shutdown", "/s /t 0");
            return true;
        }

        public static bool restart()
        {
            Process.Start("shutdown", "/s /t 0");
            return true;
        }

        public static bool hibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
            return true;
        }
    }
}

