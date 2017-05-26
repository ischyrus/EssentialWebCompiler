## EssentialWebCompiler

Simple, standalone executable for Windows command-line compilation of LESS and SASS files derived from madskristensen's excellent WebCompiler project.

### Features

- Compiles LESS, Scss, Stylus, JSX, ES6 and (Iced)CoffeeScript
- Based upon acclaimed WebCompiler project by madskristensen
- Standalone Windows compiler executable
- Command line execution
- Single configuration file, no dependencies

### Getting started

Place the WebCompiler.exe executable in the same directory as compilerconfig.json.
Running the executable will run a fresh compilation of your stylesheet using
the provided configuration. No arguments are necessary; configuration is ascertained
from compilerconfig.json.

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