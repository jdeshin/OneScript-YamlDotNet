// Copyright (c) Yury Deshin
// This software based on YamlDotNet library
// Copyright (c) Antoine Aubry and contributors
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

using YamlDotNet;

namespace YamlDotNetProcessorCom
{
    [Guid("66567974-A8CE-49E7-A8EF-8D48C88E7A12")]
    public interface YamlDotNetYamlProcessorInterface
    {
        [DispId(1)]
        Object ReadYamlString(string yaml);
        [DispId(2)]
        int GetValueType(Object obj);
        [DispId(3)]
        Object GetByIndex(Object obj, int index);
        [DispId(4)]
        Object GetByKey(Object obj, Object key);
        [DispId(5)]
        Object ToArray(object obj);
        [DispId(6)]
        Object GetKeyByIndex(Object obj, Object index);
        [DispId(7)]
        Object GetValueByIndex(Object obj, Object index);
    }

    [Guid("A387B637-513B-4FAC-8CC5-C1F25DB463B8"),
        InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface YamlDotNetYamlProcessorEvents
    {
    }

    [Guid("FFB18C29-0078-4020-A8CC-2127F1671460"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(YamlDotNetYamlProcessorEvents))]
    public class YamlDotNetYamlProcessor : YamlDotNetYamlProcessorInterface
    {
        public YamlDotNetYamlProcessor()
        {
        }

 
        public Object ReadYamlString(string yaml)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var reader = new StringReader(yaml);
            var result = deserializer.Deserialize(reader);

            return BuildResults(result);
        }

        public int GetValueType(Object obj)
        {
            // Соответствие
            if (obj is Hashtable)
                return 0;
            // Массив
            if (obj is ArrayList)
                return 1;
            // Простой тип
            return 2;
        }
        
        public Object GetByIndex(Object obj, int index)
        {
            return ((ArrayList)obj)[index];
        }

        public Object GetByKey(Object obj, Object key)
        {
            return ((Hashtable)obj)[key];
        }

        public Object ToArray(object obj)
        {
            Hashtable table = (Hashtable)obj;
            ArrayList list = new ArrayList();

            foreach(DictionaryEntry current in table)
            {
                list.Add(current);
            }

            return list;
        }

        public Object GetKeyByIndex(Object obj, Object index)
        {
            return ((DictionaryEntry)((ArrayList)obj)[(int)index]).Key;
        }

        public Object GetValueByIndex(Object obj, Object index)
        {
            return ((DictionaryEntry)((ArrayList)obj)[(int)index]).Value;
        }

        /// <summary>
        /// Строит объекты OneScript на основе результатов парсинга 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private Object BuildResults(object source)
        {
            if (source is List<object>)
            {
                ArrayList array = new ArrayList();

                foreach (object element in (List<object>)source)
                    array.Add(BuildResults(element));

                return array;
            }

            if (source is Dictionary<object, object>)
            {
                Hashtable map = new Hashtable();

                foreach (var element in (Dictionary<object, object>)source)
                    map.Add(System.Convert.ToString(element.Key), BuildResults(element.Value));

                return map;
            }

            if (   source == null
                || source is string
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
                || source is sbyte
                || source is bool
               )

                return source;

            // ToDo: Поддерживается или нет?
            if (source is DateTime)
                return source;

            //нечто другое
            return System.Convert.ToString(source);
        }

    }
}
