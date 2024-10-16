namespace MahasDemo.PL.Models.helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1- Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProjectFiles\\", folderName);

            // 2- Get File Name and Make it unique
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- Get File Path
            var filePath = Path.Combine(folderPath,fileName);

            // 4- SaveFile as Stream
            using var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProjectFiles\\", folderName,fileName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
