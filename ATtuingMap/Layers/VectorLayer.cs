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
        private ShapeFile _dataSource;
        public  VectorStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }
        /// <summary>
        /// 初始化化图层传入数据源
        /// </summary>
        /// <param name="dataSource"></param>
        public VectorLayer(ShapeFile dataSource) {
            _dataSource = dataSource;
            _style = new VectorStyle();
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public ShapeFile DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
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
        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="g"></param>
        /// <param name="map"></param>
        public void Render(Graphics g, Map map) {
            foreach (var geo in DataSource.GetAllGeometry())
            {
                RenderGeometry(g,map, geo);
            }
        }
    }
}
