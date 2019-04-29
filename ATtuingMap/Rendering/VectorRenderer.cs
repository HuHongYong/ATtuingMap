using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    /// <summary>
    /// 矢量图层渲染静态类
    /// </summary>
   public static class VectorRenderer
    {
        /// <summary>
        /// 在地图上绘制点
        /// </summary>
        public static void DrawPoint(Graphics g, Point point, Brush b, float size,  Map map) {
            if (point == null)
                return;
            PointF pp = Transform.WorldtoMap(point, map);
            Matrix startingTransform = g.Transform;
            float width = size;
            float height = size;
            g.FillEllipse(b, (int)pp.X - width / 2,
                        (int)pp.Y - height / 2 , width, height);
        }
    }
}
