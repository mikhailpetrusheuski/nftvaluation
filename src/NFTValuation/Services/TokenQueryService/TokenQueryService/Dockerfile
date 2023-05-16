FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/TokenQueryService/TokenQueryService/TokenQueryService.csproj", "TokenQueryService/"]
RUN dotnet restore "Services/TokenQueryService/TokenQueryService/TokenQueryService.csproj"
COPY . .
WORKDIR "/src/TokenQueryService"
RUN dotnet build "TokenQueryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TokenQueryService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TokenQueryService.dll"]
