//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace NetCMS.Content.Common
{
    /// <summary>
    /// 生成水印、缩略图、生成有指定内容的图片
    /// </summary>
    public class FSImage
    {
        private int _width;
        private int _height;
        private string _title = "www.netcms";    //文字标题
        private Color _bgcolor = Color.White;
        private int _fontsize = 14;  //字号
        private string _familyname = "仿宋";   //字体
        private FontStyle _fontstyle = FontStyle.Regular;
        private Color _forcecolor = Color.Black;
        private string _filepath = "";   //获取路径及文件名
        private PointF _txtpos;
        private long _quality =100;
        private string _waterpos = "0";
        private string _waterpath = "";
        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="width">生成图片的宽度。
        /// 如果小于或等于零：高度也小于或等于零，则图片按原图的大小（水印或缩略图）或标题文字的大小生成图片；如高度大于零，则按高度优先比例缩放原图
        /// 如果大于零：高度也大于零，按指定大小生成新图片；如果高度小于等于零，则按宽度优先比较缩放图片</param>
        /// <param name="height">图片高度，见图片宽度注释</param>
        /// <param name="filepath">原文件物理磁盘路径（水印、缩略图）或保存文件路径（按文字生成图片）</param>
        public FSImage(int width, int height, string filepath)
        {
            _txtpos = new PointF(-1, -1);
            _width = width;
            _height = height;
            _filepath = filepath.Replace("/","\\");
        }
        /// <summary>
        /// 水印或生成图片的文字,默认为:www.netcms
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 以像素为单位的字体大小，默认为：14
        /// </summary>
        public int FontSize
        {
            get { return _fontsize; }
            set { _fontsize = value; }
        }
        /// <summary>
        /// 设置新图片的背景色,默认为透明
        /// </summary>
        public Color BackGroudColor
        {
            get { return _bgcolor; }
            set { _bgcolor = value; }
        }
        /// <summary>
        /// 采用的字体名称,默认为仿宋体
        /// </summary>
        public string FontFamilyName
        {
            get { return _familyname; }
            set { _familyname = value; }
        }
        /// <summary>
        /// 设置字体样式,默认为普通文本
        /// </summary>
        public FontStyle StrStyle
        {
            set { _fontstyle = value; }
            get { return _fontstyle; }
        }
        /// <summary>
        /// 水印文字的颜色,默认为红色
        /// </summary>
        public Color FontColor
        {
            set { _forcecolor = value; }
            get { return _forcecolor; }
        }
        /// <summary>
        /// 水印文本的位置坐标,以图片的左上角为原点,默认位置为图片的右下角
        /// 如果x座标小于零则文字在图片的右边,如果y座标小于0则文字在图片的下边
        /// 设置时注意这个位置是从文字的左上角为原点,如(0,0)表示文字在图片的左上角
        /// </summary>
        public PointF TextPos
        {
            set { _txtpos.X = value.X; _txtpos.Y = value.Y; }
            get { return _txtpos; }
        }

        /// <summary>
        /// 输出图片质量，质量范围0-100,类型为long
        /// </summary>
        public long Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /// <summary>
        /// 水印位置
        /// </summary>
        public string Waterpos
        {
            get { return _waterpos; }
            set { _waterpos = value; }
        }

        /// <summary>
        /// 水印图片宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 水印图片高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 水印图片地址
        /// </summary>
        public string WaterPath
        {
            get { return _waterpath; }
            set { _waterpath = value; }
        }

        /// <summary>
        /// 生成文字水印
        /// </summary>
        public void Watermark()
        {
            if (_title == null || _title.Equals(""))
                throw new NullReferenceException("图片标题为空!");
            ImageFormat format = GetFormat();
            if (!File.Exists(_filepath))
                throw new FileNotFoundException("指定路径的文件不存在");
            Graphics g;
            Bitmap bitmap = GetBitmap(out g);
            Font f = new Font(_familyname, _fontsize, _fontstyle, GraphicsUnit.Pixel);
            SizeF size = g.MeasureString(_title, f);
            switch (_waterpos)
            {
                case "1":
                    _txtpos.X = ((float)_width * (float).01) + (size.Width / 2) - 20;
                    _txtpos.Y = (float)_height * (float).01;
                    break;
                case "3":
                    _txtpos.X = ((float)_width * (float).99) - (size.Width);
                    _txtpos.Y = (float)_height * (float).01;
                    break;
                case "4":
                    _txtpos.X = ((float)_width * (float).99) - (size.Width);
                    _txtpos.Y = ((float)_height * (float).99) - size.Height;
                    break;
                case "2":
                    _txtpos.X = ((float)_width * (float).01) + (size.Width / 2) - 20;
                    _txtpos.Y = ((float)_height * (float).99) - size.Height;
                    break;
                default:
                    _txtpos.X = ((float)_width * (float).50) - 20;
                    _txtpos.Y = ((float)_height * (float).50);
                    break;
            }
            Brush b = new SolidBrush(_forcecolor);
            g.DrawString(_title, f, b, _txtpos);
            g.Dispose();
            bitmap.Save(_filepath, format);
            bitmap.Dispose();
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="newpath">缩略图的保存物理路径（包括文件名），如果为null或空则覆盖原图</param>
        public void Thumbnail(string newpath)
        {
            ImageFormat format = GetFormat();
            if (!File.Exists(_filepath))
                throw new FileNotFoundException("指定路径的文件不存在");
            Graphics g;
            Bitmap bitmap = GetBitmap(out g);
            if (newpath == null || newpath.Trim().Equals(""))
                bitmap.Save(_filepath, format);
            else
            {
                newpath = newpath.Replace("/","\\");
                if (newpath.IndexOf("\\") > 0)
                {
                    string dir = newpath.Substring(0, newpath.LastIndexOf("\\"));
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }
                bitmap.Save(newpath, format);
            }
            g.Dispose();
            bitmap.Dispose();
        }
        /// <summary>
        /// 生成文本图片
        /// </summary>
        public void GenerateTextPic()
        {
            if (_title == null || _title.Equals(""))
                throw new NullReferenceException("图片标题为空!");

            ImageFormat format = GetFormat();

            Font f = new Font(_familyname, _fontsize, _fontstyle, GraphicsUnit.Pixel);
            Bitmap _bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            Graphics _g = Graphics.FromImage(_bitmap);
            SizeF size = _g.MeasureString(_title, f);
            _g.Dispose();
            _bitmap.Dispose();
            double rate = size.Height / size.Width;
            if (_width <= 0 && _height <= 0)
            {
                _width = (int)size.Width;
                _height = (int)size.Height;
            }
            else if (_width > 0 && _height <= 0)
            {
                _height = (int)(_width * rate);
            }
            else if (_width <= 0 && _height > 0)
            {
                _width = (int)(_height / rate);
            }
            Bitmap bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(_bgcolor);
            if (_txtpos.X < 0)
                _txtpos.X = _width - size.Width;
            if (_txtpos.Y < 0)
                _txtpos.Y = _height - size.Height;
            Brush b = new SolidBrush(_forcecolor);
            g.DrawString(_title, f, b, _txtpos);
            g.Dispose();
            if (_filepath.IndexOf("\\") > 0)
            {
                string dir = _filepath.Substring(0, _filepath.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            bitmap.Save(_filepath, format);
            bitmap.Dispose();
        }
        /// <summary>
        /// 按照指定的大小生成图片，并将原图绘制过去
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        private Bitmap GetBitmap(out Graphics graphics)
        {
            //判断文件类型是否为图像类型
            Image image = Image.FromFile(_filepath);
            int x = image.Width;
            int y = image.Height;
            double rate = (float)y / (float)x;
            if (_width <= 0 && _height <= 0)
            {
                _width = x;
                _height = y;
            }
            else if (_width > 0 && _height <= 0)
            {
                _height = (int)(_width * rate);
            }
            else if (_width <= 0 && _height > 0)
            {
                _width = (int)(_height / rate);
            }
            Bitmap bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(_bgcolor);
            graphics.DrawImage(image, new Rectangle(0, 0, _width, _height));
            image.Dispose();
            return bitmap;
        }
        /// <summary>
        /// 根据文件扩展名取得图片的格式
        /// </summary>
        /// <returns></returns>
        private ImageFormat GetFormat()
        {
            if (_filepath == null || _filepath.IndexOf(".") < 0)
                throw new FileNotFoundException("文件路径不能为空或是文件名不正确!");
            string extend = _filepath.Substring(_filepath.LastIndexOf(".")).ToLower();
            switch (extend)
            {
                case ".jpe":
                    return ImageFormat.Jpeg;
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".tif":
                    return ImageFormat.Tiff;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".gif":
                    return ImageFormat.Gif;
                default:
                    throw new FormatException("文件格式不受支持!");
            }
        }


        /// <summary>
        ///  生成图片水印
        /// </summary>
        public void WaterPicturemark()
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(_filepath);
            Bitmap b = new Bitmap(image.Width, image.Height,PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);

            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            WaterPicturmark(g, image.Height, image.Width);
            image.Dispose();

            b.Save(_filepath);
            b.Dispose();
        }

        protected void WaterPicturmark(Graphics g, int height, int width)
        {
            try
            {
                Image watermark = new Bitmap(_waterpath);
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int xpos = 0;
                int ypos = 0;
                int WatermarkWidth = 0;
                int WatermarkHeight = 0;
                double bl = 1d;
                //计算水印图片的比率
                //取背景的1/4宽度来比较
                if ((width > watermark.Width * 4) && (height > watermark.Height * 4))
                {
                    bl = 1;
                }
                else if ((width > watermark.Width * 4) && (height < watermark.Height * 4))
                {
                    bl = Convert.ToDouble(height / 4) / Convert.ToDouble(watermark.Height);
                }
                else if ((width < watermark.Width * 4) && (height > watermark.Height * 4))
                {
                    bl = Convert.ToDouble(width / 4) / Convert.ToDouble(watermark.Width);
                }
                else
                {
                    if ((width * watermark.Height) > (_height * watermark.Width))
                    {
                        bl = Convert.ToDouble(height / 4) / Convert.ToDouble(watermark.Height);
                    }
                    else
                    {
                        bl = Convert.ToDouble(width / 4) / Convert.ToDouble(watermark.Width);
                    }
                }

                WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
                WatermarkHeight = Convert.ToInt32(watermark.Height * bl);

                switch (_waterpos)
                {
                    case "1":
                        xpos = 10;
                        ypos = 10;
                        break;
                    case "3":
                        xpos = width - WatermarkWidth - 10;
                        ypos = 10;
                        break;
                    case "4":
                        xpos = width - WatermarkWidth - 10;
                        ypos = height - WatermarkHeight - 10;
                        break;
                    case "2":
                        xpos = 10;
                        ypos = height - WatermarkHeight - 10;
                        break;
                    default:
                        xpos = Convert.ToInt32(width / 2) - (WatermarkWidth / 2);
                        ypos = Convert.ToInt32(height / 2) - (WatermarkHeight / 2);
                        break;
                }
                g.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                watermark.Dispose();
                imageAttributes.Dispose();
            }catch{}
        }
    }
}
