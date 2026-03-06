using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public class CsvHandler : ICsvHandler
    {
        public Task<T[]> DeserializeAsync<T>(string[] context)
        {
            if (context == null || context.Count() == 0)
            {
                return Task.FromResult(Array.Empty<T>());
            }
            return Task.Run(async () =>
            {
                var csvHelper = new TextFieldParser(new StringReader(string.Join(Environment.NewLine, context)))
                {
                    TextFieldType = FieldType.Delimited,
                    Delimiters = new[] { "," }
                };
                var fieldNames = csvHelper.ReadFields() ?? Array.Empty<string>();

                if (!fieldNames.All(f => typeof(T).GetProperties().Select(s => s.Name.ToLower()).Contains(f.ToLower())))
                {
                    throw new InvalidOperationException("CSV field names do not match the properties of the target type.");
                }

                var results = new List<T>();

                while (!csvHelper.EndOfData)
                {
                    var fields = csvHelper.ReadFields() ?? Array.Empty<string>();
                    var obj = Activator.CreateInstance<T>();
                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        var property = typeof(T).GetProperty(fieldNames[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (property != null)
                        {
                            var value = Convert.ChangeType(fields[i], property.PropertyType);
                            property.SetValue(obj, value);
                        }
                    }
                    results.Add(obj);
                }

                return results.ToArray();
            });
        }
    }
}