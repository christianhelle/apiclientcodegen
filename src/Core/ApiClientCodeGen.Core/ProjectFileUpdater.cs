using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public class ProjectFileUpdater
    {
        private readonly string file;
        private readonly XDocument xml;

        public ProjectFileUpdater(string projectFile)
            : this(XDocument.Load(projectFile))
        {
            file = projectFile;
        }

        public ProjectFileUpdater(XDocument xml)
        {
            this.xml = xml ?? throw new ArgumentNullException(nameof(xml));
        }

        public XDocument UpdatePropertyGroup(IReadOnlyDictionary<string, string> properties)
        {
            var propertyGroups = xml
                .Elements("Project")
                .Elements("PropertyGroup")
                .Elements()
                .ToList();

            foreach (var property in properties)
            {
                if (propertyGroups.All(c => c.Name != property.Key))
                {
                    propertyGroups.Add(
                        new XElement(property.Key, property.Value));
                }
                else
                {
                    propertyGroups
                        .First(c => c.Name == property.Key)
                        .Value = property.Value.ToString();
                }
            }

            xml.Root?.Element("PropertyGroup")?.ReplaceNodes(propertyGroups);

            if (file != null)
                xml.Save(file);

            return xml;
        }
    }
}