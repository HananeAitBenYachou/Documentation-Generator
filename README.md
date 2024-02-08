# Documentation Generator

## Overview
This project provides a user-friendly tool for automatically generating documentation for C# class libraries. It supports multiple output formats, including XML, HTML, and Markdown. By leveraging reflection and custom attributes, the tool extracts essential information such as method signatures, comments, summaries, and return values to create clear and informative documentation.

## Key Features
- Automatic Documentation Generation: Simplifies documentation creation without manual effort.
- Multiple Format Support: Choose from XML, HTML, and Markdown for the generated documentation.
- Customizable Output: Specify a custom file path for the generated documentation.
- User-Friendly Interface: Select the target assembly and save documentation with ease.
- Input Validation: Ensures valid class library (DLL) file paths.
- DocumentationAttribute Support: Enables method summaries and return values through a custom attribute.
- Error Handling: Handles invalid inputs and file operations gracefully.
- Reflection-Based Exploration: Dynamically navigates the class library structure.

## Usage
1. Compile the Project: Build the project using Visual Studio or .NET Framework tools.
2. Run the Application: Launch the executable (.exe) file.
3. Specify Assembly Path: Enter the full path to the target C# class library (DLL) file.
4. Select File Format: Choose the desired file format for the generated documentation.
5. Click "Generate" Button: Initiate the documentation generation process.
6. Save Documentation: Choose a location and filename to save the generated Markdown file.

## Additional Information
### DocumentationAttribute Custom Attribute
This project relies on a custom attribute named `DocumentationAttribute` to provide summary and return value descriptions for methods. This attribute must be applied to methods in the target class library to enable documentation generation.

#### Structure of DocumentationAttribute:
```csharp
[AttributeUsage(AttributeTargets.Method)]
public class DocumentationAttribute : Attribute
{
    public string Summary { get; set; }
    public string Return { get; set; }

    public DocumentationAttribute(string summary, string returnVal)
    {
        Summary = summary;
        Return = returnVal;
    }
}
```

#### Example Usage:
```csharp
public class MyClass
{
    [Documentation("Calculates the sum of two numbers.", "The sum of the provided numbers.")]
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```
## Technology Used
- C# .NET Framework
- WinForms (Desktop Interface)
- Visual Studio (IDE)

## Demo 
You can also watch a demo of the Documentation Generator in action by clicking the link below:

[Demo Video](https://drive.google.com/file/d/1jioMRtx3zuvP4miUVDrkm4mRRsx3rR3Y/view?usp=drive_link)
