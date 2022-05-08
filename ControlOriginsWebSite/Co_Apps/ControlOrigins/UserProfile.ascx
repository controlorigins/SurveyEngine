<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserProfile.ascx.vb" Inherits="UserProfile" %>
<%@ Register Src="~/controls/AlertBox.ascx" TagName="AlertBox" TagPrefix="uc1" %>
<div id="page-wrapper">
    <div class="row">
        <uc1:AlertBox ID="AlertBox1" runat="server" alertType="success" boldnote="Saved" dismissable="True" message="Your profile information has been updated." Visible="False" />
    </div>
    <div class="row text-center">
        <h1>User Profile</h1>
    </div>
    <br />

    <label for="firstname" class="col-md-2">
        First Name:
    </label>
    <div class="col-md-9">
        <asp:TextBox ValidationGroup="regForm" class="form-control" ID="firstname" runat="server" placeholder="Enter First Name" />
        <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="firstname" ID="RequiredFieldValidator1" runat="server" ErrorMessage="User First Name is Required"></asp:RequiredFieldValidator>
    </div>


    <label for="lastname" class="col-md-2">
        Last Name:
    </label>
    <div class="col-md-9">
        <asp:TextBox ValidationGroup="regForm" runat="server" type="text" class="form-control" ID="lastname" placeholder="Enter Last Name" />
        <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="lastname" ID="RequiredFieldValidator2" runat="server" ErrorMessage="User Last Name is Required"></asp:RequiredFieldValidator>
    </div>
    <label for="emailaddress" class="col-md-2">
        Email address:
    </label>
    <div class="col-md-9">
        <asp:TextBox ValidationGroup="regForm" runat="server" type="email" class="form-control" ID="emailaddress" placeholder="Enter email address" />
        <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="emailaddress" ID="RequiredFieldValidator3" runat="server" ErrorMessage="User Email Address is Required"></asp:RequiredFieldValidator>
    </div>
    <br />
    <label for="emailaddress" class="col-md-2">
        Portal Account (Login):
    </label>
    <div class="col-md-9">
        <asp:TextBox ValidationGroup="regForm" runat="server" type="text" class="form-control" ID="tbAccountNM" placeholder="Enter Portal Login" />
        <asp:RequiredFieldValidator ForeColor="Orange" Display="Dynamic" ValidationGroup="regForm" ControlToValidate="tbAccountNM" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Portal Login is Required"></asp:RequiredFieldValidator>
    </div>
    <br />

    <br />
    <br />

    <label for="cmd_RegNewUser" class="col-md-2">
        Update Profile
    </label>

    <div class="col-md-10">
        <asp:Button ValidationGroup="regForm" OnClick="cmd_RegNewUser_Click" ID="cmd_RegNewUser" runat="server" type="submit" class="btn btn-primary" Text="Save" />
    </div>
    <br />
    <br />
    <br />
    <br />

    <div class="col-sm-12">
        <asp:LinkButton ID="cmd_editpass" runat="server" OnClick="cmd_editpass_Click">Edit Password</asp:LinkButton>
        <br />
        <br />


        <asp:Panel ID="PassChange" runat="server" Visible="false">
            <div class="col-md-12">
                <label for="">Current Password</label>
                <asp:TextBox ValidationGroup="passupdate" ID="tbOLDPASS" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator  Display="Dynamic"  ForeColor="Orange" ValidationGroup="passupdate" ControlToValidate="tbOLDPASS"  ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-12">
                <label for="">New Password</label>
                <asp:TextBox ValidationGroup="passupdate"  ID="tbNEWPASS" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator  Display="Dynamic"  ForeColor="Orange"  ValidationGroup="passupdate" ControlToValidate="tbNEWPASS"  ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                  </div>
            <div class="col-md-12">
                <label for="">Verify Password</label>
                <asp:TextBox ValidationGroup="passupdate"  ID="tbNEWPASSVER" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator  Display="Dynamic"   ForeColor="Orange"   ValidationGroup="passupdate" ControlToValidate="tbNEWPASSVER"  ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:CompareValidator  Display="Dynamic"   ForeColor="Orange"   ValidationGroup="passupdate"  ControlToValidate ="tbNEWPASSVER" ControlToCompare="tbNEWPASS" ID="CompareValidator1" runat="server" ErrorMessage="Password do not match."></asp:CompareValidator>
            </div>
            <br />
            <div class="col-md-12">
                <asp:LinkButton ValidationGroup="passupdate"  OnClick="cmd_cancelSave_Click" ID="cmd_cancelSave" CausesValidation="false" CssClass="btn btn-warning" runat="server">Cancel</asp:LinkButton>
                <asp:LinkButton ValidationGroup="passupdate"  ID="cmd_saveNewPass" runat="server" CssClass="btn btn-primary">Update Password</asp:LinkButton>
            </div>
        </asp:Panel>
    </div>
</div>
