FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Swallow.WebApi/Swallow.WebApi.csproj", "Swallow.WebApi/"]
COPY ["Swallow.Core/Swallow.Core.csproj", "Swallow.Core/"]
COPY ["Swallow.DataAccessLayer/Swallow.DataAccessLayer.csproj", "Swallow.DataAccessLayer/"]
COPY ["Swallow.DataCollector/Swallow.DataCollector.csproj", "Swallow.DataCollector/"]
RUN dotnet restore "Swallow.WebApi/Swallow.WebApi.csproj"
COPY . .
WORKDIR "/src/Swallow.WebApi"
RUN dotnet build "Swallow.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Swallow.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Swallow.WebApi.dll"]
