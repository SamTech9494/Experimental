FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Experimental/Experimental.csproj", "Experimental/"]
RUN dotnet restore "Experimental/Experimental.csproj"
COPY . .
WORKDIR "/src/Experimental"
RUN dotnet build "Experimental.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Experimental.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Experimental.dll"]
