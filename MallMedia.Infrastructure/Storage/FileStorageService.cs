

using MallMedia.Domain.Interfaces;

namespace MallMedia.Infrastructure.Storage;

internal class FileStorageService : IFileStorageService
{
    private readonly string _baseFolderPath;

    public FileStorageService(string baseFolderPath = "wwwroot")
    {
        _baseFolderPath = baseFolderPath;
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderPath = "uploads")
    {
        var uniqueFileName = GenerateUniqueFileName(fileName);

        var fullPath = Path.Combine(_baseFolderPath, folderPath, uniqueFileName);

        var directoryPath = Path.GetDirectoryName(fullPath);
        if (directoryPath != null && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (var fileStreamToSave = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await fileStream.CopyToAsync(fileStreamToSave);
        }

        return Path.Combine(folderPath, uniqueFileName);
    }

    private string GenerateUniqueFileName(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var uniqueId = Guid.NewGuid().ToString();
        return $"{fileNameWithoutExtension}_{uniqueId}{fileExtension}";
    }
}
