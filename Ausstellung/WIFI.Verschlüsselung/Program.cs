using System;

namespace WIFI.Verschlüsselung
{
    class Program
    {
        /// <summary>
        /// Interne Verschlüsselungs-Software um Benutzerdaten für die Datenbank zu verschlüsseln
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.WriteLine("Verschlüsselungsmanager");

            var input = string.Empty;
            do
            {

                Console.WriteLine("Gibt einen Text ein welcher Verschlüsselt werden soll: ");
                Console.WriteLine(new Anwendung.MySqlClient.Encryptor().Encrypt(Console.ReadLine()));

                Console.WriteLine("Gibt einen Text ein welcher Entschlüsselt werden soll: ");
                Console.WriteLine(new Anwendung.MySqlClient.Encryptor().Decrypt(Console.ReadLine()));

                Console.WriteLine("\n " +
                                  "\n " +
                                  "Wollen sie fortsetzen? (y): ");

                input = Console.ReadLine();
            } while (input == string.Empty && !(input == "y") );



        }
    }
}
