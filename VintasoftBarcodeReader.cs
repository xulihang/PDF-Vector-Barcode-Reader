using System;
using System.Collections.Generic;
using System.Text;
using Vintasoft.Barcode;
using Vintasoft.Barcode.SymbologySubsets;

namespace PDFBarcodeReader
{
    class VintasoftBarcodeReader
    {
        private BarcodeReader reader;
        public VintasoftBarcodeReader()
        {
            Vintasoft.Barcode.BarcodeGlobalSettings.Register("Tony Xu Lihang", "xulihanghai@163.com", "2021-11-22", "xpRq8S8ndgJTdedTf/Bi2V81m42f4zmuXFnStPVHDEUbvIMHgJ6AWusH4lgRkv7xxqixnZPEfNE3hbOPaF2R28SAabbVbTFRLCwtmeowBBzHE9ImvV+QkmLJk75toFOihlL57Vq0r8/Vb8Zzh+M1afdJ9KbroXDbxElB1MmDRTU");
            Vintasoft.Imaging.ImagingGlobalSettings.Register("Tony Xu Lihang", "xulihanghai@163.com", "2021-11-26", "2+VC7zvj9TeX344TfjB5Mqm10olM6D5wYILLkWDV5qUDDncfOQFM7iXedo+8W3+b8lcebbYNDh/Q9BgCFnF7YIyCfBO8HrKgVRuCuoQCDr8O8qUlc8uE90WmxxtGqx76p9R/D/YIxFQgnPf+5MGI9S9PuF2pu/9UyvmZPST3qI0");
            reader = new BarcodeReader();
            
        }

        public BarcodeResult[] DecodeFile(string path) {
            List<IBarcodeInfo> infos = ReadBarcodesFromVectorPDFDocument(path);
            IBarcodeInfo[] results = infos.ToArray();
            BarcodeResult[] barcodeResults = new BarcodeResult[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                IBarcodeInfo result = results[i];
                BarcodeResult barcodeResult = new BarcodeResult();
                barcodeResult.x1 = result.Region.LeftTop.X;
                barcodeResult.y1 = result.Region.LeftTop.Y;
                barcodeResult.x2 = result.Region.RightTop.X;
                barcodeResult.y2 = result.Region.RightTop.Y;
                barcodeResult.x3 = result.Region.RightBottom.X;
                barcodeResult.y3 = result.Region.RightBottom.Y;
                barcodeResult.x4 = result.Region.LeftBottom.X;
                barcodeResult.y4 = result.Region.LeftBottom.Y;
                barcodeResult.barcodeText = result.Value;
                barcodeResult.barcodeFormat = result.BarcodeType.ToString();
                barcodeResults[i] = barcodeResult;
            }
            return barcodeResults;
        }
        // The project, which uses this code, must have references to the following assemblies:
        // - Vintasoft.Barcode

        /// <summary>
        /// Reads barcodes from a vector PDF document.
        /// </summary>
        /// <param name="pdfFilename">A path to a PDF document.</param>
        public List<IBarcodeInfo> ReadBarcodesFromVectorPDFDocument(string pdfFilename)
        {
            List<IBarcodeInfo> infosOfImages = new List<IBarcodeInfo>();
            // create the image collection
            using (Vintasoft.Imaging.ImageCollection pdfPages = new Vintasoft.Imaging.ImageCollection())
            {
                // add PDF document to the image collection
                Console.WriteLine(pdfFilename);
                pdfPages.Add(pdfFilename);

                // set the rendering settings if necessary
                pdfPages.SetRenderingSettings(new Vintasoft.Imaging.Codecs.Decoders.RenderingSettings(new Vintasoft.Imaging.Resolution(200, 200)));

                // for each PDF page
                foreach (Vintasoft.Imaging.VintasoftImage image in pdfPages)
                {
                    // get page image
                    using (System.Drawing.Image pageImage = image.GetAsBitmap())
                    {
                        // read barcodes from image
                        infosOfImages.AddRange(ReadBarcodesFromImage(pageImage));
                    }
                }

                // clear image collection
                pdfPages.ClearAndDisposeItems();
            }
            return infosOfImages;
        }

        /// <summary>
        /// Reads barcodes from an image.
        /// </summary>
        /// <param name="barcodeImage">An image with barcodes.</param>
        public IBarcodeInfo[] ReadBarcodesFromImage(System.Drawing.Image barcodeImage)
        {
            SetAllBarcodeTypes(reader.Settings);

            // specify that reader must search for horizontal and vertical barcodes only
            reader.Settings.ScanDirection = ScanDirection.Horizontal | ScanDirection.Vertical;

            // use Automatic Recognition
            reader.Settings.AutomaticRecognition = true;

            // read barcodes from image
            IBarcodeInfo[] infos = reader.ReadBarcodes(barcodeImage);

            return infos;
        }

