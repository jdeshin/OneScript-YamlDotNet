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
    public class YamlDotNetProcessor : AutoContext<YamlDotNetProcessor>
    {
        public YamlDotNetProcessor()
        {
        }

        [ScriptConstructor(Name = "Без параметров")]
        public static IRuntimeContextInstance Constructor()
        {
            return (IRuntimeContextInstance)new YamlDotNetProcessor();
        }
    }
}
