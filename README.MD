adb pull "/sdcard/Raport_02.xlsx" "C:\temp\Raport_02.xlsx"


gdzie leży plik od bazy danych: "Filename=/data/user/0/com.companyname.prosthetics/cache/LocalDatabase-1.db"

dotnet publish -c Release -r Android -p:PackageFormat=Apk
dotnet publish -f:net8.0-android -c:Release

Paczka buduje się w lokalizacji: bin\Release\net8.0-android\publish


ADB
* lista podłączonych urząrzeń (muszą mnieć włączoną opcje debugging)
c:\platform-tools_r34.0.5-windows\platform-tools\adb devices

V    Verbose (show all possibly useless logs, default level
D    Debug (show all reasonable debug logs)
I    Info (show expected logs for regular usage)
W    Warn (show possible issues that are not yet errors)
E    Error (show issues that have caused errors)
F    Fatal (show issues that are fatal to runtime and will often result in rebooting)

* tylko dla wybranej aplikacji
adb logcat *:E | findstr com.companyname.prosthetics

*znalezienie PID aplikacji
adb shell pidof 'com.companyname.prosthetics'

* znalezienie logów po PID
adb logcat pid=1422 | findstr /L "E" 