FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9

COPY ./build /app

WORKDIR /app

ENTRYPOINT ["dotnet", "Coffers.Public.WebApi.dll"]