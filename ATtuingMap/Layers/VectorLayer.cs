using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    /// <summary>
    /// 矢量图层
    /// </summary>
    public class VectorLayer
    {
        private VectorStyle _style;
        public  VectorStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }
        public VectorLayer() {
            _style = new VectorStyle();
        }
        /// <summary>
        /// 渲染几何图形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="map"></param>
        /// <param name="feature"></param>
        protected void RenderGeometry(Graphics g, Map map, Geometry feature)
        {
            GeometryTypeEnum geometryType = feature.GeometryType;
            switch (geometryType)
            {
                case GeometryTypeEnum.Point:
                    VectorRenderer.DrawPoint(g, (Point)feature, _style.PointColor, _style.PointSize, map);
                    break;
                default:

                    break;
            }
        }
    }
}
