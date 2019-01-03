using MVCProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Interface
{
    /// <summary>
    /// 处理模型接口
    /// </summary>
    public interface IVehicleModule
    {
        /// <summary>
        /// 车名
        /// </summary>
        string Name { set; get; }

        /// <summary>
        /// 当前速度
        /// </summary>
        int Speed { set; get; }

        /// <summary>
        /// 最大前进速度
        /// </summary>
        int MaxSpeed { set; get; }

        /// <summary>
        /// 最大转弯速度
        /// </summary>
        int MaxTurnSpeed { set; get; }

        /// <summary>
        /// 最大后退速度
        /// </summary>
        int MaxReverseSpeed { set; get; }

        /// <summary>
        /// 当前方向
        /// </summary>
        AbsoluteDirection Direction { set; get; }

        /// <summary>
        /// 转向
        /// </summary>
        /// <param name="direction"></param>
        void Turn(RelativeDirection direction);

        /// <summary>
        /// 加速
        /// </summary>
        /// <param name="paramAmount"></param>
        void Accelerate(int paramAmount);

        /// <summary>
        /// 减速
        /// </summary>
        /// <param name="paramAmount"></param>
        void Decelerate(int paramAmount);


    }
}
