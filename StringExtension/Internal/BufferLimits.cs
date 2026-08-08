namespace StringExtension.Internal;

/// <summary>
/// Shared implementation constants. Not part of the public API.
/// </summary>
internal static class BufferLimits
{
    /// <summary>
    /// Above this length, buffers are rented from <see cref="System.Buffers.ArrayPool{T}"/>
    /// instead of stack-allocated, to avoid excessive stack usage for large inputs. 512 chars
    /// (1024 bytes) matches the safe stackalloc limit used throughout the .NET base class library.
    /// </summary>
    internal const int StackAllocThreshold = 512;
}