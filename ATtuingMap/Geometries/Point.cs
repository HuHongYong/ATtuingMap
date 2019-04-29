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
    }
}
