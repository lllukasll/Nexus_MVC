namespace Nexus_MVC.Helpers
{
    public static class Errors
    {
        public static string UploadBothFiles()
        { return $"Please upload both files."; }

        public static string AllowedXml()
        { return $"Only XML files are allowed."; }

        public static string DeserializeError(string error) 
            { return $"Failed to deserialize XML: {error}"; }

        public static string XmlNullOrEmpty()
            { return $"Input XML is empty or null."; }

        public static string XmlNullOrEmpty(string name)
            { return $"{name} data is invalid or empty."; }

        public static string MissingProducts(List<int> missingProducts)
            { return $"Products with IDs {string.Join(", ", missingProducts)} not found in price list."; }

        public static string MissingProduct(int missingProductId)
            { return $"Product with ID {missingProductId} not found in price list.";  }

        public static string ProcessingError(string message)
            { return $"An error occurred while processing the files: {message}";  }

        public static string SerializeError(string message)
            { return $"An error occurred while trying to generate invoice.xml: {message}"; }

	}
}
