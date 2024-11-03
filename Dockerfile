# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY thoughtBubbles-server/*.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port your app runs on
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "thoughtBubbles-server.dll"]
