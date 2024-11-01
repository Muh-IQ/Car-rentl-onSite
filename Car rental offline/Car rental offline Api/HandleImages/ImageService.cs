using Microsoft.AspNetCore.Mvc;

namespace Car_rental_offline_Api.HandleImages
{
    public class ImageService
    {
        public static async Task<string> UploadImageAsync(IFormFile imageFile , string fileName)
        {
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return null;

            try
            {
                // Directory where files will be uploaded
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Images";

                var filePath = Path.Combine(uploadDirectory, fileName);

                // Ensure the uploads directory exists, create if it doesn't
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Return the file path as a response
                return filePath;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }
        public static async Task<string> UpdateImageAsync(IFormFile newImageFile, string NewFileName,string existingFileName)
        {
            // تحقق من الملف المرفوع
            if (newImageFile == null || newImageFile.Length == 0)
                return null;

            try
            {
                // مسار المجلد الذي تُحفظ فيه الصور
                var uploadDirectory = @"C:\Users\Asus\Desktop\Projects\Car rental\Car rental online\Car rental by EF\Images";

                // المسار الكامل للصورة القديمة
                var existingFilePath = Path.Combine(uploadDirectory, existingFileName);
                var newFilePath = Path.Combine(uploadDirectory, NewFileName);
                // حذف الصورة القديمة إذا كانت موجودة
                if (File.Exists(existingFilePath))
                {
                    File.Delete(existingFilePath);
                }
                // حفظ الصورة الجديدة
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(stream);
                }

                // إرجاع مسار الصورة الجديدة
                return newFilePath;
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا إذا لزم الأمر
                return null;
            }
        }

    }
}
