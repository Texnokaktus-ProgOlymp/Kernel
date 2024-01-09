FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Texnokaktus.ProgOlymp.Kernel/Texnokaktus.ProgOlymp.Kernel.csproj", "Texnokaktus.ProgOlymp.Kernel/"]
RUN dotnet restore "Texnokaktus.ProgOlymp.Kernel/Texnokaktus.ProgOlymp.Kernel.csproj"
COPY . .
WORKDIR "/src/Texnokaktus.ProgOlymp.Kernel"
RUN dotnet build "Texnokaktus.ProgOlymp.Kernel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Texnokaktus.ProgOlymp.Kernel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Texnokaktus.ProgOlymp.Kernel.dll"]
