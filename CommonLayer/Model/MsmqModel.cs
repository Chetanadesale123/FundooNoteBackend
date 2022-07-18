using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MsmqModel
    {
        MessageQueue msqQueue = new MessageQueue();
        public void MsmqSend(string token)
        {
            //Setting the QueuPath where we want to store the messages.
            msqQueue.Path = @".\private$\Bills";
            if (!MessageQueue.Exists(msqQueue.Path))
            {
                MessageQueue.Create(msqQueue.Path);
            }
            msqQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msqQueue.ReceiveCompleted += MsqQueue_ReceiveCompleted;
            msqQueue.Send(token);
            msqQueue.BeginReceive();
            msqQueue.Close();
        }
        private void MsqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = msqQueue.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "Fundoo Notes password reset";
            string body = token;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("testcoding654@gmail.com", "chhqzjgvyqndzjot"),
                EnableSsl = true
            };
            smtpClient.Send("testcoding654@gmail.com", "testcoding654@gmail.com", subject, body);
            msqQueue.BeginReceive();
        }
    }
}
