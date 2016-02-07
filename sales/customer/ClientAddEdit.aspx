<%@ Page Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="ClientAddEdit.aspx.vb" Inherits="Client_ClientAddEdit" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">
 
 $(document).ready(function(){
 
 $(".crate").keypress(function(e){
 //console.log(e.keyCode);
    var charCode = (e.which) ? e.which : e.keyCode
     if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

         return true;

 });
  
 });
      
      
   </script>
   <style type="text/css" >
.lable-control
{
 text-align: right;
    float: left;
    width: 160px;
    padding-top: 4px;
}
.lable-control .required {
    color: #E02222;
    font-size: 12px;
    padding-left: 2px;
   
   }
   .controls
   {
   	margin-left :180px;
   }

</style>

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
                     
                    <%=txtname.UniqueID %>: {
                       required: true,
                    },
                    <%=txtaddress.UniqueID %>: {
                       required: true,
                    },
                     
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

                submitHandler: function (form) {
                    success1.show();
                    error1.hide();
                }
            });
            </script>



</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

<div class="row-fluid">
                         <!-- block -->
                  <div class="block">
                            <div class="navbar navbar-inner block-header">
                                <div class="muted pull-left">Customer Detail</div>
                            </div>
                            <div class="block-content collapse in">
                                <div class="span12">
					<!-- BEGIN FORM-->
					<form action="#" id="form_sample_1" class="form-horizontal">
						<fieldset>
							<div class="alert alert-error hide">
								<button class="close" data-dismiss="alert"></button>
								You have some form errors. Please check below.
							</div>
							<div class="alert alert-success hide">
								<button class="close" data-dismiss="alert"></button>
								Your form validation is successful!
							</div>
  							<div class="control-group">
  								<label class="lable-control">Customer Name<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtname" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Address<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtaddress" data-required="1"  TextMode="MultiLine" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>

                            <div class="control-group">
  								<label class="lable-control">City<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtcity" data-required="1"  class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Phone No<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtphno" data-required="1"   class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>

                             <div class="control-group">
  								<label class="lable-control">Fax No<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtfaxno" data-required="1"  class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">TIN<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txttin" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">CST<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtcst" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                             <div class="control-group">
  								<label class="lable-control">Email<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtemail" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>

                           <div class="control-group">
  								<label class="lable-control">Contact Person Name<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtcnname" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>

                             <div class="control-group">
  								<label class="lable-control">Contact Person Phone no<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtcnphnno" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                              <div class="control-group">
                            <label class="lable-control">Terms </label>
                            <div class="controls">
                                <table>
                                <tbody >
                                <tr>
                                <td><asp:RadioButton ID="against"  GroupName="Terms" runat="server" />
                                <asp:Label ID="Label1" runat="server" Text="  Against Perform"></asp:Label></td>
                                <td><asp:RadioButton ID="Immediate"    GroupName="Terms" runat="server" />
                                <asp:Label ID="Label2" runat="server" Text="  Immediate" ></asp:Label>
                                </td>
                                </tr>
                                 <tr>
                                <td> <asp:RadioButton ID="days15"   GroupName="Terms" runat="server" />
                                 <asp:Label ID="Label3" runat="server" Text="15 Days" ></asp:Label>
                                </td>
                                <td><asp:RadioButton ID="days30"   GroupName="Terms" runat="server" />
                                 <asp:Label ID="Label4" runat="server" Text="30 Days" ></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td> <asp:RadioButton ID="days45"    GroupName="Terms" runat="server" />
                                 <asp:Label ID="Label5" runat="server" Text="45 Days" ></asp:Label>
                                </td>
                                <td><asp:RadioButton ID="days60"   GroupName="Terms" runat="server" />
                                 <asp:Label ID="Label6" runat="server" Text="60 Days" ></asp:Label>
                                </td>
                                </tr>
                                 <tr>
                                <td> <asp:RadioButton ID="days90"   GroupName="Terms" runat="server" />
                                 <asp:Label ID="Label7" runat="server" Text="90 Days" ></asp:Label>
                                </td>
                                <td>
                                </td>
                                </tr>                       
                                </tbody>                               
                                </table>
  							</div>
                            </div> 
                        <div class="form-actions">
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" />
                                <asp:Button ID="btncancle" class="btn" runat="server" Text="Button" />
  								<asp:Label ID="lblcid" runat="server"></asp:Label>
  							</div>                                                
                       </fieldset>
					</form>
					<!-- END FORM-->
				</div>
			   </div>
			</div>
           
           
         <div class="row-fluid">
                        <!-- block -->
                 <div class="span6">
                  <div class="block">
                            <div class="navbar navbar-inner block-header">
                                <div class="muted pull-left">Customer Model </div>
                            </div>
                            <div class="block-content collapse in">
           
                       <asp:GridView ID="vsclientmodel" runat="server"  AutoGenerateColumns="False" Width="405px" Height="45px">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" />
                    </ItemTemplate> 
                    </asp:TemplateField> 
                            
                     <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"  />
                     <asp:BoundField DataField="Modelname" HeaderText="Model Name" ReadOnly="True"  />
                     <asp:BoundField DataField="Rate" HeaderText="Rate" ReadOnly="True" />
                     <asp:BoundField DataField="Assess_Rate" HeaderText="AssessableValue" ReadOnly="True" Visible ="False"   />
                     <asp:BoundField DataField="DiscountPer" HeaderText="Discount Percentage" ReadOnly="True" />
                         
                    <asp:TemplateField>
                    <ItemTemplate>
                    <asp:ImageButton ID="imgbtnDelete"  CommandName="Delete"  runat="server" ImageUrl="../images/delete.jpg"  ToolTip="Delete" Height="20px" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                                         
                </Columns>
        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
        
            
            </asp:GridView>
        <br />
        


        </div> 
        </div> 
        </div>
        
                        <!-- block -->
                 <div class="span6">
                     <div class="block">
                            <div class="navbar navbar-inner block-header">
                                <div class="muted pull-left">Customer Model Detail</div>
                            </div>
                            <div class="block-content collapse in">
                           <div class="control-group">
  								<label class="lable-control">Model Name search <span class="required">*</span></label>
  								<div class="controls">
                                 <asp:DropDownList ID="txtmodelname" runat="server">
                                 </asp:DropDownList>
                               </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Model Name <span class="required">*</span></label>
  								<div class="controls">
                                 <asp:DropDownList ID="cmbmodelname"  AutoPostBack ="true" EnableViewState ="true" runat="server">
                                 </asp:DropDownList>
                               </div>
  							</div>
                             <div class="control-group">
  								<label class="lable-control">Rate<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtrate" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                             <div class="control-group">
  								<label class="lable-control">Accessable Value<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtAssessValue" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>

              
         <div class="form-actions">
  								
                                <asp:Label ID="lblspid" runat="server" Text="Label"></asp:Label>								
                                <asp:Button ID="btnAdd"  class="btn btn-primary" runat="server" Text="Add" />
  							</div>
                      </div> 
                      </div> 
                   </div> 
 </div> 
        
        
        
   
</div> 
</asp:Content>