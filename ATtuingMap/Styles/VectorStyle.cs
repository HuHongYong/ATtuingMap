using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

            PointColor = Brushes.Red;
            PointSize = 3f;
        }
    }
}
