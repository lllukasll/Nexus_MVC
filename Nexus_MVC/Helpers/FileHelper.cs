namespace Nexus_MVC.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> ReadFileContentAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static bool IsXmlFile(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            return string.Equals(fileExtension, ".xml", StringComparison.OrdinalIgnoreCase);
        }

        public static void ValidateXml(string xmlContent, string schemaPath)
        {
            XmlValidator.ValidateXml(xmlContent, schemaPath);
        }
    }
}
