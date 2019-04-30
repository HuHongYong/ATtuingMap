using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    /// <summary>
    /// 双精度类型的外包矩形
    /// </summary>
    public class BoundingBox
    {
        //右上角
        private Point _Max;
        //左下角
        private Point _Min;
        /// <summary>
        /// 右上角
        /// </summary>
        public Point Max
        {
            get { return _Max; }
            set { _Max = value; }
        }
        /// <summary>
        /// 左下角
        /// </summary>
        public Point Min
        {
            get { return _Min; }
            set { _Min = value; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get { return Math.Abs(_Max.Y - _Min.Y); }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get { return Math.Abs(_Max.X - _Min.X); }
        }
        /// <summary>
        /// 初始化外包矩形框
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public BoundingBox(double minX, double minY, double maxX, double maxY)
        {
            _Min = new Point(minX, minY);
            _Max = new Point(maxX, maxY);
        }
        /// <summary>
        /// 初始化外包矩形框
        /// </summary>
        /// <param name="lowerLeft">左下角</param>
        /// <param name="upperRight">右上角</param>
        public BoundingBox(Point lowerLeft, Point upperRight)
            : this(lowerLeft.X, lowerLeft.Y, upperRight.X, upperRight.Y)
        {
        }
        /// <summary>
        /// 计算比较算出最新的外包矩形
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public BoundingBox Join(BoundingBox box)
        {

                return new BoundingBox(Math.Min(Min.X, box.Min.X), Math.Min(Min.Y, box.Min.Y),
                                       Math.Max(Max.X, box.Max.X), Math.Max(Max.Y, box.Max.Y));
        }
        /// <summary>
        /// 获取中心点
        /// </summary>
        /// <returns></returns>
        public Point GetCentroid()
        {
            return (_Min + _Max) * .5f;
        }
    }
}
