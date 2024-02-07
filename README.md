# Documentation Generator

## Overview
This project provides a user-friendly and reliable tool for automatically generating comprehensive Markdown-formatted documentation for C# class libraries. By leveraging reflection and custom attributes, it extracts essential information (method signatures, comments, summaries, return values) to create clear and informative documentation.

## Key Features
- Automatic Documentation Generation: Simplifies documentation creation without manual effort.
- Markdown Format: Output is human-readable, easy to edit, and compatible with GitHub, wikis, and other platforms.
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
4. Click "Generate" Button: Initiate the documentation generation process.
5. Save Documentation: Choose a location and filename to save the generated Markdown file.

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
