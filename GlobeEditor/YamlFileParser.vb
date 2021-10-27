Imports TechTreeViewer

Public Class YamlFileParser
    Public Class InvalidSyntaxException
        Inherits Exception

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class

    Private FileName As String
    Private FileReader As System.IO.TextReader
    Private CurrentLine As String
    Private CurrentLineNumber As Integer
    Private CurrentLineProcessed As Boolean

    Private Anchors As Dictionary(Of String, YamlNode)

    Public Sub New(FileName As String)
        Me.FileName = FileName
    End Sub

    Public Function Parse() As YamlNode
        Dim RootNode As YamlNode
        Debug.WriteLine("Parsing " + FileName)
        FileReader = System.IO.File.OpenText(FileName)
        CurrentLineNumber = 0
        Anchors = New Dictionary(Of String, YamlNode)
        RootNode = ParseNextLine()
        If Not RootNode Is Nothing Then RootNode.Source = FileName

        FileReader.Close()
        Return RootNode
    End Function

    Private Function CalculateIndent(Line As String) As Integer
        CalculateIndent = 0
        Do While Line(CalculateIndent) = " "
            CalculateIndent += 1
        Loop
    End Function
    REM Only an idiot would put this into the specification: The “-”, “?” and “:” characters used to denote block collection entries are perceived by people to be part of the indentation. This is handled on a case-by-case basis by the relevant productions.
    REM I mean, give me a break here.
    Private Function CalculateStupidIndent(Line As String) As Integer
        CalculateStupidIndent = 0
        Do While Line(CalculateStupidIndent) = " "
            CalculateStupidIndent += 1
        Loop
        If Line(CalculateStupidIndent) = "-" Then CalculateStupidIndent += 1
    End Function

    Private Const EXPR_YAML_SKIP_LINE = "(?:^\s*#.*$)|(?:^\s*$)|(?:^---$)"
    Private Function ReadNextLine() As String
        Do
            ReadNextLine = FileReader.ReadLine()
            CurrentLineNumber += 1
            If ReadNextLine Is Nothing Then Return Nothing
        Loop While System.Text.RegularExpressions.Regex.IsMatch(ReadNextLine, EXPR_YAML_SKIP_LINE)
    End Function

    Private Const EXPR_YAML_SEQUENCE = "^- (.*)$"
    Private Const EXPR_YAML_SEQUENCE_REFERENCE = "^-\s*(?:\*(\w+)\s*)(?:#.*)?$"
    Private Const EXPR_YAML_PURE_SEQUENCE = "^-\s*(?:&(\w+)\s*)?(?:#.*)?$"
    Private Const EXPR_YAML_MAPPING_REFERENCE = "^(\w+?):\s*(?:\*(\w+)\s*)(?:#.*)?$"
    Private Const EXPR_YAML_MAPPING = "^(\w+?):\s*(?:&(\w+)\s*)?(?:#.*)?$"
    Private Const EXPR_YAML_INLINE_MAPPING = "^(\w+?):\s*(?:&(\w+)\s+)?""?(.+?)""?\s*(?:#.*)?$"
    Private Const EXPR_YAML_VALUE = "^""?(.+?)""?\s*(?:#.*)?$"



    Private Function ParseNextLine(Optional ExpectedStupidIndent As Integer = -1) As YamlNode
        CurrentLine = ReadNextLine()
        If CurrentLine Is Nothing Then Return Nothing
        Dim Indent As Integer
        Dim StupidIndent As Integer
        Indent = CalculateIndent(CurrentLine)
        StupidIndent = CalculateStupidIndent(CurrentLine)
        If StupidIndent < ExpectedStupidIndent Then
            Throw New InvalidSyntaxException("Unexpected indent: " + Str(Indent) + " expected at least " + Str(ExpectedStupidIndent))
        End If
        Return ParseNodeFromLine(Indent, StupidIndent)
    End Function
    Private Function ParseNodeFromLine(Indent As Integer, StupidIndent As Integer) As YamlNode
        Dim Node As YamlNode

        Dim FirstLine As Boolean = True

        Do While FirstLine = True Or CalculateStupidIndent(CurrentLine) = StupidIndent
            CurrentLineProcessed = True
            Dim UnindentedLine As String
            UnindentedLine = Mid(CurrentLine, Indent + 1)
            If FirstLine Then
                FirstLine = False
                If System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_PURE_SEQUENCE) Then
                    Node = New YamlNode(YamlNode.EType.Sequence)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_SEQUENCE_REFERENCE) Then
                    Node = New YamlNode(YamlNode.EType.Sequence)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_SEQUENCE) Then
                    Node = New YamlNode(YamlNode.EType.Sequence)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_MAPPING_REFERENCE) Then
                    Node = New YamlNode(YamlNode.EType.Mapping)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_MAPPING) Then
                    Node = New YamlNode(YamlNode.EType.Mapping)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_INLINE_MAPPING) Then
                    Node = New YamlNode(YamlNode.EType.Mapping)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_VALUE) Then
                    Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_VALUE)
                    Return New YamlNode(Matches(0).Groups(1).Value)
                Else
                    Throw New Exception("Unknown Yaml node: " + UnindentedLine)
                End If
            End If

            If System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_PURE_SEQUENCE) Then
                Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_PURE_SEQUENCE)
                Try
                    Dim ParsedNode = ParseNextLine(Indent + 1)
                    If Matches(0).Groups(1).Success Then
                        CreateAnchor(Matches(0).Groups(1).Value, ParsedNode)
                    End If
                    Node.AddItem(ParsedNode)
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                Catch ex As InvalidSyntaxException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_SEQUENCE_REFERENCE) Then
                Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_SEQUENCE_REFERENCE)
                Try
                    Node.AddItem(GetAnchor(Matches(0).Groups(1).Value))
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_SEQUENCE) Then
                Try
                    Dim LocalStupidIndent As Integer
                    If CurrentLine(Indent + 2) = "-" Then
                        LocalStupidIndent = Indent + 3
                    Else
                        LocalStupidIndent = Indent + 2
                    End If
                    Node.AddItem(ParseNodeFromLine(Indent + 2, LocalStupidIndent))
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_MAPPING_REFERENCE) Then
                Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_MAPPING_REFERENCE)
                Try
                    Node.SetMapping(Matches(0).Groups(1).Value, GetAnchor(Matches(0).Groups(2).Value))
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_MAPPING) Then
                Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_MAPPING)
                Try
                    Dim ParsedNode = ParseNextLine(Indent + 1)
                    If Matches(0).Groups(2).Success Then
                        CreateAnchor(Matches(0).Groups(2).Value, ParsedNode)
                    End If
                    Node.SetMapping(Matches(0).Groups(1).Value, ParsedNode)
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                Catch ex As InvalidSyntaxException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(UnindentedLine, EXPR_YAML_INLINE_MAPPING) Then
                Dim Matches = System.Text.RegularExpressions.Regex.Matches(UnindentedLine, EXPR_YAML_INLINE_MAPPING)
                Dim ParsedNode = New YamlNode(Matches(0).Groups(3).Value)
                If Matches(0).Groups(2).Success Then
                    CreateAnchor(Matches(0).Groups(2).Value, ParsedNode)
                End If
                Try
                    Node.SetMapping(Matches(0).Groups(1).Value, ParsedNode)
                Catch ex As YamlNode.WrongYamlNodeException
                    MsgBox("There was an error parsing a yaml file." + vbCrLf + "File: " + FileName + vbCrLf + "Around line: " + Trim(Str(CurrentLineNumber)) + vbCrLf + "Reason: " + ex.Message, MsgBoxStyle.Exclamation, "Nodes skipped")
                    Return Nothing
                End Try
            End If

            If CurrentLineProcessed Then
                CurrentLine = ReadNextLine()
                CurrentLineProcessed = False
            End If

            If CurrentLine Is Nothing Then Exit Do
        Loop
        Return Node
    End Function

    Private Sub CreateAnchor(key As String, node As YamlNode)
        If Anchors.ContainsKey(key) Then
            Anchors(key) = node
        Else
            Anchors.Add(key, node)
        End If
    End Sub
    Private Function GetAnchor(key As String) As YamlNode
        Return Anchors(key)
    End Function
End Class
