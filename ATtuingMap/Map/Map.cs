using System;
using System.Collections.Generic;
using System.Drawing;
using Point = ATtuingMap.Point;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    public class Map : IDisposable
    {


        public Map() : this(new Size(300, 150))
        {

        }
        /// <summary>
        /// 初始化一个新地图对象
        /// </summary>
        /// <param name="size"></param>
        public Map(Size size)
        {
            Size = size;
            _Center = new Point(0, 0);
            _Zoom = 1;
            _PixelAspectRatio = 1.0;
        }
        private double _Zoom;
        private Size _Size;
        private Point _Center;
        private double _PixelAspectRatio = 1.0;
        /// <summary>
        /// 地图缩放等级
        /// </summary>
        public double Zoom
        {
            get { return _Zoom; }
            set
            {
                _Zoom = value;

            }
        }

        /// <summary>
        /// 地图画布大小
        /// </summary>
        public Size Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        /// <summary>
        /// 空间坐标中心
        /// </summary>
        public Point Center
        {
            get { return _Center; }
            set
            {
                _Center = value;
            }
        }
        /// <summary>
        /// 像素的长宽比
        /// </summary>
        public double PixelAspectRatio
        {
            get { return _PixelAspectRatio; }
            set
            {
                if (_PixelAspectRatio <= 0)
                    throw new ArgumentException("Invalid Pixel Aspect Ratio");
                _PixelAspectRatio = value;
            }
        }
        /// <summary>
        /// 一个像素的高度对应空间坐标单位高度
        /// </summary>
        public double PixelHeight
        {
            get { return PixelSize * _PixelAspectRatio; }
        }
        /// <summary>
        /// 一个像素的宽度对应空间坐标单位宽度度
        /// </summary>
        public double PixelWidth
        {
            get { return PixelSize; }
        }
        /// <summary>
        /// 一个像素的宽度对应空间坐标单位宽度（一个像素所代表的实际宽度）
        /// </summary>
        public double PixelSize
        {
            get { return Zoom / Size.Width; }
        }
        /// <summary>
        /// 在空间坐标系下的显示高度
        /// </summary>
        public double MapHeight
        {
            get { return (Zoom * Size.Height) / Size.Width * PixelAspectRatio; }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
