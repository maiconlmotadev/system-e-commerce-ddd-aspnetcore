
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using QRCoder;
using SixLabors.ImageSharp;
using System.Composition;
using System.Drawing;



namespace Web_ECommerce.Models
{
    public class HelpQrCode : Controller
    {
        private async Task<byte[]> GenerateQrCode (string bankDetails)
        {
            // Lib de gerar Qrcode:  QRCodeGenerator
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator ();

            // Recebe os dados do QrCode
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(bankDetails, QRCodeGenerator.ECCLevel.H);

            // Criando o QRCode
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            var bitMapBytes = BitmapToBytes (qrCodeImage);

            return bitMapBytes;

        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream ms = new MemoryStream()) 
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
        }

        public async Task<IActionResult> Download(UserBuy userBuy, IWebHostEnvironment _environment)
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                #region Sheet Settings 
                var page = doc.AddPage();

                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                var graphics = XGraphics.FromPdfPage(page);
                var fontColor = XBrushes.Black;
                #endregion

                #region Page Numbering
                int pageQtd = doc.PageCount;

                var pageNumbering = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                pageNumbering.DrawString(Convert.ToString(pageQtd), new PdfSharpCore.Drawing.XFont("Arial", 10), fontColor, new PdfSharpCore.Drawing.XRect(575, 825, page.Width, page.Height));
                #endregion

                #region Logo 
                var webRoot = _environment.WebRootPath;

                var invoiceLogo = string.Concat(webRoot, "/img/", "loja-virtual-1.png");

                XImage imagem = XImage.FromFile(invoiceLogo);

                graphics.DrawImage(imagem, 20, 5, 300, 50);
                #endregion

                #region Information 1
                var billingReport = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                var title = new PdfSharpCore.Drawing.XFont("Arial", 14, PdfSharpCore.Drawing.XFontStyle.Bold);

                billingReport.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;

                billingReport.DrawString("BOLETO ONLINE", title, fontColor, new XRect(0, 65, page.Width, page.Height));

                #endregion

                #region Informations 2

                var detailsTitleHeightY = 120;

                var details = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                var InfoTitle_1 = new PdfSharpCore.Drawing.XFont("Arial", 8, XFontStyle.Regular);

                details.DrawString("Bank details", InfoTitle_1, fontColor, new XRect(25, detailsTitleHeightY, page.Width, page.Height));
                details.DrawString("Active Bank 007", InfoTitle_1, fontColor, new XRect(150, detailsTitleHeightY, page.Width, page.Height));

                detailsTitleHeightY += 9;
                details.DrawString("Generated Code", InfoTitle_1, fontColor, new XRect(25, detailsTitleHeightY, page.Width, page.Height));
                details.DrawString("000000 000000 000000 000000", InfoTitle_1, fontColor, new XRect(150, detailsTitleHeightY, page.Width, page.Height));


                detailsTitleHeightY += 9;
                details.DrawString("Quantidade:", InfoTitle_1, fontColor, new XRect(25, detailsTitleHeightY, page.Width, page.Height));
                details.DrawString(userBuy.ProductsQuantity.ToString(), InfoTitle_1, fontColor, new XRect(150, detailsTitleHeightY, page.Width, page.Height));

                detailsTitleHeightY += 9;
                details.DrawString("Amount:", InfoTitle_1, fontColor, new XRect(25, detailsTitleHeightY, page.Width, page.Height));
                details.DrawString(userBuy.ProductsQuantity.ToString(), InfoTitle_1, fontColor, new XRect(150, detailsTitleHeightY, page.Width, page.Height));

                var tituloInfo_2 = new PdfSharpCore.Drawing.XFont("Arial", 8, XFontStyle.Bold);


                try
                {
                    var img = await GenerateQrCode("Bank details here");

                    Stream streamImage = new MemoryStream(img);

                    XImage qrCode = XImage.FromStream(() => streamImage);

                    detailsTitleHeightY += 40;
                    graphics.DrawImage(qrCode, 140, detailsTitleHeightY, 310, 310);
                }
                catch (Exception error)
                {

                }

                detailsTitleHeightY += 620;
                details.DrawString("Invoice with QrCode for online payment.", tituloInfo_2, fontColor, new XRect(20, detailsTitleHeightY, page.Width, page.Height));

                #endregion

            }
        }               
        
    }
}
