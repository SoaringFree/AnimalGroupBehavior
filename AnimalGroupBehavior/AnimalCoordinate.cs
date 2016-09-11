using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalGroupBehavior
{
    /// <summary>
    /// 
    /// AnimalCoordinate  -  存储动物坐标及坐标变化状态
    /// 
    /// </summary>
    public class AnimalCoordinate
    {
        /// <summary>
        /// 动物ID
        /// </summary>
        public string animalId { get; set; }

        /// <summary>
        /// 1. 动物上一时刻x坐标
        /// 2. 当 x/yTransform 同时为 0 时，表示动物第一次出现，且x坐标为 xCoordinate
        /// </summary>
        public int xCoordinate { get; set; }

        /// <summary>
        /// 1. 动物上一时刻y坐标
        /// 2. 当 x/yTransform 同时为 0 时，表示动物第一次出现，且y坐标为 yCoordinate
        /// </summary>
        public int yCoordinate { get; set; }

        /// <summary>
        /// 当前时刻动物x坐标与上一时刻的变化量
        /// </summary>
        public int xOffset { get; set; }

        /// <summary>
        /// 当前时刻动物y坐标与上一时刻的变化量
        /// </summary>
        public int yOffset { get; set; }


        public AnimalCoordinate() { }

        public AnimalCoordinate(string _animalId, int _x, int _y, int _xOffset = 0, int _yOffset = 0)
        {
            this.animalId = _animalId;
            this.xCoordinate = _x;
            this.yCoordinate = _y;
            this.xOffset = _xOffset;
            this.yOffset = _yOffset;
        }

        /// <summary>
        /// 判断本实例 是否与 参考实例 的值相等
        /// </summary>
        /// <param name="other"> 参考实例 </param>
        /// <returns></returns>
        public bool ValueEquals(AnimalCoordinate other)
        {
            return (this.animalId.Equals(other.animalId)
                && this.xCoordinate.Equals(other.xCoordinate)
                && this.yCoordinate.Equals(other.yCoordinate)
                && this.xOffset.Equals(other.xOffset)
                && this.yOffset.Equals(other.yOffset));
        }
    }
}
