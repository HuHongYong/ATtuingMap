using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    public  class Transform
    {
        /// <summary>
        /// 空间坐标转屏幕坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static PointF WorldtoMap(Point p, Map map)
        {
            PointF result = new System.Drawing.Point();

            double height = (map.Zoom * map.Size.Height) / map.Size.Width;
            double left = map.Center.X - map.Zoom * 0.5;
            double top = map.Center.Y + height * 0.5 * map.PixelAspectRatio;
            result.X = (float)((p.X - left) / map.PixelWidth);
            result.Y = (float)((top - p.Y) / map.PixelHeight);
            return result;
        }
        /// <summary>
        /// 屏幕坐标转空间坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Point MapToWorld(PointF p, Map map)
        {
            Point ul = new Point(map.Center.X - map.Zoom * .5, map.Center.Y + map.MapHeight * .5);
            return new Point(ul.X + p.X * map.PixelWidth,
                             ul.Y - p.Y * map.PixelHeight);
        }
    }
}
