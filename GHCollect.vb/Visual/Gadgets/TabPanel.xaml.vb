Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class TabPanel

    Public Property TabWidth As Double = 100
    Public Property TabMinWidth As Double = 50
    Public Property TabActualWidth As Double = 100
    Public Property IsFilled As Boolean = False

    Private TabCount As Integer = 0

    ''' <summary>
    ''' 只有主页标签页时，自动隐藏并替换为Default Title，如果为空，则不隐藏
    ''' </summary>
    Public Property MainPage As Tabs

    Public Shared ReadOnly DefaultTitleProperty As DependencyProperty = DependencyProperty.Register("DefaultTitle", GetType(String), GetType(TabPanel), New PropertyMetadata(String.Empty, AddressOf OnDefaultTitleChanged))

    Public Property DefaultTitle As String
        Get
            Return GetValue(DefaultTitleProperty).ToString()
        End Get
        Set(value As String)
            SetValue(DefaultTitleProperty, value)
        End Set
    End Property

    Private Shared Sub OnDefaultTitleChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim control As TabPanel = CType(d, TabPanel)
        control.lb_title.Text = e.NewValue.ToString()
    End Sub

    Private leftScroll As Double = 0

    Sub UpdateDisplay()
        '排列标签页
        Me.IsFilled = False
        If TabCount * TabWidth >= Me.ActualWidth Then
            Me.TabActualWidth = Me.ActualWidth / TabCount
        Else
            Me.TabActualWidth = Me.TabWidth
        End If
        If Me.TabActualWidth < TabMinWidth Then
            Me.TabActualWidth = Me.TabMinWidth
            Me.IsFilled = True
        End If
        If IsFilled Then
            Me.bt_Prev.Visibility = Visibility.Visible
            Me.bt_Next.Visibility = Visibility.Visible
        Else
            leftScroll = 0
            Me.bt_Prev.Visibility = Visibility.Collapsed
            Me.bt_Next.Visibility = Visibility.Collapsed
        End If
        For Each itab In Me.pn_tabs.Children
            CType(itab, Tabs).Width = Me.TabActualWidth
        Next
        '自动显示标题
        Me.lb_title.Visibility = Visibility.Hidden
        If TabCount = 1 Then
            MainPage.Visibility = Visibility.Hidden
            Me.lb_title.Visibility = Visibility.Visible
        ElseIf TabCount > 1 Then
            MainPage.Visibility = Visibility.Visible
            Me.lb_title.Visibility = Visibility.Collapsed
        End If
    End Sub

    Public Sub AddTab(title As String, tbItem As TabItem, Optional isMain As Boolean = False)
        Dim index As New Tabs With {
            .holder = Me
        }
        Me.pn_tabs.Children.Add(index)
        TabCount += 1
        If isMain Then
            Me.MainPage = index
            index.closeable = False
        End If
        index.Initialize(title, tbItem)
        UpdateDisplay()
    End Sub

    Public Sub RemoveTab(tab As Tabs)
        tab.closeMe()
        Me.TabCount -= 1
        UpdateDisplay()
    End Sub

    Private Sub bt_Prev_Click(sender As Object, e As RoutedEventArgs) Handles bt_Prev.Click
        UpdateDisplay()
        gd_tabs.PageLeft()
    End Sub

    Private Sub bt_Next_Click(sender As Object, e As RoutedEventArgs) Handles bt_Next.Click
        UpdateDisplay()
        gd_tabs.PageRight()
    End Sub

    Private Sub TabPanel_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        UpdateDisplay()
    End Sub
End Class
