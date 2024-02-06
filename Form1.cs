using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentationGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtAssemblyPath_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtAssemblyPath.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAssemblyPath, 
                    "Please provide the path of the class library for which you would like to generate the documentation.");
            }

            else if (!File.Exists(txtAssemblyPath.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAssemblyPath,
                    "The path you provided does not exists ! please enter a valid path !");
            }

            else if(!Regex.IsMatch(txtAssemblyPath.Text, @"\.dll$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAssemblyPath,
                    "The provided path does not point to a class library (DLL) file. Please make sure to provide a valid path to a class library.");
            } 
            
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAssemblyPath, null);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show(@"Please provide the path of the class library for which you would like to generate the documentation.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _GenerateDocumentation();
        }

        private void _GenerateDocumentation()
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                clsDocumentationGenerator docsGenerator = new clsDocumentationGenerator(txtAssemblyPath.Text.Trim());
            
                if (docsGenerator.GenerateDocumentation(saveFileDialog1.FileName))
                {
                    MessageBox.Show("Documentation generated and saved successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to generate documentation. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
