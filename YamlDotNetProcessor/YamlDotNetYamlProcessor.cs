// Copyright (c) Yury Deshin
// This software based on YamlDotNet library
// Copyright (c) Antoine Aubry and contributors
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public IValue ReadYamlString(string yaml)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var reader = new StringReader(yaml);
            var result = deserializer.Deserialize(reader);

            return BuildResults(result);
        }

        /// <summary>
        /// Строит объекты OneScript на основе результатов парсинга 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private IValue BuildResults(object source)
        {
            if (source == null)
                return ValueFactory.Create();

            if (source is List<object>)
            {
                ArrayImpl array = new ArrayImpl();

                foreach (object element in (List<object>)source)
                    array.Add(BuildResults(element));

                return array;
            }

            if (source is Dictionary<object, object>)
            {
                MapImpl map = new MapImpl();

                foreach (var element in (Dictionary<object, object>)source)
                    map.Insert(BuildResults(element.Key), BuildResults(element.Value));

                return map;
            }

            if (source is bool)
                return ValueFactory.Create(System.Convert.ToBoolean(source));

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

                return ValueFactory.Create(System.Convert.ToDecimal(source));

            if (source is DateTime)
                return ValueFactory.Create(System.Convert.ToDateTime(source));

            // Строка или нечто другое
            return ValueFactory.Create(System.Convert.ToString(source));
        }

    }
}
