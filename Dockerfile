#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/App.csproj", "App/"]
COPY ["App.Bll/App.BLL.csproj", "App.Bll/"]
COPY ["App.IDal/App.IDAL.csproj", "App.IDal/"]
COPY ["App.Model/App.Model.csproj", "App.Model/"]
COPY ["App.Dal/App.DAL.csproj", "App.Dal/"]
RUN dotnet restore "App/App.csproj"
COPY . .
WORKDIR "/src/App"
RUN dotnet build "App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]