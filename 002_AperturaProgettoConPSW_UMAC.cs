using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siemens.Engineering;
using System.IO;
using System.Security;

namespace AperturaProgettoProtetto
{
    class Program
    {
        static void Main(string[] args)
        {
            //Apertura TIA Portal
            Console.WriteLine(@"Inserisci il progetto protetto nella directory C:\Esempio\ProgettoTestPSW\ProgettoTestPSW.ap17 e premi invio");
            Console.ReadLine();
            TiaPortal mioTiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface);

            //Prima di aprire il progetto preparo il tipo di dato UmacDelegate da passare al metodo 'Open'.
            //Essendo UmacDelegate una classe di tipo delegate UmacDelegate(UmacCredentials) creo (in fondo) il metodo da associare al delegate 
            UmacDelegate mioPuntatoreUmacDelegate = mioUmacDelegate; 
          
            //Apro il progetto passando in ingresso il path e il metodo appena creato
            Project mioProgetto = mioTiaPortal.Projects.Open(new FileInfo(@"C:\Esempio\ProgettoTestPSW\ProgettoTestPSW.ap17"), mioPuntatoreUmacDelegate);
            
        }

        // Definizione del metodo da associare al delegate
        private static void mioUmacDelegate(UmacCredentials umacCredentials)
        {
            //Definisco il tipo di accesso e lo UserName
            umacCredentials.Type = UmacUserType.Project;
            umacCredentials.Name = "Admin";

            //La PSW non può essere definita direttamente con una stringa dato che è di tipo SecureString costruibile carattere per carattere
            SecureString password = new SecureString(); 
            string PSW = "Admin123!";
            foreach (char mioCharX in PSW)
            {
                password.AppendChar(mioCharX);
            }
            umacCredentials.SetPassword(password);
        }
    }
}