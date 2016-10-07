using System;
using Serilog.Core;
using Serilog.Events;

namespace grundlage.serilog
{
    public class PrimitiveSerilogSupport : IDestructuringPolicy
    {
        public bool TryDestructure(Object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            var vs = value as ValueType<string>;
            if (vs != null)
            {
                result = Destructure(vs, propertyValueFactory);
                return true;
            }

            var vi = value as ValueType<int>;
            if (vi != null)
            {
                result = Destructure(vi, propertyValueFactory);
                return true;
            }

            var vd = value as ValueType<decimal>;
            if (vd != null)
            {
                result = Destructure(vd, propertyValueFactory);
                return true;
            }

            var vdd = value as ValueType<double>;
            if (vdd != null)
            {
                result = Destructure(vdd, propertyValueFactory);
                return true;
            }

            var vdt = value as ValueType<DateTime>;
            if (vdt != null)
            {
                result = Destructure(vdt, propertyValueFactory);
                return true;
            }

            var vts = value as ValueType<TimeSpan>;
            if (vts != null)
            {
                result = Destructure(vts, propertyValueFactory);
                return true;
            }

            var vc = value as ValueType<char>;
            if (vc != null)
            {
                result = Destructure(vc, propertyValueFactory);
                return true;
            }

            result = null;
            return false;
        }

        LogEventPropertyValue Destructure(ValueType<string> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<char> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<int> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<decimal> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<double> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<DateTime> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
        LogEventPropertyValue Destructure(ValueType<TimeSpan> value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValueFactory.CreatePropertyValue(value.Unwrap(), true);
        }
    }
}
