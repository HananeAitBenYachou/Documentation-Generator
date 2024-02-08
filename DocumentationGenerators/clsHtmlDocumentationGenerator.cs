using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.DocumentationGenerators
{
    public class clsHtmlDocumentationGenerator
    {
        private static Assembly Assembly { get; set; }

        public static bool GenerateDocumentation(string assemblyPath, string generatedfilePath)
        {
            try
            {
                StringBuilder documentationBuilder = new StringBuilder();

                Assembly = Assembly.LoadFrom(assemblyPath);

                documentationBuilder.AppendLine($"<h1>📚 Assembly Name: {Assembly.GetName().Name}</h1>");
                documentationBuilder.AppendLine();

                foreach (Type type in Assembly.GetTypes())
                {
                    if (type.GetMethods().Any(method => method.DeclaringType == type && !method.IsSpecialName))
                    {
                        _GenerateClassDocumentation(type, ref documentationBuilder);

                        documentationBuilder.AppendLine("<hr>");
                        documentationBuilder.AppendLine();
                    }
                }

                generatedfilePath += ".html";
                File.WriteAllText(generatedfilePath, documentationBuilder.ToString());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void _GenerateClassDocumentation(Type type, ref StringBuilder documentationBuilder)
        {
            documentationBuilder.AppendLine($"<h2>📌 Class Name: {type.Name}</h2>");
            documentationBuilder.AppendLine("<h3>📋 Class Methods:</h3>");

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

                    documentationBuilder.AppendLine("<hr>");
                    documentationBuilder.AppendLine();

                    documentationBuilder.AppendLine($"<h4>🔧 Method Name: {method.Name}</h4>");
                    documentationBuilder.AppendLine($"<h4>📝 Parameters List:</h4><pre>{_GetMethodParametersList(method.GetParameters())}</pre>");
                    documentationBuilder.AppendLine($"<h4>💡 Summary:</h4><p>{summary}</p>");
                    documentationBuilder.AppendLine($"<h4>🎯 Return:</h4><p>{returnVal}</p>");
                }
            }
        }

        private static string _GetMethodParametersList(ParameterInfo[] parameters)
        {
            if (parameters.Length == 0)
                return "This method does not take any parameters";

            return string.Join("", parameters.Select((parameter, index) => $"\n    {index + 1}. {parameter.ParameterType} {parameter.Name}"));
        }
    }
}
