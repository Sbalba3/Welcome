namespace Welcome.Helpers
{
    public static class DocumentSettinga
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath =Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\Files\\" + folderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filePath=Path.Combine(folderPath, fileName);
            using var fs=new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;

        }
        public static void DeleteFile(string fileName, string folderName) 
        { 
            if(fileName is not null && folderName is not null) 
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\"+folderName,fileName);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            
        
        }
    }
}
