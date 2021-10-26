using System;

namespace PDFBarcodeReader
{
    class Program
    {
        static void Main(string[] args)
        {   
            DynamsoftBarcodeReader dbr = new DynamsoftBarcodeReader();
            BarcodeResult[] results;
            Dynamsoft.DBR.PublicRuntimeSettings settings = dbr.reader.GetRuntimeSettings();
            settings.PDFRasterDPI = 200;
            settings.PDFReadingMode = Dynamsoft.DBR.EnumPDFReadingMode.PDFRM_RASTER;
            dbr.reader.UpdateRuntimeSettings(settings);

            long startTime;
            long elapsedTime;
            
            startTime = DateTime.Now.Ticks;
            results = dbr.DecodeFile("F:\\Vector_PDF_Sample.pdf");
            elapsedTime = (DateTime.Now.Ticks - startTime) / 10000;
            Console.WriteLine("Found " + results.Length + " barcodes in "+ elapsedTime +" ms.");

            settings.PDFReadingMode = Dynamsoft.DBR.EnumPDFReadingMode.PDFRM_VECTOR;
            dbr.reader.UpdateRuntimeSettings(settings);

            startTime = DateTime.Now.Ticks;
            results = dbr.DecodeFile("F:\\Vector_PDF_Sample.pdf");
            elapsedTime = (DateTime.Now.Ticks - startTime) / 10000;
            Console.WriteLine("Found " + results.Length + " barcodes in " + elapsedTime + " ms.");

            VintasoftBarcodeReader vinta = new VintasoftBarcodeReader();
            startTime = DateTime.Now.Ticks;
            results = vinta.DecodeFile("F:\\Vector_PDF_Sample.pdf");
            elapsedTime = (DateTime.Now.Ticks - startTime) / 10000;
            Console.WriteLine("Found " + results.Length + " barcodes in " + elapsedTime + " ms.");

            ClearImageBarcodeReader clearImage = new ClearImageBarcodeReader();
            startTime = DateTime.Now.Ticks;
            clearImage.DecodeFile("F:\\Vector_PDF_Sample.pdf");
            elapsedTime = (DateTime.Now.Ticks - startTime) / 10000;
            Console.WriteLine("Found " + results.Length + " barcodes in " + elapsedTime + " ms.");

        }
    }
}
