FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["acdt-project/acdt-project.csproj", "acdt-project/"]
RUN dotnet restore "acdt-project/acdt-project.csproj"
COPY . .
WORKDIR "/src/acdt-project"
RUN dotnet build "acdt-project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "acdt-project.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations add InitialCreate
RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "acdt-project.dll"]
