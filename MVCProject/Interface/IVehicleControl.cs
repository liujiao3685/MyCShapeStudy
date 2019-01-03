using MVCProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Interface
{
    /// <summary>
    /// 处理控制器接口-车
    /// </summary>
    public interface IVehicleControl
    {
        #region 观察者模式

        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="view"></param>
        void AddObserver(IVehicleView view);

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="view"></param>
        void RemoveObserver(IVehicleView view);

        /// <summary>
        /// 通知观察者
        /// </summary>
        void NotifyObservers();


        #endregion


        #region 交互

        /// <summary>
        /// 请求加速
        /// </summary>
        /// <param name="value"></param>
        void RequestAccelerate(int value);

        /// <summary>
        /// 请求减速
        /// </summary>
        /// <param name="value"></param>
        void RequestDecelerate(int value);

        /// <summary>
        /// 请求转向
        /// </summary>
        /// <param name="direction"></param>
        void RequestTurn(RelativeDirection direction);

        /// <summary>
        /// 设置视图
        /// </summary>
        /// <param name="view"></param>
        void SetView(IVehicleView view);

        /// <summary>
        /// 设置模型
        /// </summary>
        /// <param name="module"></param>
        void SetModule(IVehicleModule module);

        #endregion


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

        /// <summary>
        /// 转向
        /// </summary>
        /// <param name="direction"></param>
        void Turn(RelativeDirection direction);

    }
}
