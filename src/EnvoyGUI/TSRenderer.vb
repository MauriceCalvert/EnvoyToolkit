Public Class TSRenderer
    Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
        Dim btn As ToolStripButton = TryCast(e.Item, ToolStripButton)

        If btn IsNot Nothing Then
            Dim bounds As Rectangle = New Rectangle(Point.Empty, e.Item.Size)

            If btn.Pressed Then
                ' Draw the pressed state with a different 3D effect
                ControlPaint.DrawButton(e.Graphics, bounds, ButtonState.Pushed)
            ElseIf btn.Selected Then
                ' Draw the hovered state with a 3D effect
                ControlPaint.DrawButton(e.Graphics, bounds, ButtonState.Normal)
            Else
                ' Draw the default state with a 3D effect
                ControlPaint.DrawButton(e.Graphics, bounds, ButtonState.Normal)
            End If
        Else
            MyBase.OnRenderButtonBackground(e)
        End If
    End Sub
End Class
