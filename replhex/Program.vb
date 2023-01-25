Imports System
Imports System.Globalization
Imports System.IO
Imports System.Text

Module Program
    Sub Main(args As String())
        Try
            If args.Length = 0 Then
                Console.ForegroundColor = ConsoleColor.Magenta
                Console.WriteLine("ReplHEX - by Nazky")
                Console.ForegroundColor = ConsoleColor.Blue
                Console.WriteLine("A simple cli HEX replacer")
                Console.WriteLine("")
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine("Command:")
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(vbTab & "-a , --address <value> | Required: int or hex value (Prefix hex with '0x')")
                Console.WriteLine(vbTab & "-h , --hex <hex> | Can have spaces and quotations")
                Console.WriteLine(vbTab & "-hr , --hex_reverse <hex> | Same as above, just the hex is written in reverse")
                Console.WriteLine(vbTab & "-t , --text <text> | Plain text")
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine("exemple:")
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(vbTab & ".\replhex.exe ""path/to/file"" -a 0x420 -h ""00000000000000000000000000000000""")
                Console.WriteLine(vbTab & ".\replhex.exe ""path/to/file"" -a 0x420 -t ""this is a test""")
                Console.ForegroundColor = ConsoleColor.White
                Return
            ElseIf File.Exists(args(0)) Then
                Dim path As String = args(0)
                Dim num As UInteger = 0UI
                Dim arr As Byte() = Nothing
                Dim b As Boolean = False
                Dim b2 As Boolean = False
                For i As Integer = 1 To args.Length - 1

                    If args(i) = "--address" Or args(i) = "-a" Then
                        Dim a As String = args(i + 1)

                        If a.StartsWith("0x") Then
                            a = a.Remove(0, 2)
                            num = UInteger.Parse(a, NumberStyles.HexNumber)
                        Else
                            num = UInteger.Parse(a)
                        End If

                        b = True
                        i += 1
                    ElseIf args(i) = "--hex" Or args(i) = "-h" Then
                        arr = Convert.FromHexString(args(i + 1).Replace(" ", ""))
                        b2 = True
                        i += 1
                    ElseIf args(i) = "--hex_reverse" Or args(i) = "-hr" Then
                        Dim hr As String = args(i + 1)
                        hr = hr.Replace(" ", "")
                        Dim h As String = String.Empty

                        For num2 As Integer = h.Length - 2 To 0 Step 2
                            h += h.Substring(num2, 2)
                        Next

                        arr = Convert.FromHexString(h)
                        b2 = True
                        i += 1
                    ElseIf args(i) = "--text" Or args(i) = "-t" Then
                        Dim t As String = args(i + 1)
                        arr = Encoding.ASCII.GetBytes(t)
                        b2 = True
                        i += 1
                    End If
                Next

                Try
                    Using fileStream As FileStream = New FileStream(path, FileMode.Open, FileAccess.Write)
                        fileStream.Seek(CInt(num), SeekOrigin.Begin)
                        fileStream.Write(arr, 0, arr.Length)
                    End Using
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine("done.")
                    Console.ForegroundColor = ConsoleColor.White

                    Return
                Catch ex As Exception
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(ex.Message)
                    Console.ForegroundColor = ConsoleColor.White
                End Try

            End If
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ForegroundColor = ConsoleColor.White
        End Try

    End Sub
End Module
