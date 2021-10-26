using System;
using System.Collections.Generic;
using System.Text;
using Inlite.ClearImageNet;

namespace PDFBarcodeReader
{
    class ClearImageBarcodeReader
    {
        private BarcodeReader reader;
        public ClearImageBarcodeReader() {
            reader = new BarcodeReader();
            reader.Auto1D = true;
        }

        public BarcodeResult[] DecodeFile(string path) {
            Barcode[] results = reader.Read(path);
            BarcodeResult[] barcodeResults = new BarcodeResult[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                Barcode result = results[i];
                BarcodeResult barcodeResult = new BarcodeResult();
                barcodeResult.x1 = result.TopLeft.X;
                barcodeResult.y1 = result.TopLeft.Y;
                barcodeResult.x2 = result.TopRight.X;
                barcodeResult.y2 = result.TopRight.Y;
                barcodeResult.x3 = result.BottomRight.X;
                barcodeResult.y3 = result.BottomRight.Y;
                barcodeResult.x4 = result.BottomLeft.X;
                barcodeResult.y4 = result.BottomLeft.Y;
                barcodeResult.barcodeText = result.Text;
                barcodeResult.barcodeFormat = result.Type.ToString();
                barcodeResults[i] = barcodeResult;
            }
            return barcodeResults;
        }

    }
}
