FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Denso.HotSheet.Web.Host/Denso.HotSheet.Web.Host.csproj", "src/Denso.HotSheet.Web.Host/"]
COPY ["src/Denso.HotSheet.Web.Core/Denso.HotSheet.Web.Core.csproj", "src/Denso.HotSheet.Web.Core/"]
COPY ["src/Denso.HotSheet.Application/Denso.HotSheet.Application.csproj", "src/Denso.HotSheet.Application/"]
COPY ["src/Denso.HotSheet.Core/Denso.HotSheet.Core.csproj", "src/Denso.HotSheet.Core/"]
COPY ["src/Denso.HotSheet.EntityFrameworkCore/Denso.HotSheet.EntityFrameworkCore.csproj", "src/Denso.HotSheet.EntityFrameworkCore/"]
WORKDIR "/src/src/Denso.HotSheet.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Denso.HotSheet.Web.Host", "src/Denso.HotSheet.Web.Host"]
COPY ["src/Denso.HotSheet.Web.Core", "src/Denso.HotSheet.Web.Core"]
COPY ["src/Denso.HotSheet.Application", "src/Denso.HotSheet.Application"]
COPY ["src/Denso.HotSheet.Core", "src/Denso.HotSheet.Core"]
COPY ["src/Denso.HotSheet.EntityFrameworkCore", "src/Denso.HotSheet.EntityFrameworkCore"]
WORKDIR "/src/src/Denso.HotSheet.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Denso.HotSheet.Web.Host.dll"]
