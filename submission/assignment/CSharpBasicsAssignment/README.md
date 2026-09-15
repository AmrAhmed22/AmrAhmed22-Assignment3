# CSharpBasicsAssignment

**Module:** C# Basics — Module 1-1
**Assignment:** Assignment 4 — Console Apps, Types & Memory Model
**Based on:** Lecture 01 (project structure, variables & types, value vs. reference types, scope, operators, casting, stack vs. heap)

## Overview

A single console solution containing several short, focused programs, each mapping to one part of the assignment. Every part is implemented as its own method, called from the top-level `Program.cs`, with a labeled console header printed before it runs.

## How to run

```bash
dotnet restore
dotnet run
```

This prints labeled output for every part (A, B, C, D, F) in sequence to the console.

## Project structure

```
CSharpBasicsAssignment/
 CSharpBasicsAssignment.csproj   -> project file (SDK, target framework, build settings)
 Program.cs                      -> entry point; top-level statements calling each part's method
 Order.cs                        -> Order class used in Part C (reference type demo)
 STACK_HEAP.md                   -> Part E: stack/heap diagrams (Markdown, not code)
 README.md                       -> this file
 ANSWERS.md                      -> Part G: short answers
```

## Parts covered

| Part | Topic | Where |
|------|-------|-------|
| A | Project & Structure | Comments at top of `Program.cs` |
| B | Variables, Types & Casting | `RunTypesDemo()` in `Program.cs` |
| C | Value vs. Reference Types | `RunValueVsReferenceDemo()` in `Program.cs` + `Order.cs` |
| D | Scope & Operators | `RunScopeDemo()`, `RunOperatorsDemo()`, `RunBitwiseDemo()` in `Program.cs` |
| E | Draw the Stack & Heap | `STACK_HEAP.md` |
| F | LeetCode 136 — Single Number | `RunLeetCodeDemo()` / `FindSingleNumber()` in `Program.cs` |
| G | Short Answers | `ANSWERS.md` |

## Notes

- `Program.cs` uses **top-level statements**, so it lives in the implicit global-namespace `Program` class and does **not** declare an explicit namespace itself (mixing top-level statements with a namespace declaration is a compile error). The file-scoped namespace requirement from Part A is instead demonstrated in `Order.cs`.
- Solution format used: classic `.sln` (not the newer `.slnx`).
