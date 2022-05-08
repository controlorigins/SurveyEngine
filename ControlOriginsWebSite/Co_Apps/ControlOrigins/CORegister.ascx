<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CORegister.ascx.vb" Inherits="Co_Apps_ControlOrigins_CORegister" %>

<div id="page-wrapper">
    <div class="row text-center">
        <h1 class="title"><%= AppInfo.ApplicationNM %></h1>
        <br />
        <h2>New Registration</h2>
    </div>
    <div>
        <label for="firstname" class="col-md-2">
            First Name:
        </label>
        <div class="col-md-9">
            <asp:TextBox ValidationGroup="regForm" class="form-control" ID="firstname" runat="server" placeholder="Enter First Name" />
            <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="firstname" ID="RequiredFieldValidator1" runat="server" ErrorMessage="User First Name is Required"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-1 hidden-xs hidden-sm">
            <i class="fa fa-lock fa-2x"></i>
        </div>
    </div>
    <div>
        <label for="lastname" class="col-md-2">
            Last Name:
        </label>
        <div class="col-md-9">
            <asp:TextBox ValidationGroup="regForm" runat="server" type="text" class="form-control" ID="lastname" placeholder="Enter Last Name" />
            <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="lastname" ID="RequiredFieldValidator2" runat="server" ErrorMessage="User Last Name is Required"></asp:RequiredFieldValidator>

        </div>
        <div class="col-md-1  hidden-xs hidden-sm">
            <i class="fa fa-lock fa-2x"></i>
        </div>
    </div>
    <div>
        <label for="emailaddress" class="col-md-2">
            Email address:
        </label>
        <div class="col-md-9">
            <asp:TextBox ValidationGroup="regForm" runat="server" type="email" class="form-control" ID="emailaddress" placeholder="Enter email address" />
            <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="emailaddress" ID="RequiredFieldValidator3" runat="server" ErrorMessage="User Email Address is Required"></asp:RequiredFieldValidator>
            <p class="help-block">
                Example: yourname@domain.com
            </p>
        </div>
        <div class="col-md-1  hidden-xs hidden-sm">
            <i class="fa fa-lock fa-2x"></i>
        </div>
    </div>
    <div>
        <label for="password" class="col-md-2">
            Password:
        </label>
        <div class="col-md-9">
            <asp:TextBox ValidationGroup="regForm" runat="server" type="password" class="form-control" ID="password" placeholder="Enter Password" />
            <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="password" ID="RequiredFieldValidator4" runat="server" ErrorMessage="User Password is Required"></asp:RequiredFieldValidator>
            <p class="help-block">
                Min: 6 characters (Alphanumeric only)
            </p>
        </div>
        <div class="col-md-1 hidden-xs hidden-sm">
            <i class="fa fa-lock fa-2x"></i>
        </div>
    </div>


    <div>
        <label for="password" class="col-md-2">
            Password Confirm:
        </label>
        <div class="col-md-9">
            <asp:TextBox ValidationGroup="regForm" runat="server" type="password" class="form-control" ID="passwordconfirm" placeholder="Enter Password" />

            <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="passwordconfirm" ID="RequiredFieldValidator5" runat="server" ErrorMessage="User Password Verification is Required"></asp:RequiredFieldValidator>
            <asp:CompareValidator ForeColor="Orange" Display="Dynamic" ID="CompareValidator1" runat="server" ControlToCompare="password" ControlToValidate="passwordconfirm" ErrorMessage="Passwords must match"></asp:CompareValidator>
        </div>
        <div class="col-md-1 hidden-xs hidden-sm">
            <i class="fa fa-lock fa-2x"></i>
        </div>
    </div>

    <br />

    <div>
        <div class="col-md-2">
        </div>
        <div class="col-md-10">
            <label>
                <asp:CheckBox ID="acceptconditions" runat="server" ValidationGroup="regForm" type="checkbox" />
                I hereby accept the terms and conditions for using this site</label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-10">
            <asp:Button ValidationGroup="regForm" ID="cmd_RegNewUser" runat="server" type="submit" class="btn btn-primary" OnClick="cmd_RegNewUser_Click" Text="Register" />

        </div>
    </div>
</div>

