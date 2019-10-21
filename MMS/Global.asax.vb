Imports System.Web.SessionState
Imports System.Web.Routing

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
    End Sub

End Class