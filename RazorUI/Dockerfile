FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RazorUI/RazorUI.csproj", "RazorUI/"]
RUN dotnet restore "RazorUI/RazorUI.csproj"
COPY . .
WORKDIR "/src/RazorUI"
RUN dotnet build "RazorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RazorUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RazorUI.dll"]
