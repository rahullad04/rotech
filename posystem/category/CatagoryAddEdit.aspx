<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="CatagoryAddEdit.aspx.vb"  EnableViewState="true" Inherits="Part_CatagoryAddEdit" Title="Category Add-Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lable-control
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 5px;
        }
        .lable-control .required
        {
            color: #E02222;
            font-size: 12px;
            padding-left: 2px;
        }
        .controls
        {
            margin-left: 180px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
        $(function () {
            $.ajax({
                type: "POST",
                url: 'CatagoryAddEdit.aspx/GetClassname',
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var cmbclass = $("[id*=cmbclass]");
                    cmbclass.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(r.d, function () {
                        cmbclass.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        });
    </script>

    <script type="text/javascript">
var FormValidation = function () {

    var handleValidation1 = function() {
        // for more info visit the official plugin documentation: 
            // http://docs.jquery.com/Plugins/Validation
            //http://www.c-sharpcorner.com/UploadFile/ee01e6/jquery-validation-with-Asp-Net-web-form/

            var form1 = $('#aspnetForm');
            var error1 = $('.alert-error', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                     <%=txtcatagory.UniqueID %>: {
                        required: true,
                    },
                    <%=cmbclass.UniqueID %>: {
                       required: true,
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    FormValidation.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                },

//                submitHandler: function (form) {
//                    success1.show();
//                    error1.hide();
//                }


                submitHandler: function(form) {
               // $("#form1").submit();
               form1.submit();
                      }
            });
    }

    return {
        //main function to initiate the module
        init: function () {

            handleValidation1();

        },

	// wrapper function to scroll to an element
        scrollTo: function (el, offeset) {
            pos = el ? el.offset().top : 0;
            jQuery('html,body').animate({
                    scrollTop: pos + (offeset ? offeset : 0)
                }, 'slow');
        }

    };

}();
--%>

<script type="text/javascript">
    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            case 'Warning':
                cssclass = 'alert-warning'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        $(".close").click(function () {
            $("#alert_container").fadeOut("slow", function () {
                $(this).html("");
            });
        });
    }
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Category Detail</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <!-- BEGIN FORM-->
                    <form action="#" id="form_sample_1" class="form-horizontal">
                    <fieldset>
                    <div id="alert_container">
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="updateaddedit" UpdateMode="Conditional" runat="server" >
                        <ContentTemplate>
                          <div class="control-group">
                            <label class="lable-control">
                                Category Name<span class="required">*</span></label>
                            <div class="controls">
                                <asp:TextBox ID="txtcatagory" name="txtcatagory" type="text" data-required="1" class="span6 m-wrap"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="lable-control">
                                Class <span class="required">*</span></label>
                            <div class="controls">
                                <asp:DropDownList ID="cmbclass" runat="server">
                                </asp:DropDownList>
                                <%--<asp:DropDownList ID="cmbclass" AutoPostBack="true" EnableViewState="true" runat="server"
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Text="Select" Value=""></asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        
                        </ContentTemplate>
                        <Triggers >
                        <asp:AsyncPostBackTrigger ControlID="btncancle" EventName="Click" />
                        </Triggers>
                        </asp:UpdatePanel>
                       
                        <div class="form-actions">
                            <asp:Button ID="btnSubmit" ClientIDMode="Static" class="btn btn-primary" runat="server"
                                Text="Save"  OnClick =" btnSubmit_Click"/>
                            <asp:Button ID="btncancle" class="btn btn-primary" OnClick="btncancle_click" runat="server" Text="Reset" />
                            <asp:Label ID="lblid" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                    </form>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
