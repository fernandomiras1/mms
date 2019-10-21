Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Public Class Crypt
    Public Shared Function Encrypt(ByVal clearText As String, ByVal Password As String) As String
        'Convert text to bytes
        Dim clearBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(clearText)

        'We will derieve our Key and Vectore based on following
        'password and a random salt value, 13 bytes in size.
        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})

        Dim encryptedData As Byte() = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16))

        Return Convert.ToBase64String(encryptedData)
    End Function

    Public Shared Function Encrypt(ByVal clearData As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
        Dim encryptedData As Byte()
        'Create stream for encryption
        Using ms As New MemoryStream()
            'Create Rijndael object with key and vector
            Using alg As Rijndael = Rijndael.Create()
                alg.Key = Key
                alg.IV = IV
                'Forming cryptostream to link with data stream.
                Using cs As New CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write)
                    'Write all data to stream.
                    cs.Write(clearData, 0, clearData.Length)
                End Using
                encryptedData = ms.ToArray()
            End Using
        End Using

        Return encryptedData
    End Function

    'Call following function to decrypt data
    Public Shared Function Decrypt(ByVal cipherText As String, ByVal Password As String) As String
        'Convert base 64 text to bytes

        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

        'We will derieve our Key and Vectore based on following
        'password and a random salt value, 13 bytes in size.
        Dim pdb As New PasswordDeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
         &H65, &H64, &H76, &H65, &H64, &H65, _
         &H76})
        Dim decryptedData As Byte() = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16))

        'Converting unicode string from decrypted data
        Return Encoding.Unicode.GetString(decryptedData)
    End Function

    Public Shared Function Decrypt(ByVal cipherData As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
        Dim decryptedData As Byte()
        'Create stream for decryption
        Using ms As New MemoryStream()
            'Create Rijndael object with key and vector
            Using alg As Rijndael = Rijndael.Create()
                alg.Key = Key
                alg.IV = IV
                'Forming cryptostream to link with data stream.
                Using cs As New CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write)
                    'Write all data to stream.

                    cs.Write(cipherData, 0, cipherData.Length)
                End Using
                decryptedData = ms.ToArray()
            End Using
        End Using
        Return decryptedData
    End Function
End Class
