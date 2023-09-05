FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR webapi

EXPOSE 80
EXPOSE 5098

#COPIAR LOS ARCHIVOS DE PROYECTO
COPY ./*.csproj ./
RUN dotnet restore

#COPIAR TODO EL RESTRO DE LAS COSAS
COPY . .
RUN dotnet publish -c Release -o out

#BUILD DE LA IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /webapi
COPY --from=build /webapi/out .
ENTRYPOINT ["dotnet","we-working-api.dll"]
