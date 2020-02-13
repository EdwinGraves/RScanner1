using Motorola.Snapi;
using Motorola.Snapi.Constants;
using Motorola.Snapi.Constants.Enums;
using Motorola.Snapi.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Initlizing Scanner Driver...");

            BarcodeScannerManager.Instance.Open();

            BarcodeScannerManager.Instance.RegisterForEvents(EventType.Barcode);
            BarcodeScannerManager.Instance.DataReceived += OnDataReceived;

            Console.WriteLine("Scanning for hardware...");

            List<IMotorolaBarcodeScanner> _scanners = BarcodeScannerManager.Instance.GetDevices();

            Console.WriteLine(String.Format("Found {0} Devices.", _scanners.Count));
            if (_scanners.Count == 0)
            {
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();
                return;
            }

            foreach (var scanner in _scanners)
            {
                scanner.SetHostMode(HostMode.USB_SNAPI_Imaging);
            }

            Console.WriteLine("Entering an infinite loop. Press ESCAPE to close the application.");

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            { }

            Console.WriteLine("Exiting");
        }

        private static void OnDataReceived(object sender, BarcodeScanEventArgs e)
        {
            Console.WriteLine("Scan Successful");
            Console.WriteLine("Data: " + e.Data);
        }
    }
}