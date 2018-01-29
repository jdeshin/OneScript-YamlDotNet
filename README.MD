# Yaml процессор для OneScript

Внешняя компонента реализующая возможность преобразования текста в формате Yaml, в объекты [OneScript](https://github.com/EvilBeaver/OneScript).

Компонента представляет собой обертку над библиотекой [YamlDotNet](https://github.com/aaubry/YamlDotNet).


## Установка

Установка осуществляется простым копированием файлов dll в какую-нибудь папку.

## Использование

```bsl
ПодключитьВнешнююКомпоненту("ПутьКПапкеГдеРасположеныDll\YamlDotNetProcessor.dll");
Процессор = Новый YamlПроцессорYamlDotNet;

// Нижеследующий текст будет преобразован в массив строк
СтрокаYaml = "
|--- # Favorite movies
| - Casablanca
| - North by Northwest
| - The Man Who Wasn't There";

ОбъектыМассив = Процессор.ПрочитатьYaml(СтрокаYaml);

Для Каждого ЭлементМассива Из ОбъектыМассив Цикл
	Сообщить(ЭлементМассива);
КонецЦикла;

// Нижеследующий текст будет преобразован в соответствие
СтрокаYaml = "---
|a: 123                     # an integer
|b: ""123""                 # a string, disambiguated by quotes
|c: 123.0                   # a float
|d: !!float 123             # also a float via explicit data type prefixed by (!!)
|e: !!str 123               # a string, disambiguated by explicit type
|f: !!str Yes               # a string via explicit type
|g: True                     # a boolean True (yaml1.1), string ""Yes"" (yaml1.2)
|h: Yes we have No bananas  # a string, ""Yes"" and ""No"" disambiguated by context.
|...";

ОбъектыСоответствие = Процессор.ПрочитатьYaml(СтрокаYaml);

Для Каждого ЭлементСоответствия Из ОбъектыСоответствие Цикл
	Сообщить(ЭлементСоответствия.Ключ + " | " + ЭлементСоответствия.Значение);
КонецЦикла;

```

