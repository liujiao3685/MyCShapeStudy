using MVCProject.Enum;
using MVCProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Core
{
    public class AutoMobileControl : IVehicleControl
    {
        private IVehicleView m_view;

        private IVehicleModule m_module;

        public AutoMobileControl()
        {

        }

        public AutoMobileControl(IVehicleView view, IVehicleModule module)
        {
            m_view = view;
            m_module = module;
        }

        public void Accelerate(int paramAmount)
        {
            
        }

        public void Decelerate(int paramAmount)
        {
        }

        public void AddObserver(IVehicleView view)
        {

        }

        public void NotifyObservers()
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IVehicleView view)
        {
            throw new NotImplementedException();
        }

        public void RequestAccelerate(int value)
        {
            m_module?.Accelerate(value);
            if (m_view != null) SetView();
        }

        public void RequestDecelerate(int value)
        {
            m_module?.Decelerate(value);
            if (m_view != null) SetView();
        }

        public void RequestTurn(RelativeDirection direction)
        {
            m_module?.Turn(direction);
            if (m_view != null) SetView();

        }

        public void SetModule(IVehicleModule module)
        {
            m_module = module;
        }

        public void SetView(IVehicleView view)
        {
            m_view = view;
        }

        public void Turn(RelativeDirection direction)
        {
            throw new NotImplementedException();
        }

        public void SetView()
        {
            if (m_module.Speed >= m_module.MaxSpeed)
            {
                m_view.DisableAcceleration();
                m_view.DisableDeceleration();
            }
            else if (m_module.Speed <= m_module.MaxReverseSpeed)
            {
                m_view.DisableDeceleration();
                m_view.DisableAcceleration();
            }
            else
            {
                m_view.EnableAcceleration();
                m_view.EnableDeceleration();
            }

            if (m_module.Speed >= m_module.MaxTurnSpeed)
            {
                m_view.DisableTurning();
            }
            else
            {
                m_view.EnableTurning();
            }
        }

    }
}
