# Byte Press API

Backend todo app API with auth.

## Prerequisites

 * .NET 8 (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
 * Sql Server (https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
 * Ngrok (https://ngrok.com/) for use with mobile app

## Getting Started

1. Clone the repo `git clone https://github.com/kylemccullen/byte-press-api.git`
2. Run the application
    - Standalone `dotnet run --project=API` or `make run`
    - With Expo Mobile App (https://github.com/kmccullen/byte-press-mobile)
      - Run the following two commands simultaneously
        - Run app `dotnet run --project=API -- --urls "http://0.0.0.0:5100"` or `make run`
        - Run ngrok `ngrok http 5100` or `make t`
          - You will need the forwarding url from ngrok to run the mobile app (Ex: `https://<random-url-key>.ngrok-free.app`)
3. Access swagger
    - Standalone `https://localhost:7002/swagger/index.html`
    - With Expo `https://<random-url-key>.ngrok-free.app/swagger/index.html`
