# Prepare data

Download backup `AdventureWorks2022.bak` from https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks and place in this folder.

This backup is used to create Docker image with backup inside.

# Build image

Run command in the same directory

``` powershell
docker build -t sqlserver-adventure-works-image .
```

# Run image

``` powershell
docker run --name 'sql-aw1' -p 1477:1433 -d sqlserver-adventure-works-image
```

# Connect

- Addres type `.,1477`
- User: `sa`
- Pass: the same from ENV
- Set in connection string `TrustServerCertificate=Yes`
