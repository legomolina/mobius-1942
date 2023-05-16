# Mobius

> Just for learning purposes

This project is a 2D game engine based on [SDL2 technology](https://www.libsdl.org/) and 
I'm using a [C# wrapper](https://github.com/flibitijibibo/SDL2-CS) over SDL2 C lib.

Also, while developing the engine, I'm just building a simple 1942-like game just for testing.

All assets are downloaded from [Kenney website](https://www.kenney.nl/).

### Develop

I'm Windows user so I'm using Visual Studio Community 2022 and NuGet to handle deps.
All you need to run this project is open the solution and set 1942 as Run Project (if not set) and hit Play.

In linux you can use `dotnet run` inside 1942 folder to execute the program.

In case anyone knows how to run this better on Linux/MacOS just open a pull request updating this readme. :)

### Build

From Visual Studio go to `Build -> Publish` and there should be 2 projects: one for linux and one for windows. It builds the project into `Mobius\1942\bin\Release\net6.0\publish`.

In linux you can use

```bash
dotnet build 1942/1942.csproj -o build/ --self-contained true -r linux-x64 -c Release
```

### Known caveats

#### Linux run

To make program run under linux I've needed to install the next packages:

```bash
libsdl2-dev
libsdl2-image-dev
libsdl2-mixer-dev
libsdl2-ttf-dev
```

I suppose that program should install these libs automatically but I have no idea how to do it now, if anyone knows how or could point me to the correct direction just open an issue :)
