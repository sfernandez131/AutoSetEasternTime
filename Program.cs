using System.Diagnostics;

try
{
    // Get the current time zone
    TimeZoneInfo currentTimeZone = TimeZoneInfo.Local;

    // Define the Eastern Time zone ID (this includes both EST and EDT)
    string easternZoneId = "Eastern Standard Time";

    // Check if the current time zone is not Eastern Time (including DST handling)
    if (currentTimeZone.Id != easternZoneId)
    {
        try
        {
            // Use the tzutil command to change the time zone
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/c tzutil /s \"{easternZoneId}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true; // To hide the command prompt window
            process.Start();

            process.WaitForExit(); // Wait for the command to finish

            if (process.ExitCode == 0)
            {
                Console.WriteLine("Time zone changed to Eastern Time (including DST handling) successfully.");
            }
            else
            {
                Console.WriteLine("Failed to change the time zone.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Current time zone is already set to Eastern Time (including DST handling).");
    }
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.ToString());
    using (StreamWriter w = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + @"\FixemLog.txt"))
    {
        w.WriteLine(DateTime.Now + "\n");
        w.WriteLine(ex.ToString());
    }
}