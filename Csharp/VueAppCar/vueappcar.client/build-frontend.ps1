# build-frontend.ps1
Set-Location -Path "E:\revv\Csharp\RevvCar\RevvCar\RevvView\ClientApp"
npm install
npm run build
Remove-Item -Recurse -Force -Path "E:\revv\Csharp\RevvCar\RevvCar\wwwroot\*"
Copy-Item -Recurse -Path "E:\revv\Csharp\RevvCar\RevvCar\RevvView\dist\*" -Destination "E:\revv\Csharp\RevvCar\RevvCar\wwwroot"