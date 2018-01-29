using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;

using YamlDotNet;
using YamlDotNetProcessor;

namespace YamlDotNetProcessor
{
    [ContextClass("YamlПроцессорYamlDotNet", "YamlDotNetProcessor")]
    public class YamlDotNetYamlProcessor : AutoContext<YamlDotNetYamlProcessor>
    {
        public YamlDotNetYamlProcessor()
        {
        }

        [ScriptConstructor(Name = "Без параметров")]
        public static IRuntimeContextInstance Constructor()
        {
            return (IRuntimeContextInstance)new YamlDotNetYamlProcessor();
        }

        [ContextMethod("ПрочитатьYaml", "ReadYaml")]
        public IValue ReadYamlString(string yamlString)
        {
            return ValueFactory.Create();
        }

        public static object GetValue(object source)
        {
            if (source == null)
                return source;

            if (source is List<object>)
            {
                foreach (object element in (List<object>)source)
                {
                    object value = GetValue(element);
                }

                return source;
            }

            if (source is Dictionary<object, object>)
            {
                foreach (var element in (Dictionary<object, object>)source)
                {
                    object key = GetValue(element.Key);
                    object value = GetValue(element.Value);
                }

                return source;
            }

            if (source is bool)
                return source;

            // Число
            if (source is sbyte
                || source is byte
                || source is short
                || source is ushort
                || source is int
                || source is uint
                || source is long
                || source is ulong
                || source is float
                || source is double
                || source is decimal
               )

                return source;

            // Дата
            if (source is DateTime)
                return source;

            // Строка или нечто другое
            return source.ToString();
        }

    }
}
