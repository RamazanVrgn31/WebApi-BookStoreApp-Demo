using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Services.Contrats;

namespace Services.Concrete
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {

        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        IEnumerable<ExpandoObject> IDataShaper<T>.ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequeiredProperties(fieldsString);
            return FetcData(entities, requiredProperties);
        }

        ExpandoObject IDataShaper<T>.ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequeiredProperties(fieldsString);
            return FetcDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequeiredProperties(string fieldsString)
        {

            var requiredFields = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    if (property != null)
                    {
                        requiredFields.Add(property);
                    }
                }
            }
            else
            {
                requiredFields= Properties.ToList();
            }
            
            return requiredFields;
        }

        private ExpandoObject FetcDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);

            }
            return shapedObject;
        }
        private IEnumerable<ExpandoObject> FetcData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var shapedObject = FetcDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }

}
