# Installation Guide

## Prerequisites

Before running the project, make sure you have the following installed:

- **Windows 10/11**

- **Visual Studio 2022 or later**  
  Download: [https://visualstudio.microsoft.com/vs/](https://visualstudio.microsoft.com/vs/)  
  During installation, select the workload:  
  ✅ **.NET Desktop Development**

- **.NET SDK** (recommended version: .NET 8.0 or 9.0 Preview)  
  Download:  
  [.NET 8.0 SDK (LTS)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
  [.NET 9.0 SDK (Preview)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
  > ✅ After installation, verify it using:  
  ```cmd
  dotnet --version
  ```

- **Git** (optional, for cloning the repository)  
  Download: [https://git-scm.com/downloads](https://git-scm.com/downloads)  
  > ✅ After installation, you can verify with:  
  ```cmd
  git --version
  ```

- **Newtonsoft.Json package** (for JSON serialization)  
  Install via NuGet Package Manager Console in Visual Studio:  
  ```powershell
  Install-Package Newtonsoft.Json
  ```
  Or via .NET CLI:  
  ```bash
  dotnet add package Newtonsoft.Json
  ```
