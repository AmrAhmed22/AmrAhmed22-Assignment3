# Part G — Short Answers

## Q1 — .csproj contents

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

Yes, all 4 are there: OutputType, TargetFramework, ImplicitUsings, Nullable.

## Q2 — Do #region / #endregion change the compiled output?

No, they don't do anything to the actual build. They just let you collapse chunks of code in the editor so the file is easier to read. The compiler ignores them completely.

## Q3 — When would you use /// instead of //?

I'd use /// when I'm documenting something other people (or my IDE) will use, like a method's purpose, its parameters, what it returns. That's what makes the tooltip show up in IntelliSense. Regular // is more for quick notes to myself about a specific line while I'm writing the code.

## Q4 — Why no true global variables in C#, and what's closest to one?

Everything in C# has to belong to a class, there's no "floating" variable outside of one. It's basically to stop people from making messy code where any part of the program can change some variable with no clear owner. The closest thing to a global variable is a public static field on a class, since you can access it from anywhere without creating an object first.
