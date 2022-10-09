FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR src
COPY . .
RUN ls
RUN dotnet restore "Cyber.Api/Cyber.Api.csproj"
WORKDIR "/src/Cyber.Api"
RUN dotnet build "Cyber.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cyber.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cyber.Api.dll"]
