FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Easy.Commerce.Presentation.Web/Easy.Commerce.Presentation.Web.csproj src/Easy.Commerce.Presentation.Web/
RUN dotnet restore src/Easy.Commerce.Presentation.Web/Easy.Commerce.Presentation.Web.csproj
COPY . .
WORKDIR /src/src/Easy.Commerce.Presentation.Web
RUN dotnet build Easy.Commerce.Presentation.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Easy.Commerce.Presentation.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Easy.Commerce.Presentation.Web.dll"]
