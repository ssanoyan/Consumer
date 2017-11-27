using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Mail;
using System.Text;


namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "admin", Password = "7851/-*Chkdsk" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ProjecBuilded",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    


                };
                channel.BasicConsume(queue: "ProjecBuilded",
                                     autoAck: true,
                                     consumer: consumer);
                MailSender();
            }
        }

        static void MailSender()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("wf.exam.jenkins@gmail.com");
                mail.To.Add("sargis.sanoyan@gmail.com");
                mail.Subject = "Jenkins has completed building successfully!";
                mail.Body = "Here will be inserted a detailed log about building :)" ;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("wf.exam.jenkins", "7851/-*Chkdsk");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {              
            }
        }
    }
}
