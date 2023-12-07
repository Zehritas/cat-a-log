using System;
using System.IO;

public class ExceptionLogger
{
    private const string logFilePath = "exception_log.txt";

    public static void LogException(Exception ex)
    {
        
        using (var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
        using (var streamWriter = new StreamWriter(fileStream))
        {
            streamWriter.WriteLine($"Exception occurred at {DateTime.Now}");
            streamWriter.WriteLine($"Message: {ex.Message}");
            streamWriter.WriteLine($"Stack Trace: {ex.StackTrace}");
            streamWriter.WriteLine("-------------------------------------------------------------");
        }
    }
}