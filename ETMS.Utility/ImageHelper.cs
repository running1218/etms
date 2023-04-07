
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ETMS.Utility
{
    /// <summary>
    /// ͼƬ���Է�ʽ
    /// </summary>
    public enum ImageThumbnailMode
    {
        /// <summary>
        /// ָ���ĸ߿����ţ����ܱ��Σ�
        /// </summary>
        HW,

        /// <summary>
        /// ָ���߿����ţ����ܱ��Σ�����С�򲻱䣩
        /// </summary>
        HWO,

        /// <summary>
        /// ָ�����߰�����
        /// </summary>
        W,

        /// <summary>
        /// ָ������С�򲻱䣩���߰�����
        /// </summary>
        WO,

        /// <summary>
        /// ָ���ߣ�������
        /// </summary>
        H,

        /// <summary>
        /// ָ���ߣ���С�򲻱䣩��������
        /// </summary>
        HO,

        /// <summary>
        /// ָ���߿�ü��������Σ�
        /// </summary>
        CUT,
        /// <summary>
        /// �ȱ������ţ������Σ���ָ�����߿�
        /// </summary>
        Equimultiple
    }
    /// <summary>
    /// ����ͼƬ�����һЩʵ�÷���
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// ����ͼƬ������ͼ
        /// </summary>
        /// <param name="originalImage">ԭͼ</param>
        /// <param name="width">����ͼ�Ŀ����أ�</param>
        /// <param name="height">����ͼ�ĸߣ����أ�</param>
        /// <param name="mode">���Է�ʽ</param>
        /// <returns>����ͼ</returns>
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
                case ImageThumbnailMode.HW: //ָ���߿����ţ����ܱ��Σ�
                    break;
                case ImageThumbnailMode.HWO: //ָ���߿����ţ����ܱ��Σ�����С�򲻱䣩
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
                case ImageThumbnailMode.W: //ָ�����߰�����
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ImageThumbnailMode.WO: //ָ������С�򲻱䣩���߰�����
                    if (originalImage.Width <= width)
                    {
                        return originalImage;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case ImageThumbnailMode.H: //ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ImageThumbnailMode.HO: //ָ���ߣ���С�򲻱䣩��������
                    if (originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case ImageThumbnailMode.CUT: //ָ���߿�ü��������Σ�
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
                case ImageThumbnailMode.Equimultiple: //�ȱ������ţ������Σ�
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

            //�½�һ��bmpͼƬ
            Image bitmap = new Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                        new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// ����ͼƬ������ͼ
        /// </summary>
        /// <param name="originalStream">ԭͼ</param>
        /// <param name="thumbnailPath">��������ͼ��·��</param>
        /// <param name="width">����ͼ�Ŀ����أ�</param>
        /// <param name="height">����ͼ�ĸߣ����أ�</param>
        /// <param name="mode">���Է�ʽ</param>
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
        /// ����ͼƬ������ͼ
        /// </summary>
        /// <param name="originalImage">ԭͼ</param>
        /// <param name="thumbnailPath">��������ͼ��·��</param>
        /// <param name="width">����ͼ�Ŀ����أ�</param>
        /// <param name="height">����ͼ�ĸߣ����أ�</param>
        /// <param name="mode">���Է�ʽ</param>
        public static void MakeThumbnail(Image originalImage, string thumbnailPath, int width, int height, ImageThumbnailMode mode)
        {
            Image bitmap = MakeThumbnail(originalImage, width, height, mode);
            try
            {
                //��jpg��ʽ��������ͼ
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// ����ͼƬ������ͼ
        /// </summary>
        /// <param name="originalImagePath">ԭͼ��·��</param>
        /// <param name="thumbnailPath">��������ͼ��·��</param>
        /// <param name="width">����ͼ�Ŀ����أ�</param>
        /// <param name="height">����ͼ�ĸߣ����أ�</param>
        /// <param name="mode">���Է�ʽ</param>
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
