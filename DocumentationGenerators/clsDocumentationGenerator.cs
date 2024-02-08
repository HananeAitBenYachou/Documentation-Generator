using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator
{
    public class clsDocumentationGenerator
    {
        public enum enFileFormats
        {
            Markdown,
            Html,
            Xml
        }

        public delegate bool GenerateDocumentationAction(string assemblyPath, string generatedfilePath);

        private GenerateDocumentationAction _GenerateDocumentationAction;

        public clsDocumentationGenerator(GenerateDocumentationAction action)
        {
            _GenerateDocumentationAction = action;
        }

        public bool GenerateDocumentation(string assemblyPath, string generatedfilePath)
        {
            return _GenerateDocumentationAction(assemblyPath, generatedfilePath);
        }
    }
}
