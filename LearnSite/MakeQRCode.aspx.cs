using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace ETMS.Studying
{
    public partial class MakeQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["data"]))
            {
                string str = Request.QueryString["data"];

                //初始化二维码生成工具
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeScale = 4;

                //将字符串生成二维码图片
                Bitmap image = qrCodeEncoder.Encode(str, Encoding.Default);

                //保存为PNG到内存流  
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);

                //输出二维码图片
                Response.BinaryWrite(ms.GetBuffer());
                Response.End();

                //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                //qrCodeEncoder.QRCodeScale = 4;  // 二维码大小
                //Bitmap image = qrCodeEncoder.Encode(str, Encoding.Default);
                //MemoryStream ms = new MemoryStream();
                //image.Save(ms, ImageFormat.Png);
                //HttpContext.Current.Response.ContentType = "image/x-png";
                //HttpContext.Current.Response.BinaryWrite(ms.GetBuffer());
            }
        }
    }
}