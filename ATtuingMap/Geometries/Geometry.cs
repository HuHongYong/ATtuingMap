using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATtuingMap
{
    /// <summary>
    /// 抽象几何类
    /// </summary>
   public abstract  class Geometry
    {
        public virtual GeometryTypeEnum GeometryType
        {
            get { return GeometryTypeEnum.Geometry; }
        }
    }
}
