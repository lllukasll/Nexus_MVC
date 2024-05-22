using System.Xml.Schema;
using System.Xml;

namespace Nexus_MVC.Helpers
{
    public static class XmlValidator
    {
        public static void ValidateXml(string xmlContent, string xsdPath)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(new StringReader(File.ReadAllText(xsdPath))));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            xmlDoc.Schemas.Add(schemas);

            xmlDoc.Validate((sender, args) =>
            {
                if (args.Severity == XmlSeverityType.Error || args.Severity == XmlSeverityType.Warning)
                {
                    throw new Exception($"XML validation error: {args.Message}");
                }
            });
        }

    }
}
