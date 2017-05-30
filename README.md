## EssentialWebCompiler

EssentialWebCompiler is a run-time stylesheet compiler for .NET, and a fork of madskristensen's [WebCompiler](https://github.com/madskristensen/WebCompiler).
While WebCompiler offers an out-of-the-box Visual Studio extension for easily updating your stylesheets at development time, 
EssentialWebCompiler allows you to invoke the powerful compiler at run-time, making modifying your application's theme simple and painless. 

Since the heart of EssentialWebCompiler is WebCompiler, your existing compilerconfig.json file is interoperable for
both purposes.  Because EssentialWebCompiler is meant to provide speedy run-time compilation, much of the underlying compiler library is preloaded,
which means it uses a little more space in your codebase, but will have a faster average compilation time.

### Features

- Runtime stylesheet compilation
- Based upon popular and reliable [WebCompiler](https://github.com/madskristensen/WebCompiler) project by madskristensen
- Compiles LESS, Scss, Stylus, JSX, ES6 and (Iced)CoffeeScript
- Event and error logging included
- Simple installation into your application via [NuGet](https://www.nuget.org/packages/Equus.Tools.EssentialWebCompiler/)

### Getting started

Install EssentialWebCompiler into your project that requires run-time compilation using [NuGet](https://www.nuget.org/packages/Equus.Tools.EssentialWebCompiler/).
After that, it's simple as pointing the compiler in the direction of you WebCompiler configuration file and saying "Compile".

```C#
var results = Essential.Compile("C:/FooSolution/BarProject/compilerconfig.json");
```
The outputted results contains any log messages that were generated as well as any errors. Your stylesheet(s) will automatically be updated according to your configuraiton file.

### compilerconfig.json

This is an example compilerconfig.json file. The format is identical to the original specified in madskristensen's WebCompiler.

```json
[
  {
    "outputFile": "output/site.css",
    "inputFile": "input/site.less",
    "minify": {
        "enabled": true
    },
    "includeInProject": true,
    "options":{
        "sourceMap": false
    }
  },
  {
    "outputFile": "output/scss.css",
    "inputFile": "input/scss.scss",
    "minify": {
        "enabled": true
    },
    "includeInProject": true,
    "options":{
        "sourceMap": true
    }
  }
]
```