Imports System.ComponentModel

<System.Windows.Forms.Design.ToolStripItemDesignerAvailability(
    System.Windows.Forms.Design.ToolStripItemDesignerAvailability.ToolStrip _
    Or System.Windows.Forms.Design.ToolStripItemDesignerAvailability.StatusStrip _
    Or System.Windows.Forms.Design.ToolStripItemDesignerAvailability.MenuStrip)>
Public Class TSDatePicker : Inherits ToolStripControlHost
    Public Sub New()
        MyBase.New(New System.Windows.Forms.DateTimePicker())
    End Sub

    Public ReadOnly Property ExposedControl() As DateTimePicker
        Get
            Return CType(Control, DateTimePicker)
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Overrides Property Text As String
        Get
            Return ExposedControl.Text
        End Get
        Set(value As String)
            ' verify valid date
            Dim dt As DateTime
            If DateTime.TryParse(value, dt) Then
                ExposedControl.Text = value
            End If
        End Set
    End Property
End Class