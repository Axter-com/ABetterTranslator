using System;
using System.Buffers;

namespace GTranslate;

internal class MemorySegment<T> : ReadOnlySequenceSegment<T>
{
    public MemorySegment(ReadOnlyMemory<T> memory)
    {
        Memory = memory;
    }

    public MemorySegment<T> Append(ReadOnlyMemory<T> memory)
    {
        MemorySegment<T> segment = new(memory)
        {
            RunningIndex = RunningIndex + Memory.Length
        };

        Next = segment;

        return segment;
    }
}