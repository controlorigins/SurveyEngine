Imports CODataCon.com.controlorigins.ws

Partial Class Co_Apps_ControlOrigins_CoUserMessages
    Inherits ApplicationControlBase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserID.Value = UserInfo.ApplicationUserID

        messagelist.DataSource = UserInfo.Messages
        messagelist.DataBind()

    End Sub
    Protected Sub cmd_Refress_Click()
        messagelist.DataBind()
        MyMessagesModals.DataSource = UserInfo.Messages.ToList()
        MyMessagesModals.DataBind()
        UserInfo.MessageCount = UserInfo.MessageCount - 1
    End Sub

    Protected Sub cmd_deleteMessage_Click(sender As Object, e As EventArgs)
        DeleteMessage(CType(sender, LinkButton).Attributes("data-MessageID"))
        cmd_Refress_Click()
    End Sub

    Protected Sub cmd_replay_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub MessageFormControl_MailSent() Handles MessageFormControl.MailSent
        cmd_Refress_Click()
    End Sub

End Class
