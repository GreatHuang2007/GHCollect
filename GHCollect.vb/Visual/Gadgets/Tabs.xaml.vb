Public Class Tabs

    Private Page As TabItem

    Public holder As TabPanel
    Public Property closeable As Boolean = True

    Public Sub Initialize(ByVal title As String, ByVal page As TabItem)
        Me.lb_title.Text = title
        Me.Page = page
        If Not closeable Then
            Me.bt_close.IsEnabled = False
            Me.bt_close.Visibility = Visibility.Collapsed
        End If
    End Sub

    Public Sub ShowPage() Handles Me.MouseUp
        CType(Me.Page.Parent, TabControl).SelectedItem = Me.Page
    End Sub

    Public Sub SetMainPage()
        Me.bt_close.Visibility = Visibility.Collapsed
        Me.bt_close.IsEnabled = False
    End Sub

    Public Sub closeMe()
        CType(Me.Page.Parent, TabControl).Items.Remove(Me.Page)
        CType(Me.Parent, StackPanel).Children.Remove(Me)
    End Sub

    Private Sub bt_close_Click(sender As Object, e As RoutedEventArgs) Handles bt_close.Click
        holder.RemoveTab(Me)
    End Sub
End Class
