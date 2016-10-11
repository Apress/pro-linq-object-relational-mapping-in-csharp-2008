using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


namespace BoP.Util
{
    public class DTOHelper<T>
    {
        public T Clone(T entity)
        {
            PropertyInfo[] pis = entity.GetType().GetProperties();
            
            object o = Activator.CreateInstance(typeof(T));
            
            foreach (PropertyInfo pi in pis)
            {
                if(o.GetType().GetProperty(pi.Name).CanWrite)
                    o.GetType().GetProperty(pi.Name).SetValue(o, pi.GetValue((object)entity, null),null);

            }
            return (T)o;
            
        }

    }
}