        /// <summary>
        /// Sets all supported barcode types and subsets to specified reader settings.
        /// </summary>
        /// <param name="settings">The reader settings.</param>
        private static void SetAllBarcodeTypes(ReaderSettings settings)
        {
            //settings.ScanBarcodeTypes |= BarcodeType.PatchCode; // not supported in demo version
            //settings.ScanBarcodeTypes |= BarcodeType.Pharmacode;

            settings.ScanBarcodeTypes |= BarcodeType.AustralianPost;
            settings.ScanBarcodeTypes |= BarcodeType.Aztec;
            settings.ScanBarcodeTypes |= BarcodeType.Codabar;
            settings.ScanBarcodeTypes |= BarcodeType.Code11;
            settings.ScanBarcodeTypes |= BarcodeType.Code128;
            settings.ScanBarcodeTypes |= BarcodeType.Code16K;
            settings.ScanBarcodeTypes |= BarcodeType.Code39;
            settings.ScanBarcodeTypes |= BarcodeType.Code93;
            settings.ScanBarcodeTypes |= BarcodeType.DataMatrix;
            settings.ScanBarcodeTypes |= BarcodeType.DutchKIX;
            settings.ScanBarcodeTypes |= BarcodeType.EAN13;
            settings.ScanBarcodeTypes |= BarcodeType.EAN13Plus2;
            settings.ScanBarcodeTypes |= BarcodeType.EAN13Plus5;
            settings.ScanBarcodeTypes |= BarcodeType.EAN8;
            settings.ScanBarcodeTypes |= BarcodeType.EAN8Plus2;
            settings.ScanBarcodeTypes |= BarcodeType.EAN8Plus5;
            settings.ScanBarcodeTypes |= BarcodeType.HanXinCode;
            settings.ScanBarcodeTypes |= BarcodeType.IATA2of5;
            settings.ScanBarcodeTypes |= BarcodeType.IntelligentMail;
            settings.ScanBarcodeTypes |= BarcodeType.Interleaved2of5;
            settings.ScanBarcodeTypes |= BarcodeType.Mailmark4StateC;
            settings.ScanBarcodeTypes |= BarcodeType.Mailmark4StateL;
            settings.ScanBarcodeTypes |= BarcodeType.Matrix2of5;
            settings.ScanBarcodeTypes |= BarcodeType.MaxiCode;
            settings.ScanBarcodeTypes |= BarcodeType.MicroPDF417;
            settings.ScanBarcodeTypes |= BarcodeType.MicroQR;
            settings.ScanBarcodeTypes |= BarcodeType.MSI;
            settings.ScanBarcodeTypes |= BarcodeType.PDF417;
            settings.ScanBarcodeTypes |= BarcodeType.PDF417Compact;
            settings.ScanBarcodeTypes |= BarcodeType.Planet;
            settings.ScanBarcodeTypes |= BarcodeType.Postnet;
            settings.ScanBarcodeTypes |= BarcodeType.QR;
            settings.ScanBarcodeTypes |= BarcodeType.RoyalMail;
            settings.ScanBarcodeTypes |= BarcodeType.RSS14;
            settings.ScanBarcodeTypes |= BarcodeType.RSS14Stacked;
            settings.ScanBarcodeTypes |= BarcodeType.RSSExpanded;
            settings.ScanBarcodeTypes |= BarcodeType.RSSExpandedStacked;
            settings.ScanBarcodeTypes |= BarcodeType.RSSLimited;
            settings.ScanBarcodeTypes |= BarcodeType.Standard2of5;
            settings.ScanBarcodeTypes |= BarcodeType.Telepen;
            settings.ScanBarcodeTypes |= BarcodeType.UPCA;
            settings.ScanBarcodeTypes |= BarcodeType.UPCAPlus2;
            settings.ScanBarcodeTypes |= BarcodeType.UPCAPlus5;
            settings.ScanBarcodeTypes |= BarcodeType.UPCE;
            settings.ScanBarcodeTypes |= BarcodeType.UPCEPlus2;
            settings.ScanBarcodeTypes |= BarcodeType.UPCEPlus5;

            settings.ScanBarcodeSubsets.AddRange(BarcodeSymbologySubsets.GetSupportedBarcodeSymbologySubsets());
        }
    }
}
