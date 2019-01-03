C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe  E:\C#\WindowsServiceTest-master\WindowsServiceTest\WindowsServiceTestUI\bin\Debug\Service\WindowsServiceTest.exe
Net Start ServiceTest
sc config ServiceTest start= auto
pause

