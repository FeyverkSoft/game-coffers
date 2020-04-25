FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

COPY /build /app

WORKDIR /app

ENTRYPOINT ["dotnet", "Coffers.Public.WebApi.dll"]