using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace grundlage.webapi
{
    public abstract class BasePrimitiveWebApiModelBinder<TMicroType, TPrimitive> : IModelBinder
        where TMicroType : ValueType<TPrimitive>
        where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive>, IComparable
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(TMicroType))
                return false;

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var rawValue = value?.RawValue;

            if (rawValue == null)
            {
                bindingContext.Model = GetDefault();
                return true;
            }

            TMicroType v;
            if (TryConvert(rawValue, out v))
            {
                bindingContext.Model = v;
                return true;
            }


            bindingContext.ModelState.AddModelError(bindingContext.ModelName,
                $"Cannot convert '{rawValue}' to type {typeof(TMicroType).Name}");
            return false;
        }

        bool TryConvert(object input, out TMicroType result)
        {
            try
            {
                var v = (TPrimitive) Convert.ChangeType(input, typeof(TPrimitive));
                result = CreateInstance(v);
                return true;
            }
            catch (Exception)
            {
                result = default(TMicroType);
                return false;
            }
        }

        protected virtual TMicroType CreateInstance(TPrimitive value)
        {
            return (TMicroType) Activator.CreateInstance(typeof(TMicroType), value);
        }

        protected virtual TMicroType GetDefault()
        {
            return default(TMicroType);
        }
    }
}