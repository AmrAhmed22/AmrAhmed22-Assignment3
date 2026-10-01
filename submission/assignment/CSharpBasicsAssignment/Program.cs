/* .csproj->Project file.Tells the .NET SDK how to build this code:
               target framework, output type, dependencies, compiler
               settings (ImplicitUsings, Nullable, etc). No app logic here.
 Program.cs -> Entry point of the app. Contains the code that actually
               runs (top-level statements act as Main).
 obj/       -> Intermediate build artifacts (generated code, incremental
               build cache). Safe to delete; regenerated every build.
 bin/       -> Final build output (compiled .dll/.exe + runtime config)
               that actually gets run/deployed*/

// File-scoped namespace: everything in this file belongs to this namespace
// without wrapping it in an extra { } block. Top-level statements below
// don't need to be nested one level deeper — same meaning as
// "namespace CSharpBasicsAssignment { ... everything ... }" but flatter.
using CSharpBasicsAssignment;


// This project uses the classic .sln format (not the newer .slnx).
// Advantage of .slnx (the one NOT picked): plain, much smaller XML file,
// far easier to read/diff in git than the GUID-heavy classic .sln format.




// Acts like a private field: declared at the top level, it becomes a field
// of the compiler-generated Program class, so every local function in this
// file can read/write it (field scope).
int sharedCounter = 10;


Console.WriteLine("=== PART A: Project & Structure ===");
Console.WriteLine("See comments at the top of this file for the explanation.");
Console.WriteLine();

RunTypesDemo();
RunValueVsReferenceDemo();
RunScopeDemo();
RunOperatorsDemo();
RunBitwiseDemo();
RunLeetCodeDemo();

// ---------------------------------------------------------------------
// PART B — Variables, Types & Casting
// ---------------------------------------------------------------------
void RunTypesDemo()
{
    Console.WriteLine("=== PART B: Variables, Types & Casting ===");

    int myInt = 10;
    long myLong = 10000000000L;
    double myDouble = 3.14;
    decimal myDecimal = 99.99m;
    bool myBool = true;
    char myChar = 'A';
    string myString = "Hello";
    var myInferred = 42; // inferred as int

    Console.WriteLine($"int: {myInt}, type: {myInt.GetType()}");
    Console.WriteLine($"long: {myLong}, type: {myLong.GetType()}");
    Console.WriteLine($"double: {myDouble}, type: {myDouble.GetType()}");
    Console.WriteLine($"decimal: {myDecimal}, type: {myDecimal.GetType()}");
    Console.WriteLine($"bool: {myBool}, type: {myBool.GetType()}");
    Console.WriteLine($"char: {myChar}, type: {myChar.GetType()}");
    Console.WriteLine($"string: {myString}, type: {myString.GetType()}");
    Console.WriteLine($"var (inferred): {myInferred}, type: {myInferred.GetType()}");

    // Implicit conversion
    int smallInt = 100;
    long widenedLong = smallInt;   // int -> long: long has more range, always fits
    char letter = 'Z';
    int charToInt = letter;        // char -> int: char is a 16-bit number under the
                                   // hood, which always fits inside an int
    Console.WriteLine($"Implicit int->long: {widenedLong}");
    Console.WriteLine($"Implicit char->int: {charToInt}");

    // Explicit conversion
    double someDouble = 9.75;
    int castResult = (int)someDouble;               // truncates toward zero -> 9
    int convertResult = Convert.ToInt32(someDouble); // rounds to nearest -> 10
    Console.WriteLine($"(int) cast: {castResult} (truncation)");
    Console.WriteLine($"Convert.ToInt32: {convertResult} (rounding)");

    // Integer division trap
    int intDivision = 5 / 2;         // both operands int -> result stays int -> 2
    double doubleDivision = 5.0 / 2; // one operand double -> result promotes to double -> 2.5
    Console.WriteLine($"5 / 2 (int): {intDivision}");
    Console.WriteLine($"5.0 / 2 (double): {doubleDivision}");

    // Boxing / unboxing
    int originalValue = 7;
    object boxed = originalValue;   // boxing: value copied onto the heap, wrapped in an object
    int unboxed = (int)boxed;       // unboxing: value copied back out into a stack value type
    Console.WriteLine($"Boxed value: {boxed}");
    Console.WriteLine($"Unboxed value: {unboxed}");

    // Parsing
    int parsed = int.Parse("42");
    Console.WriteLine($"int.Parse(\"42\"): {parsed}");

    bool success = int.TryParse("abc", out int triedValue);
    Console.WriteLine($"int.TryParse(\"abc\") succeeded: {success}, value: {triedValue}");

    // float -> decimal
    float myFloat = 3.5f;
    // decimal implicitFromFloat = myFloat; // Does NOT compile.
    // The compiler refuses this because float and decimal use completely
    // different internal representations (binary floating-point vs.
    // base-10 fixed-point) with different precision/range trade-offs.
    // Going float -> decimal silently could misrepresent the value, so C#
    // forces you to opt in explicitly with a cast.
    decimal explicitFromFloat = (decimal)myFloat; // correct, explicit way
    Console.WriteLine($"float -> decimal (explicit cast): {explicitFromFloat}");

    Console.WriteLine();
}

