#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Phoenix.Web/Phoenix.Web.csproj", "Phoenix.Web/"]
COPY ["Phoenix.Identity/Phoenix.Identity.csproj", "Phoenix.Identity/"]
COPY ["Phoenix.Infrastructure/Phoenix.Infrastructure.csproj", "Phoenix.Infrastructure/"]
RUN dotnet restore "Phoenix.Web/Phoenix.Web.csproj"
COPY . .
WORKDIR "/src/Phoenix.Web"
RUN dotnet build "Phoenix.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phoenix.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phoenix.Web.dll"]