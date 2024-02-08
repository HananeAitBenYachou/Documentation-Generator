using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DocumentationGenerator.DocumentationGenerators
{
    public class clsXmlDocumentationGenerator
    {
        private static Assembly Assembly { get; set; }

        public static bool GenerateDocumentation(string assemblyPath, string generatedfilePath)
        {
            try
            {
                XmlDocument documentation = new XmlDocument();
                XmlElement rootElement = documentation.CreateElement("assembly");
                documentation.AppendChild(rootElement);

                Assembly = Assembly.LoadFrom(assemblyPath);

                XmlElement nameElement = documentation.CreateElement("name");
                nameElement.InnerText = Assembly.GetName().Name;
                rootElement.AppendChild(nameElement);

                foreach (Type type in Assembly.GetTypes())
                {
                    if (type.GetMethods().Any(method => method.DeclaringType == type && !method.IsSpecialName))
                    {
                        XmlElement classElement = _GenerateClassDocumentation(type, documentation);
                        rootElement.AppendChild(classElement);
                    }
                }

                documentation.Save(generatedfilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static XmlElement _GenerateClassDocumentation(Type type, XmlDocument documentation)
        {
            XmlElement classElement = documentation.CreateElement("class");
            classElement.SetAttribute("name", type.Name);

            XmlElement methodsElement = documentation.CreateElement("methods");
            classElement.AppendChild(methodsElement);

            foreach (MethodInfo method in type.GetMethods())
            {
                IEnumerable<object> attributes = method.GetCustomAttributes(false).Where(attribute => attribute.GetType().Name == "DocumentationAttribute");

                if (attributes.Count() > 0)
                {
                    object attribute = attributes.ElementAt(0);

                    PropertyInfo summaryProperty = attribute.GetType().GetProperty("Summary");
                    PropertyInfo returnProperty = attribute.GetType().GetProperty("Return");

                    string summary = summaryProperty.GetValue(attribute) as string;
                    string returnVal = returnProperty.GetValue(attribute) as string;

                    XmlElement methodElement = documentation.CreateElement("method");
                    methodElement.SetAttribute("name", method.Name);

                    XmlElement parametersElement = documentation.CreateElement("parameters");
                    methodElement.AppendChild(parametersElement);

                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        XmlElement parameterElement = documentation.CreateElement("parameter");
                        parameterElement.SetAttribute("name", parameter.Name);
                        parameterElement.SetAttribute("type", parameter.ParameterType.ToString());

                        parametersElement.AppendChild(parameterElement);
                    }

                    XmlElement summaryElement = documentation.CreateElement("summary");
                    summaryElement.InnerText = summary;
                    methodElement.AppendChild(summaryElement);

                    XmlElement returnElement = documentation.CreateElement("return");
                    returnElement.InnerText = returnVal;
                    methodElement.AppendChild(returnElement);

                    methodsElement.AppendChild(methodElement);
                }
            }

            return classElement;
        }
    
    }
}
