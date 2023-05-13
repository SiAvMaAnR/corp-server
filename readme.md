# Migration template
`dotnet ef --startup-project ../CSN.WebApi/ migrations add *migration-name*`

`dotnet ef --startup-project ../CSN.WebApi/ database update`

# DotNet template
`dotnet sln add *project path*`

`dotnet add reference *project path*`

# Process kill
`sudo kill -9 $(sudo lsof -t -i:port)`




