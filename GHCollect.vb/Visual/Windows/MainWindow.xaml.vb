Public Class MainWindow
    Sub WindowNormal()
        Me.gd_titlebar.Margin = New Thickness(0, 0, 0, 0)
        Me.bt_resize.Content = "+"
    End Sub

    Sub WindowMaximize()
        Me.gd_titlebar.Margin = New Thickness(5, 8, 5, 0)
        Me.bt_resize.Content = "="
    End Sub

    Private Sub bt_min_Click(sender As Object, e As RoutedEventArgs) Handles bt_min.Click
        Me.WindowState = WindowState.Minimized
    End Sub

    Private Sub bt_resize_Click(sender As Object, e As RoutedEventArgs) Handles bt_resize.Click
        If Me.WindowState = WindowState.Maximized Then
            Me.WindowState = WindowState.Normal
            WindowNormal()
        Else
            Me.WindowState = WindowState.Maximized
            WindowMaximize()
        End If
    End Sub

    Private Sub MainWindow_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If Me.WindowState = WindowState.Maximized Then
            WindowMaximize()
        Else
            WindowNormal()
        End If
    End Sub

    Private Sub bt_close_Click(sender As Object, e As RoutedEventArgs) Handles bt_close.Click
        Me.Close()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Forms.Application.EnableVisualStyles()
        Me.tp_title.AddTab("主页", Me.pg_catalog, True)
        Me.uc_catalog.FileDirectory = InputBox("输入你的文件夹位置")
        Me.uc_catalog.Refresh()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim test As New TabItem
        Me.tp_title.AddTab("aaa", test)
        Me.tc_main.Items.Add(test)
        Me.tc_main.SelectedItem = test
    End Sub
End Class
