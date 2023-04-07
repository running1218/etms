
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ETMS.Utility
{
    /// <summary>
    /// 图片缩略方式
    /// </summary>
    public enum ImageThumbnailMode
    {
        /// <summary>
        /// 指定的高宽缩放（可能变形）
        /// </summary>
        HW,

        /// <summary>
        /// 指定高宽缩放（可能变形）（过小则不变）
        /// </summary>
        HWO,

        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        W,

        /// <summary>
        /// 指定宽（过小则不变），高按比例
        /// </summary>
        WO,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,

        /// <summary>
        /// 指定高（过小则不变），宽按比例
        /// </summary>
        HO,

        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        CUT,
        /// <summary>
        /// 等比例缩放（不变形），指定最大高宽
        /// </summary>
        Equimultiple
    }
    /// <summary>
    /// 关于图片处理的一些实用方法
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式</param>
        /// <returns>缩略图</returns>
        /// <remarks>
        /// </remarks>
        public static Image MakeThumbnail(Image originalImage, int width, int height, ImageThumbnailMode mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ImageThumbnailMode.HW: //指定高宽缩放（可能变形）
                    break;
                case ImageThumbnailMode.HWO: //指定高宽缩放（可能变形）（过小则不变）
                    if (originalImage.Width <= width && originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    if (originalImage.Width < width)
                    {
                        towidth = originalImage.Width;
                    }
                    if (originalImage.Height < height)
                    {
                        toheight = originalImage.Height;
                    }
                    break;
                case ImageThumbnailMode.W: //指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ImageThumbnailMode.WO: //指定宽（过小则不变），高按比例
                    if (originalImage.Width <= width)
                    {
                        return originalImage;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case ImageThumbnailMode.H: //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ImageThumbnailMode.HO: //指定高（过小则不变），宽按比例
                    if (originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case ImageThumbnailMode.CUT: //指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case ImageThumbnailMode.Equimultiple: //等比例缩放（不变形）
                    if (originalImage.Width <= width && originalImage.Height <= height)
                    {
                        towidth = originalImage.Width;
                        toheight = originalImage.Height;
                    }
                    else
                    {
                        if (originalImage.Height / originalImage.Width >= height / width)
                        {
                            toheight = height;
                            towidth = originalImage.Width * toheight / originalImage.Height;
                        }
                        else
                        {
                            towidth = width;
                            toheight = originalImage.Height * towidth / originalImage.Width;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                        new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalStream">原图</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式</param>
        public static void MakeThumbnail(Stream originalStream, string thumbnailPath, int width, int height, ImageThumbnailMode mode)
        {
            Image originalImage = Image.FromStream(originalStream);
            try
            {
                MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
            }
            finally
            {
                originalImage.Dispose();
            }
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式</param>
        public static void MakeThumbnail(Image originalImage, string thumbnailPath, int width, int height, ImageThumbnailMode mode)
        {
            Image bitmap = MakeThumbnail(originalImage, width, height, mode);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImagePath">原图的路径</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ImageThumbnailMode mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            try
            {
                MakeThumbnail(originalImage, thumbnailPath,width, height, mode);
            }
            finally
            {
                originalImage.Dispose();
            }
        }
    }
}
