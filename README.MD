# Yaml процессор для OneScript

Внешняя компонента реализующая возможность преобразования текста в формате Yaml, в объекты [OneScript](https://github.com/EvilBeaver/OneScript).

Компонента представляет собой обертку над библиотекой [YamlDotNet](https://github.com/aaubry/YamlDotNet).


## Установка

Установка осуществляется простым копированием файлов dll в какую-нибудь папку.

## Использование

```bsl
ПодключитьВнешнююКомпоненту("ПутьКПапкеСDll\MarkdigMarkdownProcessor.dll");

Процессор = Новый MarkdownПроцессорMarkdig;
СтрокаMarkdown = "This is a text with some *emphasis*";

// Вызов без обработки расширений Markdown
СтрокаHtml = Процессор.ПолучитьHtmlИзMarkdown(СтрокаMarkdown);
СтрокаТекст = Процессор.ПолучитьСтрокуИзMarkdown(СтрокаMarkdown);

Сообщить(СтрокаHtml); // <p>This is a text with some <em>emphasis</em></p>
Сообщить(СтрокаТекст); // 

// Вызов с обработкой расширений, за исключением BootStrap, Emoji, SmartyPants и soft line как hard line breaks
СтрокаHtml = Процессор.ПолучитьHtmlИзMarkdown(СтрокаMarkdown, Истина);
СтрокаТекст = Процессор.ПолучитьСтрокуИзMarkdown(СтрокаMarkdown, Истина);

Сообщить(СтрокаHtml);
Сообщить(СтрокаТекст);
```

