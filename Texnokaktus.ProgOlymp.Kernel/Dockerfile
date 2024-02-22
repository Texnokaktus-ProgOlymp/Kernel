FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GHPAT
WORKDIR /src
COPY ["Texnokaktus.ProgOlymp.Kernel/Texnokaktus.ProgOlymp.Kernel.csproj", "Texnokaktus.ProgOlymp.Kernel/"]
COPY ["modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts.csproj", "modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/"]
COPY ["Texnokaktus.ProgOlymp.Kernel.DataAccess/Texnokaktus.ProgOlymp.Kernel.DataAccess.csproj", "Texnokaktus.ProgOlymp.Kernel.DataAccess/"]
COPY ["Texnokaktus.ProgOlymp.Kernel.Infrastructure/Texnokaktus.ProgOlymp.Kernel.Infrastructure.csproj", "Texnokaktus.ProgOlymp.Kernel.Infrastructure/"]
COPY ["Texnokaktus.ProgOlymp.Kernel.Notifications.Email/Texnokaktus.ProgOlymp.Kernel.Notifications.Email.csproj", "Texnokaktus.ProgOlymp.Kernel.Notifications.Email/"]
COPY ["Texnokaktus.ProgOlymp.Kernel.Options/Texnokaktus.ProgOlymp.Kernel.Options.csproj", "Texnokaktus.ProgOlymp.Kernel.Options/"]
COPY ["Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets/Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.csproj", "Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets/"]
RUN dotnet nuget add source --username kav128 --password $GHPAT --store-password-in-clear-text --name "GitHub TexnoKaktus-Progolymp" https://nuget.pkg.github.com/Texnokaktus-ProgOlymp/index.json
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
