using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Interface
{
    /// <summary>
    /// 视图接口-车
    /// </summary>
    public interface IVehicleView
    {
        #region 观察者

        /// <summary>
        /// 模型发生变化
        /// </summary>
        /// <param name="module"></param>
        void Update(IVehicleModule module);

        #endregion

        /// <summary>
        /// 禁止加速
        /// </summary>
        void DisableAcceleration();

        /// <summary>
        /// 运行加速
        /// </summary>
        void EnableAcceleration();

        /// <summary>
        /// 禁止减速
        /// </summary>
        void DisableDeceleration();

        /// <summary>
        /// 允许加速
        /// </summary>
        void EnableDeceleration();

        /// <summary>
        /// 禁止转向
        /// </summary>
        void DisableTurning();

        /// <summary>
        /// 允许转向
        /// </summary>
        void EnableTurning();


    }
}
