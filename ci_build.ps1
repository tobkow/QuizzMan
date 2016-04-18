$DNU = "dnu"
$DNVM = "dnvm"

& $DNVM install 1.0.0-rc1-update1
& $DNVM use 1.0.0-rc1-update1
& $DNU restore

# Set the version number in package.json
$ProjectJsonPath = Join-Path -Path $SolutionRoot -ChildPath "src\Smidge\project.json"
(gc -Path $ProjectJsonPath) `
	-replace "(?<=`"version`":\s`")[.\w-]*(?=`",)", "$ReleaseVersionNumber$PreReleaseName" |
	sc -Path $ProjectJsonPath -Encoding UTF8
# Set the copyright
$DateYear = (Get-Date).year
(gc -Path $ProjectJsonPath) `
	-replace "(?<=`"copyright`":\s`")[\w\s©]*(?=`",)", "Copyright © Tobiasz Kowalczyk $DateYear" |
	sc -Path $ProjectJsonPath -Encoding UTF8

 #run the build
$ $DNU build
