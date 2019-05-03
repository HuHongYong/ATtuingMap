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

        public BoundingBox Envelope
        {
            get
            {

                Point ll = new Point(Center.X - Zoom * .5, Center.Y - MapHeight * .5);
                Point ur = new Point(Center.X + Zoom * .5, Center.Y + MapHeight * .5);
                return new BoundingBox(ll, ur);

                //Point lb = new Point(Center.X - Zoom*.5, Center.Y - MapHeight*.5);
                //Point rt = new Point(Center.X + Zoom*.5, Center.Y + MapHeight*.5);
                //return new BoundingBox(lb, rt);
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

        public List<VectorLayer> Layers { get; set; } = new
             List<VectorLayer>();
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
        /// <summary>
        /// 获取渲染后的地图
        /// </summary>
        /// <returns></returns>
        public Image GetMap()
        {
            Image img = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(img);
            RenderMap(g);
            g.Dispose();
            return img;
        }
        /// <summary>
        /// 渲染地图
        /// </summary>
        /// <param name="g"></param>
        public void RenderMap(Graphics g)
        {
            foreach (var layer in Layers)
            {
                layer.Render(g, this);
            }
        }
        /// <summary>
        /// 获取最大外包矩形
        /// </summary>
        /// <returns></returns>
        public BoundingBox GetExtents()
        {
            BoundingBox maxbb = null;
            foreach (var layer in Layers)
            {
                if (maxbb != null)
                {
                    maxbb = layer.DataSource.Envelope == null ? maxbb : maxbb.Join(layer.DataSource.Envelope);
                }
                else
                {
                    maxbb = layer.DataSource.Envelope;
                }
            }
            return maxbb;
        }
        /// <summary>
        /// 全图显示
        /// </summary>
        public void ZoomToExtents()
        {
            ZoomToBox(GetExtents());
        }
        /// <summary>
        /// 缩放带矩形可视范围
        /// </summary>
        /// <param name="bbox"></param>
        public void ZoomToBox(BoundingBox bbox)
        {
            if (bbox != null)
            {
                _Zoom = bbox.Width; 
                if (Envelope.Height < bbox.Height)
                    _Zoom *= bbox.Height / Envelope.Height;
                Center = bbox.GetCentroid();
            }
        }
    }
}
