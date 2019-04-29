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
    }
}
