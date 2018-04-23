Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Controls
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.ViewInfo

Namespace RTFRibbonMini
    Public Class MyRibbonControl
        Inherits RibbonControl

        Public Sub New()
        End Sub
        Protected Overrides Function CreateBarManager() As RibbonBarManager
            Return New MyRibbonBarManager(Me)
        End Function
    End Class



    Public Class MyRibbonBarManager
        Inherits RibbonBarManager

        Public Sub New(ByVal ribbonControl As RibbonControl)
            MyBase.New(ribbonControl)

        End Sub
        Protected Overrides Function CreateSelectionInfo() As BarSelectionInfo
            Return New AdvancedBarSelectionInfo(Me)
        End Function
    End Class



    Public Class AdvancedBarSelectionInfo
        Inherits RibbonSelectionInfo

        Public Sub New(ByVal manager As RibbonBarManager)
            MyBase.New(manager)
        End Sub
        Protected Overrides Sub CheckAndClosePopups(ByVal newLink As BarItemLink)
            Dim popups As BarPopupCollection = OpenedPopups
            Dim miniToolBar As PopupMenuBarControl = popups.OfType(Of PopupMenuBarControl)().FirstOrDefault()
            If miniToolBar IsNot Nothing Then
                Return
            End If

            MyBase.CheckAndClosePopups(newLink)
        End Sub

        Protected Overrides Sub PressLinkCore(ByVal link As BarItemLink, ByVal isArrow As Boolean)
            For n As Integer = OpenedPopups.Count - 1 To 0 Step -1
                Dim popup As IPopup = TryCast(OpenedPopups(n), IPopup)
                If popup.ContainsLink(link) Then

                    For i As Integer = 0 To Manager.Ribbon.MiniToolbars.Count - 1
                        Manager.Ribbon.MiniToolbars(i).Hide()
                    Next i
                End If
            Next n
            MyBase.PressLinkCore(link, isArrow)
        End Sub
    End Class
End Namespace
