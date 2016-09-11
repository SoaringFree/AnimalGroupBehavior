using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalGroupBehavior
{
    /// <summary>
    /// 
    /// AnimalStatus  -  存储某一时刻动物坐标变化的相关信息
    /// 
    /// </summary>
    class AnimalStatus
    {
        /// <summary>
        /// 当前时刻全局唯一ID
        /// </summary>
        public string currentGUID { get; set; }

        /// <summary>
        /// 当前时刻具体时间
        /// </summary>
        public DateTime currentTime { get; set; }

        /// <summary>
        /// 当前时刻动物状态
        /// </summary>
        public List<AnimalCoordinate> AnimalCoordinateList { get; set; }


        public AnimalStatus()
        {
            AnimalCoordinateList = new List<AnimalCoordinate>();
        }

    }
}
