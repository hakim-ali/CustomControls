##
##	Create Modular Xamarin Forms NuGet based on UI Technology
##  ==========================================================================================
##  
##  “Xamarin Forms Labs - Core” NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##      o	Xamarin.Forms.Labs.Droid.dll (MonoAndroid - Xamarin Android)
##      o	Xamarin.Forms.Labs.iOS.dll (MonoIos - Xamarin iOS)
##      o	Xamarin.Forms.Labs.WP.dll (WinPRT - Windows Phone 8)
##  •	Dependencies
##      o	"Xamarin.Forms" NuGet
##  
##  “Xamarin Forms Labs - Services Caching” NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Caching.SQLiteNet.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“SQLite.Net” NuGet
##  
##  “Xamarin Forms Labs - Services Cryptography” NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Cryptography.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	None
##  
##  “Xamarin Forms Labs - Services IoC Autofac” NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.Autofac.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Autofac” NuGet
##  
##  “Xamarin Forms Labs - Services IoC Ninject” NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.Ninject.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“Ninject” NuGet
##
##  “Xamarin Forms Labs - Services IoC SimpleInjector" NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.SimpleInjector.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“SimpleInjector” NuGet
##  
##  “Xamarin Forms Labs - Services IoC TinyIOC" NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.TinyIOC.dll (.NET 4.5)
##      o	Xamarin.Forms.Labs.Services.TinyIOC.Droid.dll (MonoAndroid - Xamarin Android)
##      o	Xamarin.Forms.Labs.Services.TinyIOC.iOS.dll (MonoIos - Xamarin iOS)
##      o	Xamarin.Forms.Labs.Services.TinyIOC.WP8.dll (WinPRT - Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##  
##  “Xamarin Forms Labs - Services Serialization JSON" NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.Serialization.JsonNET.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“Newtonsoft JSON” NuGet
##
##  “Xamarin Forms Labs - Services Serialization ProtoBuf" NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.Serialization.ProtoBuf.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“ProtoBuf-net” NuGet
##  
##  “Xamarin Forms Labs - Services Serialization ServiceStack" NuGet
##  •	Contents:
##      o	Xamarin.Forms.Labs.Services.Serialization.ServiceStackV3.dll (.NET 4.5)
##      o	Xamarin.Forms.Labs.Services.Serialization.ServiceStackV3.Droid.dll (MonoAndroid - Xamarin Android)
##      o	Xamarin.Forms.Labs.Services.Serialization.ServiceStackV3.iOS.dll (MonoIos - Xamarin iOS)
##      o	Xamarin.Forms.Labs.Services.Serialization.ServiceStackV3.WP8.dll (WinPRT - Windows Phone 8)
##  •	Dependencies
##      o	“Xamarin Forms Labs - Core” NuGet
##      o	“ServiceStack.Text” NuGet
##      o	“ServiceStack.Text.MonoTouch” NuGet
##      o	“ServiceStack.Text.WP8” NuGet
##
##  “Xamarin Forms Labs - Caching” NuGet
##  •	Contents:
##      o	Xamarin.Forms..Labs.Caching.dll (PCL - Xamarin Android, Xamarin iOS, Windows Phone 8)
##      o	Xamarin.Forms.Labs.CachingDroid.dll (MonoAndroid - Xamarin Android)
##      o	Xamarin.Forms.Labs.CachingiOS.dll (MonoIos - Xamarin iOS)
##      o	Xamarin.Forms.Labs.CachingWP.dll (WinPRT - Windows Phone 8)

param( [System.String] $commandLineOptions )

function OutputCommandLineUsageHelp()
{
    Write-Host "Build all NuGet packages."
    Write-Host "============================"
    Write-Host "Usage: Build All.ps1 [/PreRelease:<PreReleaseVersion>]"
    Write-Host ">E.g.: Build All.ps1"
    Write-Host ">E.g.: Build All.ps1 /PreRelease:RC1"
}

function Pause ($Message="Press any key to continue...")
{
    Write-Host -NoNewLine $Message
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    Write-Host ""
}

## Process CommandLine options
if ( [System.String]::IsNullOrEmpty($commandLineOptions) -ne $true )
{
    if ( $commandLineOptions.StartsWith("/PreRelease:", [System.StringComparison]::OrdinalIgnoreCase) )
    {
        $preRelease = $commandLineOptions.Substring( "/PreRelease:".Length )
    }
    else
    {
        OutputCommandLineUsageHelp
        return
    }
}

try 
{
    ## Initialise
    ## ----------
    $originalBackground = $host.UI.RawUI.BackgroundColor
    $originalForeground = $host.UI.RawUI.ForegroundColor
    $originalLocation = Get-Location
    $packages = @("Core", "Services Caching", "Services Cryptography", "Services IoC AutoFac", "Services IoC Ninject", "Services IoC SimpleInjector", "Services IoC TinyIOC", "Services Serialization JSON", "Services Serialization ProtoBuf", "Services Serialization ServiceStack", "Charting", "Services IoC Unity")  
    
    $host.UI.RawUI.BackgroundColor = [System.ConsoleColor]::Black
    $host.UI.RawUI.ForegroundColor = [System.ConsoleColor]::White
    
    Write-Host "Build All Xamarin Forms Labs NuGet packages" -ForegroundColor White
    Write-Host "==================================" -ForegroundColor White

    Write-Host "Creating Packages folder" -ForegroundColor Yellow
    mkdir Packages

    ## NB - Cleanup destination package folder
    ## ---------------------------------------
    Write-Host "Clean destination folders..." -ForegroundColor Yellow
    Remove-Item ".\Packages\*.nupkg" -Recurse -Force -ErrorAction SilentlyContinue
    
    ## Spawn off individual build processes...
    ## ---------------------------------------
    Set-Location "$originalLocation\Definition" ## Adjust current working directory since scripts are using relative paths
    $packages | ForEach { & ".\Build.ps1" $_ $commandLineOptions }
    Write-Host "Build All - Done." -ForegroundColor Green
}
catch 
{
    $baseException = $_.Exception.GetBaseException()
    if ($_.Exception -ne $baseException)
    {
      Write-Host $baseException.Message -ForegroundColor Magenta
    }
    Write-Host $_.Exception.Message -ForegroundColor Magenta
    Pause
} 
finally 
{
    ## Restore original values
    $host.UI.RawUI.BackgroundColor = $originalBackground
    $host.UI.RawUI.ForegroundColor = $originalForeground
    Set-Location $originalLocation
}
Pause # For debugging purpose