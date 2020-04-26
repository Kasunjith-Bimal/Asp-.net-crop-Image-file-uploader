<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.Jcrop.js"></script>
    <link href="Content/jquery.Jcrop.css" rel="stylesheet" />
    <script type="text/javascript">
        function showpreview(input) {

            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                    $('#imgpreview').Jcrop({
                        setSelect: [0, 0, 200, 100],
                        onChange: SetCoordinates,
                        onSelect: SetCoordinates,
                        allowResize: true
                    });
                }
                reader.readAsDataURL(input.files[0]);
            }

        }


        function SetCoordinates(c) {
            $('#imgX1').val(c.x);
            $('#imgY1').val(c.y);
            $('#imgWidth').val(c.w);
            $('#imgHeight').val(c.h);
            //$('#SignatureUploadBtn').show();

        };
        //$(function ($) {
        //    $('#imgpreview').Jcrop();
        //});

    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
              <asp:FileUpload ID="SignatureFileUploader" runat="server"  onchange="showpreview(this);" />
              <br />
              <img id="imgpreview"  src="" style="border-width: 0px; visibility: hidden;" />
              <br />
              <asp:Button ID="SignatureUploadBtn" runat="server" Text="Upload Signature"  OnClick="SignatureUploadBtn_Click"  />
              
        </div>
        <br />
        <br />
       
    <asp:Label ID="SignatureError" runat="server" Text=""></asp:Label>     
    <input id="imgX1" type="hidden"  runat ="server" />
    <input id="imgY1" type="hidden" runat ="server" />
    <input id="imgWidth" type="hidden" runat ="server"  />
    <input id="imgHeight" type="hidden"  runat ="server" />
      
    </form>
 
     <%--<asp:Image ID="SignatureImage" runat="server" />--%>
   <%-- <img id="SignatureImage"  src="" style="border-width: 0px"; runat ="server" />--%>
</body>
</html>
