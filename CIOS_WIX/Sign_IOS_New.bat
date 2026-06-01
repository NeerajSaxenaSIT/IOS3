
C:

cd\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64


signtool.exe sign /tr http://time.certum.pl /td sha256 /fd sha256 /a "C:\GitHub\CIOS\IOS3_WIX\CIOS_WIX\IOS.exe"
signtool.exe sign /tr http://time.certum.pl /td sha256 /fd sha256 /a "C:\GitHub\CIOS\IOS3_WIX\CIOS_WIX\IOS.Library.dll"
signtool.exe sign /tr http://time.certum.pl /td sha256 /fd sha256 /a "C:\GitHub\CIOS\IOS3_WIX\CIOS_WIX\IOS.Configuration.dll"
signtool.exe sign /tr http://time.certum.pl /td sha256 /fd sha256 /a "C:\GitHub\CIOS\IOS3_WIX\CIOS_WIX\IOS.DataLibrary.dll"

c:

cd\GitHub\CIOS\IOS3_WIX\CIOS_WIX

