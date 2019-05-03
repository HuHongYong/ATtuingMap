using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATtuingMap
{
   public class VectorStyle
    {
        private float _PointSize;

        private Brush _PointBrush = null;
        /// <summary>
        /// 点的颜色
        /// </summary>
        public Brush PointColor
        {
            get { return _PointBrush; }
            set { _PointBrush = value; }
        }
        /// <summary>
        /// 点的大小
        /// </summary>
        public float PointSize
        {
            get { return _PointSize; }
            set { _PointSize = value; }
        }
        public VectorStyle()
        {

            PointColor = RandomBrushes();
            PointSize = 5f;
        }
        /// <summary>
        /// 随机生成画笔颜色
        /// </summary>
        /// <returns></returns>
        private Brush RandomBrushes() {
            Thread.Sleep(20);
            Random r = new Random((int)DateTime.Now.Ticks);
            int red = r.Next(0, byte.MaxValue + 1);
            int green = r.Next(0, byte.MaxValue + 1);
            int blue = r.Next(0, byte.MaxValue + 1);
            System.Drawing.Brush brush = new System.Drawing.SolidBrush(Color.FromArgb(red, green, blue));
            return brush;
        }
    }
}
