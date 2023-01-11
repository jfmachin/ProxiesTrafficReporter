FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y xorg openbox libnss3 libasound2
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PTR.API/PTR.API.csproj", "PTR.API/"]
RUN dotnet restore "PTR.API/PTR.API.csproj"
COPY . .
WORKDIR "/src/PTR.API"
RUN dotnet build "PTR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PTR.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PTR.API.dll"]