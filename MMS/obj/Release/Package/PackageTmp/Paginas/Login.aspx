<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="MMS.Login" %>

<!DOCTYPE html>

<html lang="en">

<head id="Head1" runat="server">
    <title>M.M.S</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
 <%--   <meta name="viewport" content="width=device-width, initial-scale=1">--%>
     <meta name="viewport"  content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width, height=device-height">
   
    <meta name="description" content="">
    <meta name="author" content="">

    <link rel="shortcut icon" type="../Imagenes/logo_login.ico" href="~/Imagenes/logo_login.ico" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="../css/sb-admin.css" rel="stylesheet">
    <!-- Morris Charts CSS -->
    <link href="../css/plugins/morris.css" rel="stylesheet">
    <!-- Custom Fonts -->
    <link href="../Admin-2.1.0.8/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- LOS ALERT-->
    <script src="../alert/lib/alertify.js"></script>
    <link href="../alert/themes/alertify.core.css" rel="stylesheet" />
    <link href="../alert/themes/alertify.default.css" rel="stylesheet" />

</head>
<body>

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong>M.M.S</strong></h3>
                    </div>
                    <div class="panel-body">
                        <form role="form">
                            <fieldset>
                                <div class="form-group input-group">
                                    <span class="input-group-addon">@</span>
                                    <input class="form-control" placeholder="Usuario" runat="server" id="usuarios" name="usuarios" type="text" autofocus>
                                </div>
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock" style="margin-left: 5px;"></i>
                                    </span>
                                    <input class="form-control" placeholder="Contraseña" id="contras" runat="server" name="contras" type="password" value="">
                                </div>
                                <div>
                                    <label runat="server" id="LabelVer">Version 1.0.1</label>
                                </div>
                                <form id="form1" runat="server">
                                    <asp:Button Text="Ingresar" ID="ingrearL" CssClass="btn btn-lg btn-success btn-block" runat="server" />
                                </form>

                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../js/bootstrap.min.js"></script>





</body>
</html>
