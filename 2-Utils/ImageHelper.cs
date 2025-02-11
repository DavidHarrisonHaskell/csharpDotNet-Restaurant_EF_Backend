using System.IO;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Talent;

public class ImageHelper
{
    public static string SaveImage(IFormFile image)
    {
        // save the image in the database if there is one:
        // take file extension (.jpg / .bmp / .png / .gif)
        string extension = Path.GetExtension(image.FileName);

        // create a unique file name:
        string uniqueName = Guid.NewGuid() + extension;

        // create stream to image file
        using FileStream stream = File.Create("1-Assets/Images/" + uniqueName);

        // copy image to stream:
        image.CopyTo(stream);

        // return file name:
        return uniqueName;
    }

    public static string UpdateImage(IFormFile image, string? previousImageName)
    {
        DeleteImage(previousImageName);
        return SaveImage(image);
    }

    public static void DeleteImage(string? previousImageName)
    {
        if (previousImageName == null) return;
        File.Delete("1-Assets/Images/" + previousImageName);
    }

    public static byte[] GetImageBytes(string imageName)
    {
        if (!File.Exists("1-Assets/Images/" + imageName)) imageName = "NotFound.jpg";
        byte[] imageBytes = File.ReadAllBytes("1-Assets/Images/" + imageName);
        return imageBytes;
    }

}
