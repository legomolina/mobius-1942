# Mobius

> Just for learning purposes

This project is a 2D game engine based on [SDL2 technology](https://www.libsdl.org/) and 
I'm using a [C# wrapper](https://github.com/flibitijibibo/SDL2-CS) over SDL2 C lib.

Also, while developing the engine, I'm just building a simple 1942-like game just for testing.

All assets are downloaded from [Kenney website](https://www.kenney.nl/).

### Develop

I'm Windows user so I'm using Visual Studio Community 2022 and NuGet to handle deps.
All you need to run this project is open the solution and set 1942 as Run Project (if not set) and hit Play.

In case anyone knows how to run this on other OS just open a pull request updating this readme. :)

### Build

From Visual Studio you can run Release configuration and all files needed to execute the game will be compiled into `./1942/Build/Release/`
and you can just execute the `1942.exe` file.

I have no idea how to build this in other OS. I've tried dotnet in linux and it compiles the project:

```bash
dotnet build 1942/1942.csproj -o build/ --self-contained true -r linux-x64 -c Release
```

but I can't make it run when I run

```bash
./build/1942
```

it throws an error

```
Unhandled exception. System.DllNotFoundException: Unable to load shared library 'SDL2' or one of its dependencies. In order to help diagnose loading problems, consider setting the LD_DEBUG environment variable: libSDL2: cannot open shared object file: No such file or directory
   at SDL2.SDL.SDL_Init(UInt32 flags)
   at Engine.Core.GraphicsManager.Init() in /mnt/p/Projects/Mobius/Mobius/Core/GraphicsManager.cs:line 74
   at Engine.Core.GraphicsManager..ctor() in /mnt/p/Projects/Mobius/Mobius/Core/GraphicsManager.cs:line 69
   at Engine.Core.GraphicsManager.get_Instance() in /mnt/p/Projects/Mobius/Mobius/Core/GraphicsManager.cs:line 30
   at Engine.Game..ctor() in /mnt/p/Projects/Mobius/Mobius/Game.cs:line 20
   at _1942.Game1942..ctor() in /mnt/p/Projects/Mobius/1942/Game1942.cs:line 17
   at Program.<Main>$(String[] args) in /mnt/p/Projects/Mobius/1942/Program.cs:line 3
Aborted
```

So if you know how to make it work just make a PR and I've include it here!
