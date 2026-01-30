Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Public Class IOSCustomSecurityProvider

    Private _EncryptionKey As String
    Public Property EncryptionKey() As String
        Get
            Return _EncryptionKey
        End Get
        Set(ByVal value As String)
            _EncryptionKey = value
        End Set
    End Property

    Private _EncryptionSalt As String
    Public Property EncryptionSalt() As String
        Get
            Return _EncryptionSalt
        End Get
        Set(ByVal value As String)
            _EncryptionSalt = value
        End Set
    End Property
    Public Sub New(Optional ByVal eKey As String = "1E80CDFF-2B6B-4ac7-A52B-CEAC9C3789BA", Optional ByVal eSalt As String = "1E80CDFF")
        Me.EncryptionKey = eKey
        Me.EncryptionSalt = eSalt
    End Sub
    'Public Sub New()
    '    Me.New("1E80CDFF-2B6B-4ac7-A52B-CEAC9C3789BA", "1E80CDFF")
    'End Sub
    Public Function Encrypt(ByVal TextToEncrypt As String) As String
        Dim smg As New RijndaelManaged()
        Dim cs As CryptoStream
        Dim svb() As Byte = Encoding.ASCII.GetBytes(Me.EncryptionKey)
        Dim pk As New Rfc2898DeriveBytes(Me.EncryptionSalt, svb, 3)
        smg.Key = pk.GetBytes(smg.KeySize / 8)
        smg.IV = pk.GetBytes(smg.BlockSize / 8)
        Dim sv() As Byte = Encoding.ASCII.GetBytes(TextToEncrypt)
        Dim ms As New MemoryStream()
        cs = New CryptoStream(ms, smg.CreateEncryptor(), CryptoStreamMode.Write)
        cs.Write(sv, 0, sv.Length)
        cs.FlushFinalBlock()
        Return Convert.ToBase64String(ms.ToArray())
    End Function
    Public Function Decrypt(ByVal TextToDecrypt As String) As String
        Try
            Dim smg As New RijndaelManaged()
            Dim cs As CryptoStream
            Dim svb() As Byte = Encoding.ASCII.GetBytes(Me.EncryptionKey)
            Dim pk As New Rfc2898DeriveBytes(Me.EncryptionSalt, svb, 3)
            smg.Key = pk.GetBytes(smg.KeySize / 8)
            smg.IV = pk.GetBytes(smg.BlockSize / 8)
            Dim sv() As Byte = Convert.FromBase64String(TextToDecrypt)
            Dim ms As New MemoryStream()
            cs = New CryptoStream(ms, smg.CreateDecryptor(), CryptoStreamMode.Write)
            cs.Write(sv, 0, sv.Length)
            cs.FlushFinalBlock()
            Return System.Text.Encoding.ASCII.GetString(ms.ToArray())
        Catch ex As Exception
            Throw New Exception("Decryption of validation ticket failed.")
        End Try
    End Function
End Class
