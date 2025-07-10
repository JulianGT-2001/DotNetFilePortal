FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /app

COPY . .
RUN chmod +x /docker-entrypoint.sh
WORKDIR /app/WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT [ "/docker-entrypoint.sh" ]
CMD [ "dotnet", "WebApi.dll" ]