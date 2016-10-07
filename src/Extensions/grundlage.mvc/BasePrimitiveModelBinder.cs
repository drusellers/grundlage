using System;
using System.Web.Mvc;

namespace grundlage.mvc
{
    public abstract class BasePrimitiveModelBinder<TMicroType, TPrimitive> : IModelBinder
        where TMicroType : ValueType<TPrimitive>
        where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive>, IComparable
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(TMicroType))
            {
                return null;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value?.RawValue == null)
            {
                return GetDefault();
            }

            var rawArrayValue = value.RawValue as string[] ?? new[] { value.RawValue.ToString() };

            if (rawArrayValue.Length == 1)
            {
                TMicroType v;
                if (TryConvert(rawArrayValue[0], out v))
                {
                    return v;
                }
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Cannot convert '{rawArrayValue}' to type {typeof(TMicroType).Name}");
            return null;
        }

        bool TryConvert(string input, out TMicroType result)
        {
            try
            {
                var v = (TPrimitive)Convert.ChangeType(input, typeof(TPrimitive));
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
            return (TMicroType)Activator.CreateInstance(typeof(TMicroType), value);
        }

        protected virtual TMicroType GetDefault()
        {
            return default(TMicroType);
        }
    }
}
