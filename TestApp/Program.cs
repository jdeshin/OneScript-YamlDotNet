
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.IO;

using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;

using YamlDotNet;


namespace TestApp
{
    public enum ObjectType
    {
        List,
        Dictionary,
        Value
    }

    class Program
    {
        static void Main(string[] args)
        {
            ScriptEngine.HostedScript.HostedScriptEngine engine = new ScriptEngine.HostedScript.HostedScriptEngine();
            engine.Initialize();

            System.IO.TextReader rd = System.IO.File.OpenText("text.txt");
            string yaml = rd.ReadToEnd();
            var sr = new StringReader(yaml);

            var deserializer = new YamlDotNet.Serialization.Deserializer();

            var result = deserializer.Deserialize(sr);

            IValue res = BuildResults(result);
        }

        private static IValue BuildResults(object source)
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
