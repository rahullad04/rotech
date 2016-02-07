<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>

     <link rel="stylesheet" href="Css/bootstrap.min.css">
    <link href="Css/styles.css" rel="stylesheet" media="screen">
    <link href="Css/login.css" rel="Stylesheet" media ="screen" />
     <link rel="stylesheet" href=">
     <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="js/modernizr-2.6.2-respond-1.1.0.min.js" type="text/javascript"></script>
    <style type="text/css"  >
   .rowlogin
    { margin-left:0;
      margin-right:0;
    	}
    
    </style>
</head>
<body>
  <div class="container">
    <div class="row vertical-center-row">
        <form id="form1" action="#" runat="server">
            <div id="loginarea">
                <h6>Login</h6>
                <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                <hr/>
                <div class="row">
                    <div style="float:left; width:43.33%">
                        <div class="image">
                            <img src="img/loginavatar.png" class="img-responsive img-rounded" />
                        </div>
                    </div>
                    <div style="float:right;width:55.33%">
                        <div class="form-group">
                            <label for="username">Username</label>
                             <asp:TextBox ID="txtusername" class="input-block-level" Text="" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="password">Password</label>
                            <asp:TextBox ID="txtpassword" class="input-block-level"  Text="" TextMode="Password"  runat="server"></asp:TextBox>  
                        </div>
                    </div>
                </div>
                <hr/>
                <div class="row">
                    <div style="float:right;"></div>
                    <div style="float:right;">
                        <asp:Button ID="btnlogin"  class="btn btn-primary" runat="server" type="Submit"  Text="Sign In"/>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
</body>
</html>
