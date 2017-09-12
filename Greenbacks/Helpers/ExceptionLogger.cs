using System;
using System.IO;

namespace Greenbacks.Helpers
{
    public static class ExceptionLogger
    {
        private static string fileName = "Exception Log.log";

        public static void AppendExceptionLog(Exception ex)
        {
            if (ex != null)
            {
                using (StreamWriter sw = File.AppendText(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName)))
                {
                    sw.WriteLine("[" + DateTime.Now + "] " + ex.Message);
                    sw.WriteLine(ex.StackTrace);
                    sw.WriteLine(string.Empty);
                }
            }
        }
    }
}
