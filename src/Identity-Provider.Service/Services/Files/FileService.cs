
using FileSystem.Service.Exeptions.Files;
using HeavyService.Application.Exeptions.Files;
using Identity_Provider.DataAccess.ViewModels.Files;
using Identity_Provider.Service.Common.Helpers;
using Identity_Provider.Service.Interfaces.Auth;
using Identity_Provider.Service.Interfaces.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace Identity_Provider.Service.Services.FileService;


public class FileService : IFileService
{
    private readonly string FILES = "files";
    private readonly string ROOTPATH;
    private readonly IIdentityService _identityService;

    public FileService(IWebHostEnvironment env, IIdentityService identityService)
    {
        ROOTPATH = env.WebRootPath;
        this._identityService = identityService;
    }

    public async Task<List<FileViewModel>> GetAllAsync()
    {
        string _filesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        List<FileViewModel> list = new List<FileViewModel>();

        var imagePaths = Directory.GetFiles(_filesPath, "*.*");



        foreach (var item in imagePaths)
        {
            FileViewModel fileViewModel = new FileViewModel();
            fileViewModel.fileName = item;
            list.Add(fileViewModel);

        }

        return await Task.FromResult(list);
    }

    public async Task<bool> DeleteFileAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, "images\\" + subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });

            return true;
        }
        else return false;
    }

    public static string ConvertToMaskAndSave(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            throw new ArgumentException("Image path cannot be null or empty.", nameof(imagePath));
        }

        using (Bitmap sourceImage = new Bitmap(imagePath))
        {
            Bitmap maskImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);

            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    Color pixelColor = sourceImage.GetPixel(x, y);

                    int grayscaleValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);

                    maskImage.SetPixel(x, y, Color.FromArgb(grayscaleValue, 255, 255, 255));
                }
            }

            string outputFileName = Path.GetFileNameWithoutExtension(imagePath) + "_mask.png"; // Mask fayl nomini o'zgartirish
            string outputFolderPath = Path.GetDirectoryName(imagePath); // Fayl manzili uchun folder manzilini olish
            string outputPath = Path.Combine(outputFolderPath, outputFileName); // To'liq chiqarish manzili

            maskImage.Save(outputPath, ImageFormat.Png);

            return outputPath; // Yangi mask rasmi manzili
        }
    }


    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }

        if (FILES == null)
        {
            throw new FilesNotFoundExeption();
        }

        if (ROOTPATH == null)
        {
            throw new RootpathNotFoundExeption();
        }

        string role = _identityService.Role;

        if (role == "User" || role == "Admin")
        {
            string originalFileName = file.FileName;
            string fileExtension = Path.GetExtension(originalFileName);

            string newFileName = MediaHelper.MakeFileName(originalFileName) + fileExtension;
            string subpath = Path.Combine(FILES, newFileName);
            string path = Path.Combine(ROOTPATH, subpath);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return subpath;
        }
        else
        {
            string originalFileName = file.FileName;
            string fileExtension = Path.GetExtension(originalFileName); 

            string newFileName = MediaHelper.MakeFileName(originalFileName) + fileExtension;
            string subpath = Path.Combine(FILES, newFileName);
            string path = Path.Combine(ROOTPATH, subpath);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return subpath;

        }
    }


    public async Task<Stream> GetFileStreamAsync(string fileName)
    {
        string filePath = Path.Combine(ROOTPATH, FILES, fileName);

        string role = _identityService.Role;

        if (role == "User"|| role=="Admin")
        {
            if (File.Exists(filePath))
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }

            return null;
        }
        else
        {
            string maskImagePath =  ConvertToMaskAndSave(filePath);

            if (File.Exists(maskImagePath))
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }

            return null;
        }

    }
}

