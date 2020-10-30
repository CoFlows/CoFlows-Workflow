Public Class VbQuery    
    Public Shared Function getName() As String
        Return "something"
    End Function

    ''' <api name="Add">
    ''' <summary> Function that adds two numbers </summary>
    ''' <remarks> it only works for pynum </remarks>
    ''' <returns> returns an integer </returns>
    ''' <param name="x">First number to add</param>
    ''' <param name="y">Second number to add</param>
    ''' </api>
    Public Shared Function Add(x as Integer, y as Integer) As Integer
        Return x + y
    End Function
End Class

