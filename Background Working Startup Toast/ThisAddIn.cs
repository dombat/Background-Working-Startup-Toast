using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Background_Working_Startup_Toast
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                var stopTime = DateTime.Now.AddSeconds(5);
                while (DateTime.Now < stopTime)
                {
                    // Your code here

                    await Task.Delay(100); // Delay for a short period of time to prevent tight looping
                }

                ShowPopupMessage("Hello, World!", 5);
            });
        }


      

        internal static void ShowPopupMessage(string message, int durationInSeconds)
        {
            if (message.Length > 500)
            {
                throw new ArgumentException("Message cannot exceed 500 characters.", nameof(message));
            }

            if (durationInSeconds > 10)
            {
                throw new ArgumentException("Duration cannot be greater than 10 seconds.", nameof(durationInSeconds));
            }

            NotifyIcon notifyIcon = null;
            Timer timer = null;

            try
            {
                notifyIcon = new NotifyIcon();
                timer = new Timer();
                timer.Interval = durationInSeconds * 1000; // Convert seconds to milliseconds

                timer.Tick += (sender, e) =>
                {
                    // Stop the timer and hide the popup message when the time is up
                    timer.Stop();
                    notifyIcon.Visible = false;
                };

                notifyIcon.Icon = System.Drawing.SystemIcons.Information;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(durationInSeconds * 1000, "Notification", message, ToolTipIcon.Info);

                timer.Start();
            }
            finally
            {
                if (timer != null)
                {
                    timer.Dispose();
                }

                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                }
            }
        }



        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}

