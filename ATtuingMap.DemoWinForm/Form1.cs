using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = ATtuingMap.Point;

namespace ATtuingMap.DemoWinForm
{
    public partial class Form1 : Form
    {
        private Map myMap;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             myMap = new Map(pictureBox1.Size);
            ShapeFile shapeFile = new ShapeFile(@"G:\BaiduNetdiskDownload\GIS之家资源分享\OSM中国区矢量数据-20171218.shp\gis.osm_pois_free_1.shp");
            VectorLayer layer = new VectorLayer(shapeFile);
            myMap.Layers.Add(layer);
            myMap.ZoomToExtents();
            pictureBox1.Image= myMap.GetMap();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           var point= Transform.MapToWorld(new PointF(e.X,e.Y),myMap);
            label1.Text = $"坐标X:{point.X}  Y:{point.Y}";
        }
    }
}