// ---------------------------------------------------------------------
// PART C — Value vs. Reference Types
// ---------------------------------------------------------------------
void RunValueVsReferenceDemo()
{
    Console.WriteLine("=== PART C: Value vs. Reference Types ===");

    // Experiment 1 — struct copy semantics
    Point p1 = new Point { X = 1, Y = 2 };
    Point p2 = p1; // COPIES all fields; p2 is now a fully independent value

    p2.X = 99;
    Console.WriteLine($"p1.X = {p1.X}, p2.X = {p2.X}");
    // p1.X stays 1: structs are value types, so "Point p2 = p1;" copies every
    // field into a brand-new block of memory. p2 has zero connection to p1
    // after that — mutating p2 can never affect p1.

    // Experiment 2 — class reference semantics
    Order o1 = new Order
    {
        OrderId = 1,
        CustomerName = "Ali",
        Quantity = 3,
        UnitPrice = 50.0m,
        TotalPrice = 0m,
        IsPaid = false,
        DiscountPercent = 10.0,
        ShippingCity = "Cairo",
        Priority = 'H',
        ItemCode = 100200300L
    };
    
    o1.CalculateTotal();

    Order o2 = o1; // COPIES only the reference (heap address) — both variables
                   // now point at the exact same object

    o2.IsPaid = true;
    Console.WriteLine($"o1.IsPaid = {o1.IsPaid}, o2.IsPaid = {o2.IsPaid}");
    // Both are true: o1 and o2 hold the same address, so there is only ONE
    // Order object on the heap. Mutating it through either variable is
    // visible through both — shared heap identity.

    object boxedOrder = o1; // NOT boxing — Order is already a reference type.
                            // This just copies the same address into an
                            // object-typed variable.
    Order o3 = (Order)boxedOrder;
    Console.WriteLine($"ReferenceEquals(o1, o3): {object.ReferenceEquals(o1, o3)}");

    o2.PrintSummary(); // shows IsPaid = True, proving o1/o2 share one object

    Console.WriteLine();

   /*
    Value types (struct, int, double, bool...) hold their data directly  wherever they're declared (stack, or inline inside another object)
    assignment copies the entire value. Reference types (class instances,like Order) live on the heap
    the variable itself only holds the object's address. Assignment copies that address, not the object, so two variables can end up pointing at the same heap instance.
   Storing a reference type inside an 'object' variable does not create a new object 
   it just copies the same address into a variable of a more general type.
   */


    Console.WriteLine();
}

// ---------------------------------------------------------------------
// PART D1 — Scope
// ---------------------------------------------------------------------


