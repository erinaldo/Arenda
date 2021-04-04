using System;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using EmailValidation;

namespace dllJournalLoad1C
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ValidateEmail("urist@vetosnspb.ru");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    Application.Run(new frmJournalLoad1C());

                    Project.clearBufferFiles();
                }
        }

        private static bool ValidateEmail(string email)
        {
            EmailValidator emailValidator = new EmailValidator();
            EmailValidationResult result;

            if (!emailValidator.Validate(email, out result))
            {
                Console.WriteLine("Unable to check email"); // no internet connection or mailserver is down / busy
                return false;
            }

            bool isOK = false;

            switch (result)
            {
                case EmailValidationResult.OK:
                    Console.WriteLine("Mailbox exists");
                    isOK = true;
                    break;

                case EmailValidationResult.MailboxUnavailable:
                    Console.WriteLine("Email server replied there is no such mailbox");
                    isOK = false;
                    break;

                case EmailValidationResult.MailboxStorageExceeded:
                    Console.WriteLine("Mailbox overflow");
                    isOK = false;
                    break;

                case EmailValidationResult.NoMailForDomain:
                    Console.WriteLine("Emails are not configured for domain (no MX records)");
                    isOK = false;
                    break;
            }

            return isOK;
        }
    }
}
