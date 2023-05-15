param (
    [Parameter(Mandatory=$true)]
    [string]$ServiceName
)

$ComposeFile = "..\src\docker-compose.yml"
$ComposeOverrideFile = "..\src\docker-compose.override.yml"
$EnvFile = "..\src\.env"

# Build the specific service
Write-Host "Building $ServiceName service..."
docker-compose -f $ComposeFile -f $ComposeOverrideFile --env-file $EnvFile build $ServiceName

# Stop the existing container
Write-Host "Stopping $ServiceName container..."
docker-compose -f $ComposeFile -f $ComposeOverrideFile --env-file $EnvFile stop $ServiceName

# Remove the stopped container
Write-Host "Removing $ServiceName container..."
docker-compose -f $ComposeFile -f $ComposeOverrideFile --env-file $EnvFile rm -f $ServiceName

# Start the new container with the updated image
Write-Host "Starting the new $ServiceName container..."
docker-compose -f $ComposeFile -f $ComposeOverrideFile --env-file $EnvFile up -d $ServiceName

Write-Host "$ServiceName container has been rebuilt and redeployed."
