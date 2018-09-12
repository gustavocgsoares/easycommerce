FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ./Services.Web.Api/Services.Web.Api.csproj ./src/Services.Web.Api/
COPY ./Domain/Domain.csproj ./src/Domain/
COPY ./CrossCutting/CrossCutting.csproj ./src/CrossCutting/
COPY ./Data.MongoDb/Data.MongoDb.csproj ./src/Data.MongoDb/
RUN dotnet restore src/Services.Web.Api/Services.Web.Api.csproj
COPY . .
WORKDIR /src/src/Services.Web.Api
RUN dotnet build Services.Web.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Services.Web.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Easy.Commerce.Services.Web.Api.dll"]
