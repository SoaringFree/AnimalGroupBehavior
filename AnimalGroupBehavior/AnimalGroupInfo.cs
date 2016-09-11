using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalGroupBehavior
{
    /// <summary>
    /// AnimalGroupInfo  -  存储动物群体信息
    /// </summary>
    public class AnimalGroupInfo : IDisposable
    {
        #region 属性

        /// <summary>
        /// 动物群体状态数据
        /// </summary>
        private List<AnimalStatus> AnimalStatusList;

        /// <summary>
        /// 动物群体最新时刻快照
        /// </summary>
        private AnimalStatus AnimalSnapshoot;

        #endregion


        #region 构造器

        public AnimalGroupInfo()
        {
            AnimalStatusList = new List<AnimalStatus>();
            AnimalSnapshoot = new AnimalStatus();
        }

        #endregion


        #region 回收器

        void IDisposable.Dispose()
        {
            AnimalStatusList = null;
            AnimalSnapshoot = null;
            //throw new NotImplementedException();
        }

        #endregion



        /// <summary>
        /// 获取动物群体 id 时刻 群体快照
        /// </summary>
        /// <param name="historyData"> 动物群体历史数据 </param>
        /// <param name="id"> 当前时刻id </param>
        /// <returns> 群体快照 </returns>
        public string GetSnapShot(string historyData, string id)
        {
            string[] data = historyData.Split(new string[] { "\n" }, 
                StringSplitOptions.None);

            AnimalStatus resultSnapshoot = new AnimalStatus();

            int len = data.Length;
            for (int i = 0; i < len; )
            {
                AnimalStatus status = new AnimalStatus();
                DateTime currentTime = new DateTime();

                if (i < len && data[i].Equals(""))
                    i++;
                if (i == len)
                    break;

                // 读取GUID
                if (i < len && Util.TryVerifyGUID(data[i]))
                    status.currentGUID = data[i++];
                else
                    return "Invalid format.";

                // 读取日期 
                if (i < len && Util.TryVerifyDateTime(data[i++], out currentTime))
                    status.currentTime = currentTime;
                else
                    return "Invalid format.";

                // 读取动物坐标信息
                while (i < len && !data[i].Equals(""))
                {
                    AnimalCoordinate coordinate = new AnimalCoordinate();
                    if (Util.TryVerifyCoordinate(data[i++], ref coordinate))
                        status.AnimalCoordinateList.Add(coordinate);
                    else
                        return "Invalid format.";
                }

                string errorInfo = "";
                if (UpdateSnapshoot(status, out errorInfo))
                {
                    AnimalStatusList.Add(status);
                    if (status.currentGUID.Equals(id))
                        resultSnapshoot = AnimalSnapshoot;
                }
                else
                    return errorInfo;
            }

            return PrintSnapshoot(resultSnapshoot);
        }
        

        /// <summary>
        /// 根据输入全局ID，获取该时刻的动物群体快照
        /// </summary>
        /// <param name="guid"> 全局ID </param>
        /// <returns> 该时刻群体快照 </returns>
        public string GetSnapShotByGUID(string guid)
        {
            if (Util.TryVerifyGUID(guid))
            {
                AnimalStatus status = new AnimalStatus();

                Dictionary<string, AnimalCoordinate> snapshot = new Dictionary<string, AnimalCoordinate>();

                int len = AnimalStatusList.Count;
                int i = 0;
                for (; i < len; i++)
                    if (AnimalStatusList[i].currentGUID == guid)
                        break;

                if (i < len)
                {
                    status.currentGUID = AnimalStatusList[i].currentGUID;
                    status.currentTime = AnimalStatusList[i].currentTime;
                    for (; i >=0; i--)
                    {
                        foreach (AnimalCoordinate cdn in AnimalStatusList[i].AnimalCoordinateList)
                        {
                            if (!snapshot.ContainsKey(cdn.animalId))
                            {
                                AnimalCoordinate coordinate = new AnimalCoordinate(cdn.animalId, 
                                    cdn.xCoordinate + cdn.xOffset, cdn.yCoordinate + cdn.yOffset);
                                snapshot.Add(cdn.animalId, coordinate);
                            }
                        }
                    }

                    StringBuilder result = new StringBuilder();

                    var sortDic = from objDic in snapshot orderby objDic.Key select objDic.Value;
                    foreach (var item in sortDic)
                    {
                        result.Append(String.Format("{0} {1} {2}\n", item.animalId, item.xCoordinate, item.yCoordinate));
                    }

                    return result.ToString() ;
                }
                else
                    return "Invalid Input.";
            }
            else
                return "Invalid Input.";
        }


        /// <summary>
        /// 更新某一时刻动物群体快照
        /// </summary>
        /// <param name="status"> 某一时刻动物群体信息 </param>
        /// <param name="errorInfo"> 错误信息 </param>
        /// <returns></returns>
        private bool UpdateSnapshoot(AnimalStatus status, out string errorInfo)
        {
            errorInfo = null;
            foreach (AnimalCoordinate codi in status.AnimalCoordinateList)
            {
                AnimalCoordinate temp = new AnimalCoordinate(codi.animalId,
                    codi.xCoordinate, codi.yCoordinate, codi.xOffset, codi.yOffset);

                int snapLen = AnimalSnapshoot.AnimalCoordinateList.Count();
                bool isInSnap = false;
                int i = 0;
                for (; i < snapLen; i++ )
                {
                    if (temp.animalId == AnimalSnapshoot.AnimalCoordinateList[i].animalId)
                    {
                        isInSnap = true;
                        break;
                    }
                }

                if (true == isInSnap)
                {
                    if (temp.xCoordinate == AnimalSnapshoot.AnimalCoordinateList[i].xCoordinate 
                        && temp.yCoordinate == AnimalSnapshoot.AnimalCoordinateList[i].yCoordinate)
                    {
                        AnimalSnapshoot.AnimalCoordinateList[i].xCoordinate = temp.xCoordinate + temp.xOffset;
                        AnimalSnapshoot.AnimalCoordinateList[i].yCoordinate = temp.yCoordinate + temp.yOffset;
                    }
                    else
                    {
                        errorInfo = "Conflict found at " + status.currentGUID;
                        return false;
                    }
                }
                else
                {
                    temp.xCoordinate = temp.xCoordinate + temp.xOffset;
                    temp.yCoordinate = temp.yCoordinate + temp.yOffset;
                    temp.xOffset = 0;
                    temp.yOffset = 0;
                    AnimalSnapshoot.AnimalCoordinateList.Add(temp);
                }
            }
            AnimalSnapshoot.currentGUID = status.currentGUID;
            AnimalSnapshoot.currentTime = status.currentTime;

            return true;
        }


        /// <summary>
        /// 打印动物群体快照信息
        /// </summary>
        /// <param name="snapshoot"> 动物群体快照信息 </param>
        /// <returns></returns>
        private string PrintSnapshoot(AnimalStatus snapshoot)
        {
            StringBuilder result = new StringBuilder();

            IEnumerable<AnimalCoordinate> query = null; 
            query = from item in snapshoot.AnimalCoordinateList orderby item.animalId select item;
            
            foreach (var item in query)
                result.Append(String.Format("{0} {1} {2}\n", item.animalId, item.xCoordinate, item.yCoordinate));

            string res = result.ToString();
            return result.ToString();
        }

        
    }
}
