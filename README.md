dotnet new console -n StudentApp<br>
cd StudentApp<br>
dotnet run<br>

. \
dotnet new mstest -n StudentApp.Tests<br>
dotnet sln add StudentApp.Tests/StudentApp.Tests.csproj<br>
dotnet add StudentApp.Tests/StudentApp.Tests.csproj reference StudentApp/StudentApp.csproj<br>
