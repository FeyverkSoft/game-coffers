FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.3-alpine3.9

COPY ./build /app

WORKDIR /app

ENTRYPOINT ["dotnet", "Coffers.Public.WebApi.dll"]