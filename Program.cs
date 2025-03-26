using System;
using System.Diagnostics;
using System.IO;

public class Program
{
    public static void Main()
    {
        // ffmpeg -i input.mp4 -vf "scale=1280:720" output.mp4

        Console.Title = "VideoResolutionChanger | Made by https://github.com/ZygoteCode/";
        string videoPath = "";

        while (!File.Exists(videoPath))
        {
            Console.Write("Please, insert the path of the video: ");
            videoPath = Console.ReadLine();

            while (videoPath.StartsWith("\"") || videoPath.StartsWith("'") || videoPath.StartsWith(" ") || videoPath.StartsWith("\t"))
            {
                videoPath = videoPath.Substring(1);
            }

            while (videoPath.EndsWith("\"") || videoPath.EndsWith("'") || videoPath.EndsWith(" ") || videoPath.EndsWith("\t"))
            {
                videoPath = videoPath.Substring(0, videoPath.Length - 1);
            }

            if (!File.Exists(videoPath))
            {
                Console.WriteLine("The specified video file does not exist. Please, try again.");
            }
        }

        string videoWidth = "";

        while (!IsNumberValid(videoWidth))
        {
            Console.Write("Please, insert the new width of the video: ");
            videoWidth = Console.ReadLine();

            if (!IsNumberValid(videoWidth))
            {
                Console.WriteLine("The specified new video width is not a valid number. Please, try again.");
            }
        }

        string videoHeight = "";

        while (!IsNumberValid(videoHeight))
        {
            Console.Write("Please, insert the new height of the video: ");
            videoHeight = Console.ReadLine();

            if (!IsNumberValid(videoHeight))
            {
                Console.WriteLine("The specified new video height is not a valid number. Please, try again.");
            }
        }

        string newFilePath = videoPath;
        string fileExtension = Path.GetExtension(newFilePath);
        newFilePath = newFilePath.Substring(0, newFilePath.Length - fileExtension.Length);
        newFilePath += "-changed" + fileExtension;
        Console.WriteLine("Changing resolution, please wait a while.");
        RunFFMpeg($"-i \"{videoPath}\" -vf \"scale={videoWidth}:{videoHeight}\" \"{newFilePath}\"");
        Console.WriteLine($"Resolution succesfully changed. The new video is located in this path: '{newFilePath}'.");
        Console.WriteLine("Press ENTER in order to exit from the program.");
        Console.ReadLine();
    }

    private static bool IsNumberValid(string number)
    {
        if (!Microsoft.VisualBasic.Information.IsNumeric(number))
        {
            return false;
        }

        if (!short.TryParse(number, out _))
        {
            return false;
        }

        return true;
    }

    private static void RunFFMpeg(string arguments)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg.exe",
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        }).WaitForExit();
    }
}
