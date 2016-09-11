using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AnimalGroupBehavior
{
    /// <summary>
    /// 
    /// Util  -  工具类
    /// 
    /// </summary>
    public class Util
    {
        private static string guidRegex = @"^[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}$";

        private static string datetimeRegex = @"^\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}$";

        /// <summary>
        /// 验证GUID合法性
        /// </summary>
        /// <returns>正确：返回true， 否则返回false</returns>
        public static bool TryVerifyGUID(string guid)
        {
            Regex regex = new Regex(guidRegex);

            return regex.IsMatch(guid);
        }

        /// <summary>
        /// 验证日期格式字符串是否正确
        /// </summary>
        /// <param name="dtstr"> 日期格式字符串 </param>
        /// <param name="datatime"> 返回转换的日期格式 </param>
        /// <returns>正确：返回true， 否则返回false,且日期被设置为最小值 </returns>
        public static bool TryVerifyDateTime(string dtstr, out DateTime datatime )
        {
            Regex regex = new Regex(datetimeRegex);

            if (regex.IsMatch(dtstr) && DateTime.TryParse(dtstr, out datatime))
                return true;

            datatime = DateTime.MinValue;
            return false;
        }

        /// <summary>
        /// 验证动物坐标数据合法性
        /// </summary>
        /// <param name="costr"> 坐标数据 </param>
        /// <param name="coordinate"> 合法的坐标对象 </param>
        /// <returns> 正确：返回true， 否则返回false </returns>
        public static bool TryVerifyCoordinate(string costr, ref AnimalCoordinate coordinate)
        {
            string[] coordInfo = costr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int len = coordInfo.Length;
            if (3 <= len)
            {
                coordinate.animalId = coordInfo[0];

                int x = 0;
                if (Int32.TryParse(coordInfo[1], out x))
                    coordinate.xCoordinate = x;
                else
                    return false;

                int y = 0;
                if (Int32.TryParse(coordInfo[2], out y))
                    coordinate.yCoordinate = y;
                else
                    return false;

                if (3 == len)
                    return true;
                else if (5 == len)
                {
                    int xOffset = 0;
                    if (Int32.TryParse(coordInfo[3], out xOffset))
                        coordinate.xOffset = xOffset;
                    else
                        return false;

                    int yOffset = 0;
                    if (Int32.TryParse(coordInfo[4], out yOffset))
                        coordinate.yOffset = yOffset;
                    else
                        return false;

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

      
    }
}
