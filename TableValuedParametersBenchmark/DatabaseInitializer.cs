﻿using System;
using System.Diagnostics;

namespace TableValuedParameterExample
{
    internal static class DatabaseInitializer
    {
        public static void DeployFreshDatabase()
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + "\"..\\..\\PublishMovieDbToLocalDb.bat\"");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }
    }
}
