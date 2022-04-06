from  mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

COPY . .

RUN dotnet dev-certs https --trust

RUN dotnet add package MongoDB.Driver
