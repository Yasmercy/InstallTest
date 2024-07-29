:: Build MyApp
:: Copy MyApp/bin into {Installer, InstallerDependencies}/Files/Bin

:: Builds MSI 
cd Installer && dotnet run Installer && cd ..
cd InstallerDependencies && dotnet run InstallerDependencies && cd ..

:: Build Executables
cd InstallerExe && dotnet build && cd ..

:: Build Bootstrapper
cd Bootstrapper && dotnet build && cd ..