<%@ Control Language="VB" AutoEventWireup="false" CodeFile="COLogin.ascx.vb" Inherits="Co_Apps_ControlOrigins_COLogin" %>


    <div id="page-wrapper">

        <div class="row">

            <div class="col-lg-12 text-center v-center">
                <h1 class="title"><%= AppInfo.ApplicationNM %></h1>
                <br>
                <asp:Panel ID="LoginErrorMessage" Visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <strong>Login was not sucessfull.</strong> Please try again.
                    <br>
                </asp:Panel>

                <h2>User Login</h2>
                <p class="lead">Enter your email and password to login</p>


                <div class="col-lg-12">
                    <div class="input-group" style="width: 340px; text-align: center; margin: 0 auto;">
                        <asp:TextBox ValidationGroup="SignIn" ID="tbEmail" runat="server" class="form-control input-lg" title="Confidential signup."
                            placeholder="Enter your email address" type="text" />
                    </div>
                    <div class="input-group" style="width: 340px; text-align: center; margin: 0 auto;">
                        <asp:TextBox ValidationGroup="SignIn" ID="tbpass" runat="server" class="form-control input-lg" title="Password Entery" placeholder="********" type="password" />

                        <span class="input-group-btn">
                            <asp:Button ValidationGroup="SignIn" ID="cmd_login" OnClick="cmd_login_Click" runat="server" class="btn btn-lg btn-primary" type="button" Text="Login"></asp:Button>
                        </span>
                    </div>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="SignIn" ControlToValidate="tbEmail" ID="RequiredFieldValidator1" runat="server" ErrorMessage="<span class='label label-danger'>Email Required</span>"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="SignIn" ControlToValidate="tbpass" ID="RequiredFieldValidator2" runat="server" ErrorMessage="<span class='label label-danger'>Password Required</span>"></asp:RequiredFieldValidator>


                </div>






            </div>





        </div>

        <!-- /.row -->



    </div>

