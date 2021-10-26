using System;
using System.Collections.Generic;
using System.Text;
using Dynamsoft.DBR;

namespace PDFBarcodeReader
{
    class DynamsoftBarcodeReader
    {
        public BarcodeReader reader;
        public DynamsoftBarcodeReader()
        {
            reader = new BarcodeReader("t0070fQAAAHt4blHstT1COb70wKWHG+X1rt3gGP+R7FsqkBKsHMQ07ww3hduhGxDRU/Hi1TJvvd89qMjb1/ejSdKdiNCjOfEbDA==");
        }

        public void InitRuntimeSettingsWithString(string template) {
            string errorMessage;
            reader.InitRuntimeSettingsWithString(template, EnumConflictMode.CM_OVERWRITE, out errorMessage);
        }


        
        public BarcodeResult[] DecodeFile(string path) {
            Dynamsoft.TextResult[] results = reader.DecodeFile(path, "");
            BarcodeResult[] barcodeResults = new BarcodeResult[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                Dynamsoft.TextResult result = results[i];
                BarcodeResult barcodeResult = new BarcodeResult();
                barcodeResult.x1 = result.LocalizationResult.ResultPoints[0].X;
                barcodeResult.y1 = result.LocalizationResult.ResultPoints[0].Y;
                barcodeResult.x2 = result.LocalizationResult.ResultPoints[1].X;
                barcodeResult.y2 = result.LocalizationResult.ResultPoints[1].Y;
                barcodeResult.x3 = result.LocalizationResult.ResultPoints[2].X;
                barcodeResult.y3 = result.LocalizationResult.ResultPoints[2].Y;
                barcodeResult.x4 = result.LocalizationResult.ResultPoints[3].X;
                barcodeResult.y4 = result.LocalizationResult.ResultPoints[3].Y;
                barcodeResult.barcodeText = result.BarcodeText;
                barcodeResult.barcodeFormat = result.BarcodeFormatString;
                barcodeResults[i] = barcodeResult;
            }
            return barcodeResults;
        }



    }
}
