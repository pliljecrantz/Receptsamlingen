<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Receptsamlingen.Mvc.Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receptsamlingen - något gick fel.</title>
    <link href="~/Content/Styles/normalize.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/Styles/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="~/Content/Styles/jquery-ui.min.css" rel="stylesheet" />
    <link href="~/Content/Styles/jquery-ui.structure.min.css" rel="stylesheet" />
    <link href="~/Content/Styles/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="~/Content/Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="~/Content/Images/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
        <% Response.StatusCode = 500; %>
        <div class="container">
            <header class="row">
                <div class="col-md-12">
                    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                        <div class="container">
                            <div class="navbar-header">
                                <a class="navbar-brand" href="/">
                                    <img alt="Brand" src="/Content/Images/brand.png" />
                                </a>
                            </div>
                            <div id="navbar" class="navbar-collapse collapse">
                                <!-- -->
                            </div>
                        </div>
                    </nav>
                </div>
            </header>
            <div class="wrapper row">
                <div class="col-md-12" style="min-height: 500px; text-align: center;">
                    <h1 style="font-size: 8em;">500.</h1>
                    <p>En av hustomtarna har trasslat in sig i sladdarna, men vi jobbar på att få loss honom...</p>
                    <p><a href="/" class="orange">&raquo;Tillbaka till startsidan&laquo;</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
