using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap.Data
{
    /// <summary>
    /// shp文件读取类
    /// </summary>
    public class ShapeFile
    {
        private int _FileSize;
        private string _Filename;
        //.shp文件的文件流
        private FileStream fsShapeFile;
        //.shp文件的字节流
        private BinaryReader brShapeFile;
        //外包矩形
        private BoundingBox _Envelope;
        //几何类型
        private ShapeTypeEnum _ShapeType;
        //每个几何对象再文件流中的位置
        private int[] _OffsetOfRecord;
        //数据条数
        private int _FeatureCount;
        //文件路径
        public string Filename
        {
            get { return _Filename; }
            set
            {
                _Filename = value;
            }
        }
        /// <summary>
        /// 传入shp文件初始化类
        /// </summary>
        /// <param name="filename"></param>
        public ShapeFile(string filename)
        {

        }
        /// <summary>
        /// 读取和解析.shp文件的文件头
        /// </summary>
        private void ParseHeader (){
            fsShapeFile = new FileStream(_Filename, FileMode.Open, FileAccess.Read);
            brShapeFile = new BinaryReader(fsShapeFile, Encoding.Unicode);
            brShapeFile.BaseStream.Seek(0, 0);
            //读取四个字节，检查文件头
            if (brShapeFile.ReadInt32() != 170328064)
            {
                //文件真实的编码是9994，
                //170328064的16进制为0x0a27，交换字节顺序后就是0x270a，十进制就是9994了
                throw (new ApplicationException("无效的Shapefile文件 (.shp)"));
            }
            //六个没有被使用的int32整数
            brShapeFile.BaseStream.Seek(24, 0);
            //获取文件长度，包括文件头
            _FileSize = 2 * SwapByteOrder(brShapeFile.ReadInt32());
            //读取几何类型
            _ShapeType = (ShapeTypeEnum)brShapeFile.ReadInt32();
            //读取数据的外包矩形
            brShapeFile.BaseStream.Seek(36, 0);
            _Envelope = new BoundingBox(brShapeFile.ReadDouble(), brShapeFile.ReadDouble(), brShapeFile.ReadDouble(),
                            brShapeFile.ReadDouble());

            //通过.shp文件获取数据条数
            // 跳过文件头读取
            brShapeFile.BaseStream.Seek(100, 0);
            // 几何数据记录开始位置
            long offset = 100; 

            //遍历数据建立功能包含在数据文件的数量
            while (offset < _FileSize)
            {
                ++_FeatureCount;

                brShapeFile.BaseStream.Seek(offset + 4, 0); //跳过长度
                int data_length = 2 * SwapByteOrder(brShapeFile.ReadInt32());

                if ((offset + data_length) > _FileSize)
                {
                    --_FeatureCount;
                }

                offset += data_length; // 添加记录数据长度
                offset += 8; //  +添加每条数据记录头的大小
            }
            brShapeFile.Close();
            fsShapeFile.Close();
        }
        /// <summary>
        /// 生成矢量文件索引
        /// </summary>
        private void PopulateIndexes() {
            //记录当前位置的指针
            long old_position = brShapeFile.BaseStream.Position;
            //跳过文件头
            brShapeFile.BaseStream.Seek(100, 0);
            //矢量文件记录开始位置
            long offset = 100;
            for (int x = 0; x < _FeatureCount; ++x)
            {
                _OffsetOfRecord[x] = (int)offset;

                brShapeFile.BaseStream.Seek(offset + 4, 0); //跳过的长度
                int data_length = 2 * SwapByteOrder(brShapeFile.ReadInt32());
                offset += data_length; // 添加记录数据长度
                offset += 8; //   +添加每条数据记录头的大小
            }

            // 返回指针的原始位置
            brShapeFile.BaseStream.Seek(old_position, 0);

        }
        /// <summary>
        /// 从.shp文件中读取并解析几何对象
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        private Geometry ReadGeometry(int oid) {
            brShapeFile.BaseStream.Seek(_OffsetOfRecord[oid] + 8, 0);
            ShapeTypeEnum type = (ShapeTypeEnum)brShapeFile.ReadInt32(); //Shape type
            if (type== ShapeTypeEnum.Null)
            {
                return null;
            }
            if (type==ShapeTypeEnum.Point)
            {
                return new Point(brShapeFile.ReadDouble(), brShapeFile.ReadDouble());
            }
            else
            {
                throw (new ApplicationException("Shapefile 文件类型 " + _ShapeType.ToString() + " 不支持"));
            }

        }

        ///<summary>
        ///互换int32的字节顺序
        ///</summary>
        /// <param name="i">int</param>
        /// <returns>Byte Order swapped int32</returns>
        private int SwapByteOrder(int i)
        {
            byte[] buffer = BitConverter.GetBytes(i);
            Array.Reverse(buffer, 0, buffer.Length);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
