using System;
using System.Drawing.Printing;

namespace TestPrinter {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Available Printers:");

            int cntr = 1;
            foreach (var printer in PrinterSettings.InstalledPrinters) {
                Console.WriteLine($"{cntr++}. {printer}");
            }
            Console.Write("Enter Desired Printer Number: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            string selectedPrinter = PrinterSettings.InstalledPrinters[choice - 1];

            Console.Write("Copies: ");
            int copies = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Available Paper Sizes:");
            cntr = 1;
            foreach (PaperSize ps in new PrinterSettings { PrinterName = selectedPrinter }.PaperSizes) {
                Console.WriteLine($"{cntr++}. {ps.PaperName}");
            }
            Console.Write("Enter Desired Printer Number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            string paperSize = new PrinterSettings { PrinterName = selectedPrinter }.PaperSizes[choice - 1].PaperName;

            // Create the printer settings for our printer
            var printerSettings = new PrinterSettings {
                PrinterName = selectedPrinter,
                Copies = (short)copies,
            };

            // Create our page settings for the paper size selected
            var pageSettings = new PageSettings(printerSettings) {
                Margins = new Margins(0, 0, 0, 0),
            };
            foreach (PaperSize ps in printerSettings.PaperSizes) {
                if (ps.PaperName == paperSize) {
                    pageSettings.PaperSize = ps;
                    break;
                }
            }


            string filepath = @"";

            PdfiumViewer.PdfDocument pdfDocument = PdfiumViewer.PdfDocument.Load(filepath);
            PrintDocument printDoc = pdfDocument.CreatePrintDocument(PdfiumViewer.PdfPrintMode.CutMargin);
            printDoc.PrinterSettings = printerSettings;
            printDoc.Print();
        }
    }
}
