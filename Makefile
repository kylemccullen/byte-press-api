PORT = 5100

run:
	dotnet run --project=API

expo:
	dotnet run --project=API -- --urls "http://0.0.0.0:$(PORT)"

t:
	ngrok http $(PORT)
