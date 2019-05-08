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
        //鼠标按住不放的坐标点
        private System.Drawing.Point _mousedrag;
        //map核心类
        private Map myMap;
        //是否在进行拖放
        private bool _mousedragging;
        //按下鼠标当前地图的拷贝
        private Image _mousedragImg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            myMap = new Map(pictureBox1.Size);
            ///目前第一版只支持点
            ShapeFile shapeFile = new ShapeFile(@"GeoData\地级城市驻地.shp");
            VectorLayer layer = new VectorLayer(shapeFile);
            ShapeFile shapeFile1 = new ShapeFile(@"GeoData\省会城市.shp");
            VectorLayer layer1 = new VectorLayer(shapeFile1);
            ShapeFile shapeFile2 = new ShapeFile(@"GeoData\县城驻地.shp");
            VectorLayer layer2 = new VectorLayer(shapeFile2);
            myMap.Layers.Add(layer2);
            myMap.Layers.Add(layer);
            myMap.Layers.Add(layer1);
            myMap.ZoomToExtents();
            pictureBox1.Image = myMap.GetMap();
            //鼠标样式
            pictureBox1.Cursor = Cursors.Hand;
            this.pictureBox1.MouseWheel += new MouseEventHandler(MapImage_Wheel);
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var point = Transform.MapToWorld(new PointF(e.X, e.Y), myMap);
            label1.Text = $"坐标X:{point.X}  Y:{point.Y}";
            //拖动已有图像
            if (_mousedragging)
            {
                Bitmap _dragImg1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(_dragImg1);
                g.Clear(Color.Transparent);
                //图片裁剪
                g.DrawImageUnscaled(_mousedragImg,
                                    new System.Drawing.Point(e.Location.X - _mousedrag.X,
                                                             e.Location.Y - _mousedrag.Y));
                g.Dispose();
                pictureBox1.Image = _dragImg1;
            }
        }
        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //地图的新中心
            System.Drawing.Point pnt = new System.Drawing.Point(pictureBox1.Width / 2 + (_mousedrag.X - e.Location.X),
                                                   pictureBox1.Height / 2 + (_mousedrag.Y - e.Location.Y));
            //修改鼠标拖动后的地图中心空间坐标点
            myMap.Center = Transform.MapToWorld(pnt, myMap);
            pictureBox1.Image = myMap.GetMap();
            //取消鼠标拖动
            _mousedragging = false;
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标按下的屏幕坐标位置
            _mousedrag = e.Location;
            //鼠标按下，为了区分普通鼠标移动和鼠标按下移动
            _mousedragging = true;
            //当前地图 图片
            _mousedragImg = pictureBox1.Image;
        }
        /// <summary>
        /// 鼠标滚轮触发缩放地图事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapImage_Wheel(object sender, MouseEventArgs e)
        {
            //重新定位中心点
            myMap.Center = Transform.MapToWorld(new System.Drawing.Point(e.X, e.Y), myMap);
            //pictureBox1.Image = myMap.GetMap();
            //e.Delta常数，鼠标滚轮滚一下
            double scale = (e.Delta / 120.0);
            //缩放1.2倍
            double scaleBase = 1 + (2.0 / 10);
            //重新设置zoom缩放等级
            myMap.Zoom *= Math.Pow(scaleBase, scale);
            //pictureBox1.Image = myMap.GetMap();
            int NewCenterX = (pictureBox1.Width / 2) + ((pictureBox1.Width / 2) - e.X);
            int NewCenterY = (pictureBox1.Height / 2) + ((pictureBox1.Height / 2) - e.Y);
            //修改鼠标缩放后的地图中心点
            myMap.Center = Transform.MapToWorld(new System.Drawing.Point(NewCenterX, NewCenterY), myMap);
            pictureBox1.Image = myMap.GetMap();
        }
        /// <summary>
        /// 最大化，正常切换触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                myMap.Size = pictureBox1.Size;
                pictureBox1.Image = myMap.GetMap();
            }
        }
    }
}
