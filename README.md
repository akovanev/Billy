# Billy

[![](https://img.shields.io/badge/net%20core-3.1-blue?logo=visual-studio)](https://dotnet.microsoft.com/download)

Billy is a sort of educational exercise. At this moment it counts the string occurrences in the file. The string represents the filename without its extension.

Billy works with the files saved in ASCII and UTF8 encodings. For now Billy may not recognize what to do with UTF16 but you can try your luck :) 

Billy is also okay with large files 10GB+, despite it may take some time for search. The tests solution contains a simple  [FileGenerator](https://github.com/akovanev/Billy/blob/main/tests/Billy.Utilities.FileGenerator/FileGenerator.cs) which can be used to create the random data.

To test Billy locally:
* Open PowerShell and execute `Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass`to allow scripts.
* Download the [BillySetup.ps1 ](https://github.com/akovanev/Billy/blob/main/scripts/BillySetup.ps1) script and run it from PowerShell `.\BillySetup.ps1`. 