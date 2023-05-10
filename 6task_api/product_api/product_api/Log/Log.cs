namespace product_api.Log
{
    using System.IO;
    using System.Reflection;


    public class Log : ILog
    {
        private string m_exePath = string.Empty;
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText("Log/log.txt"))
                {
                    Loger(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Loger(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                    logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
