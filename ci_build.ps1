dnvm install 1.0.0-rc1-update1
dnvm use 1.0.0-rc1-update1 -a x64 -r coreclr
dnu restore
 #run the build
& "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" src\QuizzMan.sln
