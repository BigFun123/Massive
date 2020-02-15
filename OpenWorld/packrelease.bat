set filea=%date%_%time:~0,2%%time:~3,2%
echo %filea%
md ..\..\Releases\%filea%
xcopy Deploy\*.exe ..\..\Releases\%filea%
xcopy Deploy\*.dll ..\..\Releases\%filea%
xcopy Deploy\*.txt ..\..\Releases\%filea%
xcopy Deploy\*.config ..\..\Releases\%filea%
xcopy Deploy\Assets\*.* ..\..\Releases\%filea%\Assets\ /S