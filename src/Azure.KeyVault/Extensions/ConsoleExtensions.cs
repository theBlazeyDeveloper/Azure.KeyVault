using Azure;
using System;

namespace VisAbility.Azure.KeyVault
{
    internal static class ConsoleEx
    {
        private readonly static string messageHeader = "********************Azure Key Vault************************ \n";

        /// <summary>
        /// Writes the exception message to the console with the specified background and foreground colors
        /// </summary>
        /// <param name="Background">Background Color</param>
        /// <param name="Foreground">Foreground Color</param>
        /// <param name="Ex">Exception</param>
        /// <param name="Message">Message</param>
        public static void WriteLine(ConsoleColor? Background, ConsoleColor? Foreground, Exception Ex = null, string Message = null)
        {
            if (Background.HasValue)
                Console.BackgroundColor = Background.Value;

            if (Foreground.HasValue)
                Console.ForegroundColor = Foreground.Value;

            if (Ex == null)
            {
                var displayMessage = messageHeader;
                displayMessage += Message;

                Console.WriteLine(displayMessage);
            }
            else
                Console.WriteLine(CreateMessage(Ex));
        }
        /// <summary>
        /// Writes the exception message to the console with a red background and white text
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Ex"></param>
        public static void WriteLineRed(string Message = null, Exception Ex = null)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            if (Ex == null)
            {
                var displayMessage = messageHeader;
                displayMessage += Message;

                Console.WriteLine(displayMessage);
            }
            else
                Console.WriteLine(CreateMessage(Ex));
        }

        /// <summary>
        /// Writes the exception message to the console with a green background and black text
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Ex"></param>
        public static void WriteLineGreen(string Message = null, Exception Ex = null)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            if (Ex == null)
            {
                var displayMessage = messageHeader;
                displayMessage += Message;

                Console.WriteLine(displayMessage);
            }
            else
                Console.WriteLine(CreateMessage(Ex));
        }

        /// <summary>
        /// Writes the exception message to the console with a yellow background and black text
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Ex"></param>
        public static void WriteLineYellow(string Message = null, Exception Ex = null)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;

            if (Ex == null)
            {
                var displayMessage = messageHeader;
                displayMessage += Message;

                Console.WriteLine(displayMessage);
            }
            else
                Console.WriteLine(CreateMessage(Ex));
        }

        /// <summary>
        /// Writes the exception message to the console with a blue background and white text
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Ex"></param>
        public static void WriteLineBlue(string Message = null, Exception Ex = null)
        {

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            if (Ex == null)
            {
                var displayMessage = messageHeader;
                displayMessage += Message;

                Console.WriteLine(displayMessage);
            }
            else
                Console.WriteLine(CreateMessage(Ex));
        }

        private static string CreateMessage(Exception Ex)
        {
            return Ex switch
            {
                RequestFailedException e => HandleException(e),
                _ => Ex.Message,
            };
        }
        private static string HandleException(RequestFailedException e)
        {
            string message = messageHeader;
            var source = "";

            var RequestInfo = e.Message;
            if (RequestInfo != null)
            {
                var ExtendedInfo = RequestInfo;

                if (ExtendedInfo != null)
                {
                    source = e.Source;
                }
                var TargetSiteName = e.TargetSite?.Name;
                var Body = e.Message;

                message += $"Target Site: {TargetSiteName}\n" +
                          $"Body: {Body}\n" +
                          $"Source: {source}\n";
            }
            else
                message += $"Exception: {e.Message}";

            return message;
        }
    }
}