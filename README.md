# Json-To-Csv-Converter
Console app that converts json file to csv file.

Commands:

-i "json file path" is required. Can be absolute or relative. Example: "data\products.json" or "F:\VisualStudioRepos\ConsoleApp1\bin\Debug\net6.0\data\products.json".

-o "csv file path" output for converted file. If undefined will use same file name as input file and be created at same folder as executable.

-s "string" is separator string. By default is ",".

-e "int32" code page for csv encoding. By default is 65001 (utf-8).

-q is parameter to close console automatically.
