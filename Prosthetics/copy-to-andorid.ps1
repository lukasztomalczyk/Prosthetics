$src = "C:\Users\lukas\source\repos\Prosthetics\Prosthetics\bin\Release\net8.0-android\publish"
$dest = "/storage/emulated/0/Download/Instalacja"

c:\platform-tools\adb.exe devices
c:\platform-tools\adb.exe push -a -p $src $dest 