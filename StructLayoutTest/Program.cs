using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        var data = new[] { (byte)'J', (byte)'M' };

        var ptr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, ptr, data.Length);
        var sequential = Marshal.PtrToStructure<MyStructSequential>(ptr);
        Marshal.FreeHGlobal(ptr);

        Console.WriteLine($"sequential.A: {sequential.A} ({(int)sequential.A}) (0x{(int)sequential.A:X})");
        Console.WriteLine($"sequential.B: {sequential.B} ({(int)sequential.B}) (0x{(int)sequential.B:X})");

        ptr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, ptr, data.Length);
        var explicitLayout = Marshal.PtrToStructure<MyStructExplicit>(ptr);
        Marshal.FreeHGlobal(ptr);

        Console.WriteLine($"explicitLayout.A: {explicitLayout.A} ({(int)explicitLayout.A}) (0x{(int)explicitLayout.A:X})");
        Console.WriteLine($"explicitLayout.B: {explicitLayout.B} ({(int)explicitLayout.B}) (0x{(int)explicitLayout.B:X})");
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct MyStructSequential
{
    public char A;
    public char B;
}

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1)]
public struct MyStructExplicit
{
    [FieldOffset(0)]
    public char A;
    [FieldOffset(1)]
    public char B;
}