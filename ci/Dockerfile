FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Alchemy.WebAPI/Alchemy.WebAPI.csproj", "src/Alchemy.WebAPI/"]
RUN dotnet restore "src/Alchemy.WebAPI/Alchemy.WebAPI.csproj"

COPY . .
WORKDIR "/src/src/Alchemy.WebAPI"

RUN dotnet build "Alchemy.WebAPI.csproj" -c Release -o /app/build
RUN dotnet publish "Alchemy.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
 
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Alchemy.WebAPI.dll"]
