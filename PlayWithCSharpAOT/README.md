# Publish Release

## Linux

`dotnet publish -r linux-x64 -c Release -p:UseCoreRT=true`
`strip --strip-debug <executableName>`

## Windows

`dotnet publish -r win-x64 -c Release -p:UseCoreRT=true`

## Mac

`dotnet publish -r osx-x64 -c Release -p:UseCoreRT=true`
`strip <executableName>`
