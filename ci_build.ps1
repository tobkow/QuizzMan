$DNU = "dnu"
$DNVM = "dnvm"

& $DNVM install 1.0.0-rc1-update1
& $DNVM use 1.0.0-rc1-update1
& $DNU restore
 #run the build
& "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" src\QuizzMan.sln
