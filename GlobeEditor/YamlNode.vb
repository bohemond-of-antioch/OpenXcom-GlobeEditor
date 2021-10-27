Public Class YamlNode
    Public Class WrongYamlNodeException
        Inherits Exception

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class


    Enum EType
        Sequence
        Mapping
        Value
    End Enum

    Public Type As EType

    Private Mapping As Dictionary(Of String, YamlNode)
    Private Sequence As List(Of YamlNode)
    Private Value As String

    Public Source As String

    Public Sub New(Type As EType)
        Me.Type = Type
        If Type = EType.Sequence Then
            Sequence = New List(Of YamlNode)
        Else
            Mapping = New Dictionary(Of String, YamlNode)
        End If
    End Sub

    Public Sub New(Value As String)
        Me.Type = EType.Value
        Me.Value = Value
    End Sub

    Public Sub SetMapping(Name As String, Value As YamlNode)
        If Type <> EType.Mapping Then Throw New WrongYamlNodeException("This Node is not a Mapping!")
        Mapping(Name) = Value
    End Sub
    Public Function HasMapping(Name As String) As Boolean
        If Type <> EType.Mapping Then Throw New WrongYamlNodeException("This Node is not a Mapping!")
        Return Mapping.ContainsKey(Name)
    End Function
    Public Function GetMappingKeys() As List(Of String)
        Return Mapping.Keys.ToList()
    End Function
    Public Function GetMapping(Name As String) As YamlNode
        If Type <> EType.Mapping Then Throw New WrongYamlNodeException("This Node is not a Mapping!")
        Return Mapping(Name)
    End Function
    Public Function GetMapping(Name As String, DefaultValue As String) As YamlNode
        If Type <> EType.Mapping Then Throw New WrongYamlNodeException("This Node is not a Mapping!")
        If Mapping.ContainsKey(Name) Then Return Mapping(Name)
        Return New YamlNode(DefaultValue)
    End Function

    Public Sub SetValue(Value As String)
        If Type <> EType.Value Then Throw New WrongYamlNodeException("This Node is not a Value!")
        Me.Value = Value
    End Sub

    Public Function GetValue() As String
        If Type <> EType.Value Then Throw New WrongYamlNodeException("This Node is not a Value!")
        Return Value
    End Function

    Public Sub AddItem(Value As YamlNode)
        If Type <> EType.Sequence Then Throw New WrongYamlNodeException("This Node is not a Sequence!")
        Sequence.Add(Value)
    End Sub

    Public Function GetItem(Index As Integer) As YamlNode
        If Type <> EType.Sequence Then Throw New WrongYamlNodeException("This Node is not a Sequence!")
        Return Sequence(Index)
    End Function

    Public Function ItemCount() As Integer
        If Type <> EType.Sequence Then Throw New WrongYamlNodeException("This Node is not a Sequence!")
        Return Sequence.Count
    End Function

End Class
