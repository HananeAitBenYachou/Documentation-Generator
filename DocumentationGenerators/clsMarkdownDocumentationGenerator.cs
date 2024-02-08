using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.DocumentationGenerators
{
    public class clsMarkdownDocumentationGenerator
    {
        private static Assembly Assembly { get; set; }

        public static bool GenerateDocumentation(string assemblyPath, string generatedfilePath)
        {
            try
            {
                StringBuilder documentationBuilder = new StringBuilder();

                Assembly = Assembly.LoadFrom(assemblyPath);

                documentationBuilder.AppendLine($"# 📚 Assembly Name: {Assembly.GetName().Name}\n");
                documentationBuilder.AppendLine();

                foreach (Type type in Assembly.GetTypes())
                {
                    if (type.GetMethods().Any(method => method.DeclaringType == type && !method.IsSpecialName))
                    {
                        _GenerateClassDocumentation(type, ref documentationBuilder);

                        documentationBuilder.AppendLine("---");
                        documentationBuilder.AppendLine();
                    }
                }

                generatedfilePath += ".md";
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
            documentationBuilder.AppendLine($"## 📌 Class Name : *{type.Name}*");
            documentationBuilder.AppendLine("### 📋 Class Methods : \n");

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

                    documentationBuilder.AppendLine("---");
                    documentationBuilder.AppendLine();

                    documentationBuilder.AppendLine($"- #### 🔧 Method Name : {method.Name}\n");
                    documentationBuilder.AppendLine($"- #### 📝 Parameters List :\n    {_GetMethodParametersList(method.GetParameters())}\n");
                    documentationBuilder.AppendLine($"- #### 💡 Summary :\n    {summary}\n");
                    documentationBuilder.AppendLine($"- #### 🎯 Return :\n    {returnVal}\n");

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
