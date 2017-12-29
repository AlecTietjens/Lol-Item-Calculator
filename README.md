# Prereqs
### The following need installed and set up
1. .NET Core 2.x (also need the CLI)
2. Node 9.3.0 / NPM

# To run
1. Clone repo
2. In repo directory, run `npm install`
3. Run `ASPNETCORE_ENVIRONMENT=Development dotnet watch run` (for Windows, setting the environment variable will probably be different)
4. Site can now be accessed at http://localhost:5000

# To get League data

# Development Notes
1. Site being used for data retrieval is https://developer.riotgames.com/api-methods/#lol-static-data-v3
2. Browser reloading for hot modules currently doesn't work for html edits 
3. Stack is .NET Core and Angular
TBD