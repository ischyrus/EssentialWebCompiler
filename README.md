## EssentialWebCompiler

EssentialWebCompiler is a run-time stylesheet compiler for .NET, and a fork of madskristensen's [WebCompiler](https://github.com/madskristensen/WebCompiler).
The WebCompiler extension for Visual Studio allows you to easily update your stylesheets at development time, both manually and via source file tracking.
EssentialWebCompiler instead allows you to invoke the powerful compiler at run-time, making on-the-fly stylesheet compilation simple. I personally
created this solution for allowing my applications' users to amend the site theme at run-time.

### Features

- Runtime stylesheet compilation
- Based upon popular and reliable [WebCompiler](https://github.com/madskristensen/WebCompiler) project by madskristensen
- Compiles LESS, Scss, Stylus, JSX, ES6 and (Iced)CoffeeScript
- Event and error logging included
- Simple installation into your application via [NuGet](https://www.nuget.org/packages/Equus.Tools.EssentialWebCompiler/)

### WebCompiler Users
Since the heart of EssentialWebCompiler is WebCompiler, your existing compilerconfig.json file is interoperable for
both purposes. 

### Getting started

Expect EssentialWebCompiler to use ~100 MB in a temporary folder where it will store necessary Node.js dependencies.
Dependencies for all supported CSS preprocessor compilation are included, so if you're looking to save space consider taking those out.

Install EssentialWebCompiler into your project that requires run-time compilation using [NuGet](https://www.nuget.org/packages/Equus.Tools.EssentialWebCompiler/).
To compile your stylesheet, invoke the compiler, providing the path to your compilerconfig.json file which specifies source and output files as well as any other options.

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
