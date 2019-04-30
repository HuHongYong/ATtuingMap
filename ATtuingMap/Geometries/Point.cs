using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    public class Point: Geometry
    {
        private double _X;
        private double _Y;
        public double Y
        {
            get
            {
                    return _Y;
            }
            set
            {
                _Y = value;
            }
        }
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }
        public override GeometryTypeEnum GeometryType
        {
            get
            {
                return GeometryTypeEnum.Point;
            }
        }
        public Point(double x, double y)
        {
            _X = x;
            _Y = y;
        }
        /// <summary>
        /// 重写点的加法
        /// </summary>
        /// <param name="v1">点1</param>
        /// <param name="v2">点2</param>
        /// <returns></returns>
        public static Point operator +(Point v1, Point v2)
        {
            return new Point(v1.X + v2.X, v1.Y + v2.Y);
        }
        /// <summary>
        /// 重写点的乘法法 点*缩放
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Point operator *(Point m, double d)
        {
            return new Point(m.X * d, m.Y * d);
        }
    }
}
