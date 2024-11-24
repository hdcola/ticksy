using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ticksy
{
    public class XmlHelper
    {
        public static void Create(string filePath, Dictionary<string, string> content)
        {
            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Create the XML content
            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");

                foreach (KeyValuePair<string, string> entry in content)
                {
                    writer.WriteElementString(entry.Key, entry.Value);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static bool TryGetContent(string filePath, out Dictionary<string, string> content)
        {
            content = new Dictionary<string, string>();

            try
            {
                content = GetContent(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static Dictionary<string, string> GetContent(string filePath)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                reader.MoveToContent();

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        content[reader.Name] = reader.ReadElementContentAsString();
                    }
                }
            }
            return content;
        }
    }
}
