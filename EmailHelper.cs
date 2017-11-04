using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace JM0ney.Framework.Web {

    public static class EmailHelper {

        public const String EmailSmtpServerSettingName = "JM0ney:EmailSmtpServer";
        public const String EmailSmtpPortSettingName = "JM0ney:EmailSmtpPort";
        public const String EmailAddressFromSettingName = "JM0ney:EmailAddressFrom";
        public const String EmailAddressPasswordSettingName = "JM0ney:EmailAddressPassword";
        public const String EmailAddressReplyToSettingName = "JM0ney:EmailAddressReplyTo";

        public static Result SendEmail( MailMessage emailMessage ) {
            String message = String.Empty;
            List<String> settingsToValidate = new List<String>( );
            Result returnValue = Result.ErrorResult( Localization.EmailHelper.ErrorMessages.EmailNotSent );

            // Validate the settings
            settingsToValidate.Add( EmailHelper.EmailSmtpServerSettingName );
            settingsToValidate.Add( EmailHelper.EmailAddressFromSettingName );
            settingsToValidate.Add( EmailHelper.EmailAddressPasswordSettingName );

            foreach ( String setting in settingsToValidate ) {
                if ( !ConfigurationHelper.IsAppSettingConfigured( setting, ref message ) )
                    return Result.ErrorResult( message ); // Implement _FS {0}
            }

            int realPortNumber = 0;
            String smtpHost = ConfigurationHelper.TryGetValue( EmailHelper.EmailSmtpServerSettingName, String.Empty );
            String portNumber = ConfigurationHelper.TryGetValue( EmailHelper.EmailSmtpPortSettingName, String.Empty );
            String fromAccount = ConfigurationHelper.TryGetValue( EmailHelper.EmailAddressFromSettingName, String.Empty );
            String accountPassword = ConfigurationHelper.TryGetValue( EmailHelper.EmailAddressPasswordSettingName, String.Empty );

            // Instantiate a client, send the email, dispose of unmanaged resources
            using ( SmtpClient smtpRelay = new SmtpClient( smtpHost ) ) {
                if ( !String.IsNullOrWhiteSpace( portNumber ) && int.TryParse( portNumber, out realPortNumber ) )
                    smtpRelay.Port = int.Parse( portNumber );

                smtpRelay.Credentials = new System.Net.NetworkCredential( fromAccount, accountPassword );
                try {
                    smtpRelay.Send( emailMessage );
                    returnValue = Result.SuccessResult( Localization.EmailHelper.Messages.EmailSent );
                }
                catch ( Exception ex ) {
                    // Not sent - configure Result
                    returnValue = Result.ErrorResult( Localization.EmailHelper.ErrorMessages.EmailNotSent, ex );
                }
            }

            return returnValue;
        }

        public static Result SendEmail( String toEmailAddress, String subject, String messageBody, bool isBodyHtml = true, String replyTo = "" ) {
            String[ ] toAddresses = toEmailAddress.Split( new[ ] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            return EmailHelper.SendEmail( toAddresses, subject, messageBody, isBodyHtml, replyTo );
        }

        public static Result SendEmail( IEnumerable<String> toEmailAddresses, String subject, String messageBody, bool isBodyHtml = true, String replyTo = "" ) {
            String replyToAddress = String.Empty;
            MailMessage emailMessage = new MailMessage( );
            String message = String.Empty;
            String fromAddress = String.Empty;

            fromAddress = ConfigurationHelper.TryGetValue( EmailAddressFromSettingName, String.Empty );
            if ( !String.IsNullOrWhiteSpace( replyTo ) ) {
                replyToAddress = replyTo;
            }
            else {
                if ( ConfigurationHelper.IsAppSettingConfigured( EmailHelper.EmailAddressReplyToSettingName, ref message ) )
                    replyToAddress = ConfigurationHelper.TryGetValue( EmailHelper.EmailAddressReplyToSettingName, String.Empty );
                else
                    replyToAddress = fromAddress;
            }

            // Configure the email address
            emailMessage.Body = messageBody;
            emailMessage.From = new MailAddress( fromAddress );
            emailMessage.IsBodyHtml = isBodyHtml;
            emailMessage.ReplyToList.Add( new MailAddress( replyToAddress ) );
            emailMessage.Subject = subject;
            foreach ( String recipient in toEmailAddresses ) {
                MailAddress address = new MailAddress( recipient );
                emailMessage.To.Add( address );
            }

            Result returnValue = EmailHelper.SendEmail( emailMessage );
            return returnValue;
        }

    }
}
