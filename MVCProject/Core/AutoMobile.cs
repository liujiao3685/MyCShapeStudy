using MVCProject.Enum;
using MVCProject.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Core
{
    /// <summary>
    /// 框架-车
    /// </summary>
    public abstract class AutoMobile : IVehicleModule
    {
        #region Declarations 

        private string m_name = string.Empty;
        private ArrayList m_arrayList = new ArrayList();
        private int m_speed = 0;
        private int m_maxSpeed = 0;
        private int m_maxTurnSpeed = 0;
        private int m_maxReverseSpeed = 0;
        private AbsoluteDirection m_direction = AbsoluteDirection.North;

        #endregion

        public AutoMobile(string name, int maxSpeed, int maxTurnSpeed, int maxReverseSpeed)
        {
            m_name = name;
            m_maxSpeed = maxSpeed;
            m_maxTurnSpeed = maxTurnSpeed;
            m_maxReverseSpeed = maxReverseSpeed;
        }

        public void AddObserver(IVehicleView view)
        {
            m_arrayList.Add(view);
        }

        public void RemoveObserver(IVehicleView view)
        {
            if (m_arrayList.Contains(view)) m_arrayList.Remove(view);
        }

        public void NotifyObservers()
        {
            foreach (IVehicleView item in m_arrayList)
            {
                item.Update(this);
            }
        }

        public string Name { get { return m_name; } set { m_name = value; } }

        public int Speed { get { return m_speed; } set { m_speed = value; } }

        public int MaxSpeed { get { return m_maxSpeed; } set { m_maxSpeed = value; } }

        public int MaxTurnSpeed { get { return m_maxTurnSpeed; } set { m_maxTurnSpeed = value; } }

        public int MaxReverseSpeed { get { return m_maxReverseSpeed; } set { m_maxReverseSpeed = value; } }

        public AbsoluteDirection Direction { get { return m_direction; } set { m_direction = value; } }

        public void Accelerate(int paramAmount)
        {
            m_speed += paramAmount;
            if (m_speed >= m_maxSpeed) m_speed = m_maxSpeed;
            NotifyObservers();
        }

        public void Decelerate(int paramAmount)
        {
            m_speed -= paramAmount;
            if (m_speed <= m_maxReverseSpeed) m_speed = m_maxReverseSpeed;
            NotifyObservers();
        }

        public void Turn(RelativeDirection direction)
        {
            AbsoluteDirection newDirection;
            switch (direction)
            {
                case RelativeDirection.Right:
                    newDirection = (AbsoluteDirection)((int)(m_direction + 1) % 4);
                    break;
                case RelativeDirection.Left:
                    newDirection = (AbsoluteDirection)((int)(m_direction + 3) % 4);
                    break;
                case RelativeDirection.Back:
                    newDirection = (AbsoluteDirection)((int)(m_direction + 2) % 4);
                    break;
                default:
                    newDirection = AbsoluteDirection.North;
                    break;
            }
            m_direction = newDirection;
            NotifyObservers();
        }
    }
}
