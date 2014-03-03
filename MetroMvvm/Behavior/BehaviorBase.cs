using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroMvvm.Behavior
{
    public abstract class BehaviorBase
    {
        protected object AssociatedObject;

        public void Attach(object obj)
        {
            if (obj != AssociatedObject)
            {
                AssociatedObject = obj;
                OnAttached();
            }
        }

        protected virtual void OnAttached()
        {

        }
    }
    public abstract class Behavior<T> : BehaviorBase
    {
        protected T AssociatedObject
        {
            get { return (T)base.AssociatedObject; }
        }
    }
}