void RunScopeDemo()
{
    Console.WriteLine("=== PART D1: Scope ===");

    ReadCounterA();
    ReadCounterB();
    MethodScopeExample();

    for (int i = 0; i < 3; i++)
    {
        int loopLocal = i * 2; // block-scoped: destroyed at the end of each iteration
        Console.WriteLine($"i = {i}, loopLocal = {loopLocal}");
    }
    // Console.WriteLine(i);         // Compile error CS0103: "The name 'i' does
    //                                  not exist in the current context" — i
    //                                  only exists inside the for-loop's block.
    // Console.WriteLine(loopLocal); // Same error for loopLocal — both are
    //                                  destroyed the instant the loop's { } ends.

    Console.WriteLine();
}

void ReadCounterA() => Console.WriteLine($"ReadCounterA sees sharedCounter = {sharedCounter}");
void ReadCounterB() => Console.WriteLine($"ReadCounterB sees sharedCounter = {sharedCounter}");

void MethodScopeExample()
{
    int methodOnly = 5; // method-scoped: only visible inside this method
    Console.WriteLine($"methodOnly inside its own method: {methodOnly}");
}
// methodOnly is not accessible here — different method, out of scope.

// ---------------------------------------------------------------------
// PART D2 — Compound Assignment Operators
// ---------------------------------------------------------------------
void RunOperatorsDemo()
{
    Console.WriteLine("=== PART D2: Compound Assignment Operators ===");

    int total = 100;
    total += 20; Console.WriteLine($"After += 20: {total}");
    total -= 30; Console.WriteLine($"After -= 30: {total}");
    total *= 2; Console.WriteLine($"After *= 2: {total}");
    total /= 3; Console.WriteLine($"After /= 3: {total}");
    total %= 7; Console.WriteLine($"After %= 7: {total}");

    // Long-form equivalent of "total += 20;" above:
    // total = total + 20;

    Console.WriteLine();
}

// ---------------------------------------------------------------------
// PART D3 — Bitwise Operators
// ---------------------------------------------------------------------
void RunBitwiseDemo()
{
    Console.WriteLine("=== PART D3: Bitwise Operators ===");

    int a = 12; // 1100
    int b = 10; // 1010

    Console.WriteLine($"a & b = {a & b}"); // 1100 & 1010 = 1000 -> 8
    Console.WriteLine($"a | b = {a | b}"); // 1100 | 1010 = 1110 -> 14
    Console.WriteLine($"a ^ b = {a ^ b}"); // 1100 ^ 1010 = 0110 -> 6

    // & (bitwise AND) always evaluates BOTH operands and compares them bit
    // by bit, no matter what. && (logical AND) short-circuits: if the left
    // operand is false, it never evaluates the right operand at all. So in
    // an if-condition, a false left side skips the right side with && but
    // NOT with & — that's the practical difference.

    Console.WriteLine();
}

// ---------------------------------------------------------------------
// PART F — LeetCode 136: Single Number
// ---------------------------------------------------------------------
void RunLeetCodeDemo()
{
    Console.WriteLine("=== PART F: LeetCode 136 - Single Number ===");

    int[] test1 = { 4, 1, 2, 1, 2 };
    int[] test2 = { 7, 3, 5, 3, 7 };

    Console.WriteLine($"Result 1: {FindSingleNumber(test1)}"); // 4
    Console.WriteLine($"Result 2: {FindSingleNumber(test2)}"); // 5

    Console.WriteLine();
}

int FindSingleNumber(int[] nums)
{
    // XOR-ing a number with itself gives 0, and XOR-ing anything with 0
    // returns that thing unchanged. So every number appearing exactly
    // twice cancels itself out to 0 as we go, and only the number that
    // appears an odd number of times (once) survives in the final result.
    int num = nums[0];

    for (int i = 1; i < nums.Length; i++)
    {
        num ^= nums[i];
    }

    return num;
}

// Used only in Part C, Experiment 1.
struct Point
{
    public int X;
    public int Y;
}