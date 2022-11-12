﻿using System.Runtime.CompilerServices;

namespace LinkDotNet.StringBuilder;

public ref partial struct ValueStringBuilder
{
    /// <summary>
    /// Appends the string representation of the boolean to the builder.
    /// </summary>
    /// <param name="value">Bool value to add.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool value) => Append(value.ToString());

    /// <summary>
    /// Appends the string representation of the character to the builder.
    /// </summary>
    /// <param name="value">Integer to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the signed byte to the builder.
    /// </summary>
    /// <param name="value">Signed byte to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the byte to the builder.
    /// </summary>
    /// <param name="value">Byte to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the short to the builder.
    /// </summary>
    /// <param name="value">Short to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the integer to the builder.
    /// </summary>
    /// <param name="value">Integer to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the long integer to the builder.
    /// </summary>
    /// <param name="value">Long integer to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(long value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the float to the builder.
    /// </summary>
    /// <param name="value">Float to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(float value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the double to the builder.
    /// </summary>
    /// <param name="value">Double to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(double value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the decimal to the builder.
    /// </summary>
    /// <param name="value">Decimal to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(decimal value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends the string representation of the Guid to the builder.
    /// </summary>
    /// <param name="value">Guid to add.</param>
    /// <param name="format">Optional formatter. If not provided the default of the given instance is taken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(Guid value, ReadOnlySpan<char> format = default) => AppendSpanFormattable(value, format);

    /// <summary>
    /// Appends a string to the string builder.
    /// </summary>
    /// <param name="str">String, which will be added to this builder.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped ReadOnlySpan<char> str)
    {
        var newSize = str.Length + bufferPosition;
        if (newSize > buffer.Length)
        {
            Grow(newSize * 2);
        }

        str.CopyTo(buffer[bufferPosition..]);
        bufferPosition += str.Length;
    }

    /// <summary>
    /// Adds the default new line separator.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    /// <summary>
    /// Does the same as <see cref="Append(char)"/> but adds a newline at the end.
    /// </summary>
    /// <param name="str">String, which will be added to this builder.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(scoped ReadOnlySpan<char> str)
    {
        Append(str);
        Append(Environment.NewLine);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AppendSpanFormattable<T>(T value, ReadOnlySpan<char> format = default)
        where T : ISpanFormattable
    {
        Span<char> tempBuffer = stackalloc char[36];
        if (value.TryFormat(tempBuffer, out var written, format, null))
        {
            var newSize = written + bufferPosition;
            if (newSize >= buffer.Length)
            {
                Grow();
            }

            tempBuffer[..written].CopyTo(buffer[bufferPosition..]);
            bufferPosition = newSize;
        }
    }
}