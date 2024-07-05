using QRCoder;
using System.Drawing;

namespace TO.WebApp.MVC.Extensions;

public static class QrCodeGenerator
{
    public static string GenerateImage(string id)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);
        Base64QRCode qrCode = new Base64QRCode(qrCodeData);
        string qrCodeImageAsBase64 = qrCode.GetGraphic(20);
        return qrCodeImageAsBase64;
    }

    //public static byte[] GenerateByteArray(string url)
    //{
    //    var image = GenerateImage(url);
    //    return ImageToByte(image);
    //}

    //private static byte[] ImageToByte(Image img)
    //{
    //    using var stream = new MemoryStream();
    //    img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
    //    return stream.ToArray();
    //}
}