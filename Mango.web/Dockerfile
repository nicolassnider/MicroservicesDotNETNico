#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mango.web/Mango.web.csproj", "Mango.web/"]
RUN dotnet restore "Mango.web/Mango.web.csproj"
COPY . .
WORKDIR "/src/Mango.web"
RUN dotnet build "Mango.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mango.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mango.web.dll"]