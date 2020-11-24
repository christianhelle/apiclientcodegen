dotnet tool install --global rapicgen
export PATH="$PATH:/Users/runner/.dotnet/tools"

pwsh -Command Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json

rapicgen autorest Swagger.json GeneratedCode ./AutoRestOutput.cs
rapicgen nswag Swagger.json GeneratedCode ./NSwagOutput.cs
rapicgen swagger Swagger.json GeneratedCode ./SwaggerOutput.cs
rapicgen openapi Swagger.json GeneratedCode ./OpenApiOutput.cs