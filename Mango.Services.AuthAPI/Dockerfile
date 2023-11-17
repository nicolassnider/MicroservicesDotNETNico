#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mango.Services.AuthAPI/Mango.Services.AuthAPI.csproj", "Mango.Services.AuthAPI/"]
RUN dotnet restore "Mango.Services.AuthAPI/Mango.Services.AuthAPI.csproj"
COPY . .
WORKDIR "/src/Mango.Services.AuthAPI"
RUN dotnet build "Mango.Services.AuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mango.Services.AuthAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mango.Services.AuthAPI.dll"]