FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:5000
# EXPOSE 5555

COPY ["Meblex.API/nginx.conf.sigil", "/app/"]

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Meblex.API/Meblex.API.csproj", "Meblex.API/"]
COPY ["Meblex.API/nginx.conf.sigil", "/app/"]
RUN dotnet restore "Meblex.API/Meblex.API.csproj"
COPY . .
WORKDIR "/src/Meblex.API"
RUN dotnet build "Meblex.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Meblex.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Meblex.API.dll"]