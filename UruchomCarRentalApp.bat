@echo off
title CarRentalApp – uruchamianie serwera i klienta...

start "Serwer API" cmd /k "cd CarRentalApp.Server && dotnet run"

start "Klient Blazor" cmd /k "cd CarRentalApp && dotnet run"

timeout /t 5 >nul

start http://localhost:5177

exit