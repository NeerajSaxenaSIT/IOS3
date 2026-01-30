
C:

cd\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64

signtool.exe sign /f "C:\GitHub\CIOS\IOS3_WIX\IOS\pkcs12.pfx" /p "C3llS3ns#001" /tr "http://timestamp.digicert.com" /td SHA256 /fd SHA256 "C:\GitHub\CIOS\IOS3_WIX\CIOS_WIX\bin\Release\CIOS_Setup.msi"

C:

cd\GitHub\CIOS\IOS3_WIX\CIOS_WIX

