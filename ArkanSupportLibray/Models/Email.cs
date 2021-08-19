using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Linq;
using System.Threading.Tasks;
using ArkanSupportLibray.Models;
using AegisImplicitMail;

namespace ArkamSupportLibrary.Models
{
    public class Email
    {
        public DateTime Date { get; set; }
        public List<string> mailTo { get; set; }
        public string subject { get; set; }        
        public bool isHtml { get; set; }
        public Client client { get; set; }


        //private info
        private string mailFrom { get; set; }
        private string body { get; set; }
        private string login { get; set; }
        private string password { get; set; }
        private string host { get; set; }  //smtp client
        private int port { get; set; }

        public async Task send()
        {
            try
            {
                this.getEmailAccessInfo();
                this.makeBody();
                this.Date = DateTime.Now;
                SmtpClient mySmtpClient = new SmtpClient(this.host);


                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                   System.Net.NetworkCredential(this.login, this.password);
           //     mySmtpClient.EnableSsl = true;
                mySmtpClient.Credentials = basicAuthenticationInfo;
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                // add from,to mailaddresses                                
                string addresses = "";
                foreach (String mailTo in this.mailTo)
                {
                    addresses += mailTo;
                }

                MailMessage myMail = new System.Net.Mail.MailMessage(this.mailFrom, addresses);

                // add ReplyTo
                //MailAddress replyTo = new MailAddress("reply@example.com");
                //myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = this.subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = this.body;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = this.isHtml;

                await mySmtpClient.SendMailAsync(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void sendEmail()
        {
            this.getEmailAccessInfo();
            this.makeBody();
            this.Date = DateTime.Now;
            var mail = this.mailFrom;
            var host = this.host;
            var user = this.login;
            var pass = this.password;

            //Generate Message 
            var mymessage = new MimeMailMessage();
            mymessage.From = new MimeMailAddress(mail);
            mymessage.To.Add(mail);
            mymessage.Subject = this.subject;
            mymessage.IsBodyHtml = true;
            mymessage.Body = this.body;

            //Create Smtp Client
            var mailer = new MimeMailer(host, 465);
            mailer.User = user;
            mailer.Password = pass;
            mailer.SslType = SslMode.Ssl;
            mailer.AuthenticationMode = AuthenticationType.Base64;

            mailer.SendMailAsync(mymessage);
        }

        private void getEmailAccessInfo()
        {
            //this.mailFrom = "bbrizolara7@gmail.com";
            //this.login = "bbrizolara7@gmail.com";
            //this.password = "Brizo26580\"";
            //this.host = "smtp.gmail.com";
            //this.port = 587;

            this.mailFrom = "admin@arkamsoftware.com";
            this.login = "admin@arkamsoftware.com";
            this.password = "Brizo26580!!";
            this.host = "smtp.zoho.com";
            this.port = 465;

        }

        private void makeBody()
        {
            this.body = getEmailTemplate(client.clientName, client.clientEmail, client.projectName, client.projectDescription);
        }

        private string getEmailTemplate(string clientName, string clientEmail, string projectType, string projectDescription)
        {
            string emailTemplate = "";


            emailTemplate += "<!doctype html>";
            emailTemplate += "<html>";
            emailTemplate += "  <head>";
            emailTemplate += "    <meta name='viewport' content='width=device-width' />";
            emailTemplate += "    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            emailTemplate += "    <title>Client Contact Email</title>";
            emailTemplate += "    <style>      ";
            emailTemplate += "      img {";
            emailTemplate += "        border: none;";
            emailTemplate += "        -ms-interpolation-mode: bicubic;";
            emailTemplate += "        max-width: 100%; ";
            emailTemplate += "      }";
            emailTemplate += "      body {";
            emailTemplate += "        background-color: #f6f6f6;";
            emailTemplate += "        font-family: sans-serif;";
            emailTemplate += "        -webkit-font-smoothing: antialiased;";
            emailTemplate += "        font-size: 14px;";
            emailTemplate += "        line-height: 1.4;";
            emailTemplate += "        margin: 0;";
            emailTemplate += "        padding: 0;";
            emailTemplate += "        -ms-text-size-adjust: 100%;";
            emailTemplate += "        -webkit-text-size-adjust: 100%; ";
            emailTemplate += "      }";
            emailTemplate += "      table {";
            emailTemplate += "        border-collapse: separate;";
            emailTemplate += "        mso-table-lspace: 0pt;";
            emailTemplate += "        mso-table-rspace: 0pt;";
            emailTemplate += "        width: 100%; }";
            emailTemplate += "        table td {";
            emailTemplate += "          font-family: sans-serif;";
            emailTemplate += "          font-size: 14px;";
            emailTemplate += "          vertical-align: top; ";
            emailTemplate += "      }";
            emailTemplate += "      .body {";
            emailTemplate += "        background-color: #f6f6f6;";
            emailTemplate += "        width: 100%; ";
            emailTemplate += "      }";
            emailTemplate += "      .container {";
            emailTemplate += "        display: block;";
            emailTemplate += "        margin: 0 auto !important;";
            emailTemplate += "        /* makes it centered */";
            emailTemplate += "        max-width: 580px;";
            emailTemplate += "        padding: 10px;";
            emailTemplate += "        width: 580px; ";
            emailTemplate += "      }";
            emailTemplate += "      .content {";
            emailTemplate += "        box-sizing: border-box;";
            emailTemplate += "        display: block;";
            emailTemplate += "        margin: 0 auto;";
            emailTemplate += "        max-width: 580px;";
            emailTemplate += "        padding: 10px; ";
            emailTemplate += "      }";
            emailTemplate += "      .main {";
            emailTemplate += "        background: #ffffff;";
            emailTemplate += "        border-radius: 3px;";
            emailTemplate += "        width: 100%; ";
            emailTemplate += "      }";
            emailTemplate += "      .wrapper {";
            emailTemplate += "        box-sizing: border-box;";
            emailTemplate += "        padding: 20px; ";
            emailTemplate += "      }";
            emailTemplate += "      .content-block {";
            emailTemplate += "        padding-bottom: 10px;";
            emailTemplate += "        padding-top: 10px;";
            emailTemplate += "      }";
            emailTemplate += "      .footer {";
            emailTemplate += "        clear: both;";
            emailTemplate += "        margin-top: 10px;";
            emailTemplate += "        text-align: center;";
            emailTemplate += "        width: 100%; ";
            emailTemplate += "      }";
            emailTemplate += "        .footer td,";
            emailTemplate += "        .footer p,";
            emailTemplate += "        .footer span,";
            emailTemplate += "        .footer a {";
            emailTemplate += "          color: #999999;";
            emailTemplate += "          font-size: 12px;";
            emailTemplate += "          text-align: center; ";
            emailTemplate += "      }";
            emailTemplate += "      h1,";
            emailTemplate += "      h2,";
            emailTemplate += "      h3,";
            emailTemplate += "      h4 {";
            emailTemplate += "        color: #000000;";
            emailTemplate += "        font-family: sans-serif;";
            emailTemplate += "        font-weight: 400;";
            emailTemplate += "        line-height: 1.4;";
            emailTemplate += "        margin: 0;";
            emailTemplate += "        margin-bottom: 30px; ";
            emailTemplate += "      }";
            emailTemplate += "      h1 {";
            emailTemplate += "        font-size: 35px;";
            emailTemplate += "        font-weight: 300;";
            emailTemplate += "        text-align: center;";
            emailTemplate += "        text-transform: capitalize; ";
            emailTemplate += "      }";
            emailTemplate += "      p,";
            emailTemplate += "      ul,";
            emailTemplate += "      ol {";
            emailTemplate += "        font-family: sans-serif;";
            emailTemplate += "        font-size: 14px;";
            emailTemplate += "        font-weight: normal;";
            emailTemplate += "        margin: 0;";
            emailTemplate += "        margin-bottom: 15px; ";
            emailTemplate += "      }";
            emailTemplate += "        p li,";
            emailTemplate += "        ul li,";
            emailTemplate += "        ol li {";
            emailTemplate += "          list-style-position: inside;";
            emailTemplate += "          margin-left: 5px; ";
            emailTemplate += "      }";
            emailTemplate += "";
            emailTemplate += "      a {";
            emailTemplate += "        color: #3498db;";
            emailTemplate += "        text-decoration: underline; ";
            emailTemplate += "      }";
            emailTemplate += "      .btn {";
            emailTemplate += "        box-sizing: border-box;";
            emailTemplate += "        width: 100%; }";
            emailTemplate += "        .btn > tbody > tr > td {";
            emailTemplate += "          padding-bottom: 15px; }";
            emailTemplate += "        .btn table {";
            emailTemplate += "          width: auto; ";
            emailTemplate += "      }";
            emailTemplate += "        .btn table td {";
            emailTemplate += "          background-color: #ffffff;";
            emailTemplate += "          border-radius: 5px;";
            emailTemplate += "          text-align: center; ";
            emailTemplate += "      }";
            emailTemplate += "        .btn a {";
            emailTemplate += "          background-color: #ffffff;";
            emailTemplate += "          border: solid 1px #3498db;";
            emailTemplate += "          border-radius: 5px;";
            emailTemplate += "          box-sizing: border-box;";
            emailTemplate += "          color: #3498db;";
            emailTemplate += "          cursor: pointer;";
            emailTemplate += "          display: inline-block;";
            emailTemplate += "          font-size: 14px;";
            emailTemplate += "          font-weight: bold;";
            emailTemplate += "          margin: 0;";
            emailTemplate += "          padding: 12px 25px;";
            emailTemplate += "          text-decoration: none;";
            emailTemplate += "          text-transform: capitalize; ";
            emailTemplate += "      }";
            emailTemplate += "      .btn-primary table td {";
            emailTemplate += "        background-color: #3498db; ";
            emailTemplate += "      }";
            emailTemplate += "      .btn-primary a {";
            emailTemplate += "        background-color: #3498db;";
            emailTemplate += "        border-color: #3498db;";
            emailTemplate += "        color: #ffffff; ";
            emailTemplate += "      }";
            emailTemplate += "      .last {";
            emailTemplate += "        margin-bottom: 0; ";
            emailTemplate += "      }";
            emailTemplate += "      .first {";
            emailTemplate += "        margin-top: 0; ";
            emailTemplate += "      }";
            emailTemplate += "      .align-center {";
            emailTemplate += "        text-align: center; ";
            emailTemplate += "      }";
            emailTemplate += "      .align-right {";
            emailTemplate += "        text-align: right; ";
            emailTemplate += "      }";
            emailTemplate += "      .align-left {";
            emailTemplate += "        text-align: left; ";
            emailTemplate += "      }";
            emailTemplate += "      .clear {";
            emailTemplate += "        clear: both; ";
            emailTemplate += "      }";
            emailTemplate += "      .mt0 {";
            emailTemplate += "        margin-top: 0; ";
            emailTemplate += "      }";
            emailTemplate += "      .mb0 {";
            emailTemplate += "        margin-bottom: 0; ";
            emailTemplate += "      }";
            emailTemplate += "      .preheader {";
            emailTemplate += "        color: transparent;";
            emailTemplate += "        display: none;";
            emailTemplate += "        height: 0;";
            emailTemplate += "        max-height: 0;";
            emailTemplate += "        max-width: 0;";
            emailTemplate += "        opacity: 0;";
            emailTemplate += "        overflow: hidden;";
            emailTemplate += "        mso-hide: all;";
            emailTemplate += "        visibility: hidden;";
            emailTemplate += "        width: 0; ";
            emailTemplate += "      }";
            emailTemplate += "      .powered-by a {";
            emailTemplate += "        text-decoration: none; ";
            emailTemplate += "      }";
            emailTemplate += "      hr {";
            emailTemplate += "        border: 0;";
            emailTemplate += "        border-bottom: 1px solid #f6f6f6;";
            emailTemplate += "        margin: 20px 0; ";
            emailTemplate += "      }";
            emailTemplate += "      @media only screen and (max-width: 620px) {";
            emailTemplate += "        table[class=body] h1 {";
            emailTemplate += "          font-size: 28px !important;";
            emailTemplate += "          margin-bottom: 10px !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] p,";
            emailTemplate += "        table[class=body] ul,";
            emailTemplate += "        table[class=body] ol,";
            emailTemplate += "        table[class=body] td,";
            emailTemplate += "        table[class=body] span,";
            emailTemplate += "        table[class=body] a {";
            emailTemplate += "          font-size: 16px !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .wrapper,";
            emailTemplate += "        table[class=body] .article {";
            emailTemplate += "          padding: 10px !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .content {";
            emailTemplate += "          padding: 0 !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .container {";
            emailTemplate += "          padding: 0 !important;";
            emailTemplate += "          width: 100% !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .main {";
            emailTemplate += "          border-left-width: 0 !important;";
            emailTemplate += "          border-radius: 0 !important;";
            emailTemplate += "          border-right-width: 0 !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .btn table {";
            emailTemplate += "          width: 100% !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .btn a {";
            emailTemplate += "          width: 100% !important; ";
            emailTemplate += "        }";
            emailTemplate += "        table[class=body] .img-responsive {";
            emailTemplate += "          height: auto !important;";
            emailTemplate += "          max-width: 100% !important;";
            emailTemplate += "          width: auto !important; ";
            emailTemplate += "        }";
            emailTemplate += "      }";
            emailTemplate += "      @media all {";
            emailTemplate += "        .ExternalClass {";
            emailTemplate += "          width: 100%; ";
            emailTemplate += "        }";
            emailTemplate += "        .ExternalClass,";
            emailTemplate += "        .ExternalClass p,";
            emailTemplate += "        .ExternalClass span,";
            emailTemplate += "        .ExternalClass font,";
            emailTemplate += "        .ExternalClass td,";
            emailTemplate += "        .ExternalClass div {";
            emailTemplate += "          line-height: 100%; ";
            emailTemplate += "        }";
            emailTemplate += "        .apple-link a {";
            emailTemplate += "          color: inherit !important;";
            emailTemplate += "          font-family: inherit !important;";
            emailTemplate += "          font-size: inherit !important;";
            emailTemplate += "          font-weight: inherit !important;";
            emailTemplate += "          line-height: inherit !important;";
            emailTemplate += "          text-decoration: none !important; ";
            emailTemplate += "        }";
            emailTemplate += "        #MessageViewBody a {";
            emailTemplate += "          color: inherit;";
            emailTemplate += "          text-decoration: none;";
            emailTemplate += "          font-size: inherit;";
            emailTemplate += "          font-family: inherit;";
            emailTemplate += "          font-weight: inherit;";
            emailTemplate += "          line-height: inherit;";
            emailTemplate += "        }";
            emailTemplate += "        .btn-primary table td:hover {";
            emailTemplate += "          background-color: #34495e !important; ";
            emailTemplate += "        }";
            emailTemplate += "        .btn-primary a:hover {";
            emailTemplate += "          background-color: #34495e !important;";
            emailTemplate += "          border-color: #34495e !important; ";
            emailTemplate += "        } ";
            emailTemplate += "      }";
            emailTemplate += "    </style>";
            emailTemplate += "  </head>";
            emailTemplate += "  <body class=''>    ";
            emailTemplate += "    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>";
            emailTemplate += "      <tr>      ";
            emailTemplate += "        <td>&nbsp;</td>";
            emailTemplate += "        <td class='container'>";
            emailTemplate += "          <div class='content'>";
            emailTemplate += "            <table role='presentation' class='main'>";
            emailTemplate += "              <tr>";
            emailTemplate += "                <td class='wrapper'>";
            emailTemplate += "                  <h1>Arkam Software</h1>";
            emailTemplate += "                  <table role='presentation' border='0' cellpadding='0' cellspacing='0'>";
            emailTemplate += "                    <tr>";
            emailTemplate += "                      <td>";
            emailTemplate += "                      <br>";
            emailTemplate += "                        <center><h3>Nuevo contacto de Cliente</h3></center>";
            emailTemplate += "                        <p>Contacto de cliente enviado desde la web arkamsoftware.com</p><br>";
            emailTemplate += "                        <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='btn btn-primary'>";
            emailTemplate += "                          <tr>";
            emailTemplate += "                      <td>";
            emailTemplate += "                        <p><b>Nombre</b>: " + clientName + "</p>";
            emailTemplate += "                        <p><b>Tipo de Proyecto</b>: " + projectType + "</p>";
            emailTemplate += "                      </td>";
            emailTemplate += "                      <td>";
            emailTemplate += "                        <p><b>Email</b>: " + clientEmail + "</p>";
            emailTemplate += "                      </td>                      ";
            emailTemplate += "                    </tr>";
            emailTemplate += "                        </table>                        ";
            emailTemplate += "                        <p><b>Descripción del Proyecto</b>: " + projectDescription + "</p>";
            emailTemplate += "                      </td>";
            emailTemplate += "                    </tr>";
            emailTemplate += "                  </table>";
            emailTemplate += "                </td>";
            emailTemplate += "              </tr>";
            emailTemplate += "            </table>";
            emailTemplate += "            <div class='footer'>";
            emailTemplate += "              <table role='presentation' border='0' cellpadding='0' cellspacing='0'>";
            emailTemplate += "                <tr>";
            emailTemplate += "                  <td class='content-block'>";
            emailTemplate += "                    <span class='apple-link'><a href='https://arkamsoftware.com'><b>© Arkam Software " + DateTime.Now.Year.ToString() + "</b></a></span>";
            emailTemplate += "                    <span class='apple-link'></span>";
            emailTemplate += "                  </td>";
            emailTemplate += "                </tr>";
            emailTemplate += "                <tr>";
            emailTemplate += "                  <td class='content-block powered-by'>";
            emailTemplate += "                    We build technology.";
            emailTemplate += "                  </td>";
            emailTemplate += "                </tr>";
            emailTemplate += "              </table>";
            emailTemplate += "            </div>";
            emailTemplate += "          </div>";
            emailTemplate += "        </td>";
            emailTemplate += "        <td>&nbsp;</td>";
            emailTemplate += "      </tr>";
            emailTemplate += "    </table>";
            emailTemplate += "  </body>";
            emailTemplate += "</html>";
            return emailTemplate;
        }



    }
}
