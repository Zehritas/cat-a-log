using System;
using System.IO;


namespace cat_a_logB.Data
{
    public class ExceptionLogger
    {
        public void LogExceptionToFile(string message)
        {
            try
            {
                string filePath = @"ExceptionLog\ExceptionLog.txt";

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                // Log any issues occurring during logging itself
                Console.WriteLine($"Error while logging exception: {ex.Message}");
            }
        }
    }
}
