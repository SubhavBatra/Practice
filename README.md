dotnet new console -n StudentApp
cd StudentApp
dotnet run


dotnet new mstest -n StudentApp.Tests
dotnet sln add StudentApp.Tests/StudentApp.Tests.csproj
dotnet add StudentApp.Tests/StudentApp.Tests.csproj reference StudentApp/StudentApp.csproj
