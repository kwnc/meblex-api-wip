FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5555

ADD /home/dokku/meblex-api/nginx.conf.sigil .


FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Meblex.API/Meblex.API.csproj", "Meblex.API/"]
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