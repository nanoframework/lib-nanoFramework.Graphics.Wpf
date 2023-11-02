﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable enable
namespace System.Drawing
{
    // TODO: Add IEquatable<Size> once generics are supported as the class already implements the contract

    /// <summary>
    /// Represents the size of a rectangular region with an ordered pair of width and height.
    /// </summary>
    [Serializable]
    public struct Size
    {
        /// <summary>
        /// Initializes a new instance of the <see cref='Size'/> class.
        /// </summary>
        public static readonly Size Empty;

        // Do not rename (binary serialization)
        private int width;
        // Do not rename (binary serialization)
        private int height; 

        /// <summary>
        /// Initializes a new instance of the <see cref='Size'/> class from the specified
        /// <see cref='System.Drawing.Point'/>.
        /// </summary>
        public Size(Point pt)
        {
            width = pt.X;
            height = pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='Size'/> class from the specified dimensions.
        /// </summary>
        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /* Commenting out the SizeF methods for now
        /// <summary>
        /// Converts the specified <see cref='Size'/> to a <see cref='SizeF'/>.
        /// </summary>
        public static implicit operator SizeF(Size p) => new SizeF(p.Width, p.Height);
        */

        /// <summary>
        /// Performs vector addition of two <see cref='Size'/> objects.
        /// </summary>
        public static Size operator +(Size sz1, Size sz2) => Add(sz1, sz2);

        /// <summary>
        /// Contracts a <see cref='Size'/> by another <see cref='Size'/>
        /// </summary>
        public static Size operator -(Size sz1, Size sz2) => Subtract(sz1, sz2);

        /// <summary>
        /// Multiplies a <see cref="Size"/> by an <see cref="int"/> producing <see cref="Size"/>.
        /// </summary>
        /// <param name="left">Multiplier of type <see cref="int"/>.</param>
        /// <param name="right">Multiplicand of type <see cref="Size"/>.</param>
        /// <returns>Product of type <see cref="Size"/>.</returns>
        public static Size operator *(int left, Size right) => Multiply(right, left);

        /// <summary>
        /// Multiplies <see cref="Size"/> by an <see cref="int"/> producing <see cref="Size"/>.
        /// </summary>
        /// <param name="left">Multiplicand of type <see cref="Size"/>.</param>
        /// <param name="right">Multiplier of type <see cref="int"/>.</param>
        /// <returns>Product of type <see cref="Size"/>.</returns>
        public static Size operator *(Size left, int right) => Multiply(left, right);

        /// <summary>
        /// Divides <see cref="Size"/> by an <see cref="int"/> producing <see cref="Size"/>.
        /// </summary>
        /// <param name="left">Dividend of type <see cref="Size"/>.</param>
        /// <param name="right">Divisor of type <see cref="int"/>.</param>
        /// <returns>Result of type <see cref="Size"/>.</returns>
        public static Size operator /(Size left, int right) => new(unchecked(left.width / right), unchecked(left.height / right));

        /* TODO: Uncomment if SizeF is copied over
        /// <summary>
        /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="SizeF"/>.
        /// </summary>
        /// <param name="left">Multiplier of type <see cref="float"/>.</param>
        /// <param name="right">Multiplicand of type <see cref="Size"/>.</param>
        /// <returns>Product of type <see cref="SizeF"/>.</returns>
        public static SizeF operator *(float left, Size right) => Multiply(right, left);

        /// <summary>
        /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="SizeF"/>.
        /// </summary>
        /// <param name="left">Multiplicand of type <see cref="Size"/>.</param>
        /// <param name="right">Multiplier of type <see cref="float"/>.</param>
        /// <returns>Product of type <see cref="SizeF"/>.</returns>
        public static SizeF operator *(Size left, float right) => Multiply(left, right);

        /// <summary>
        /// Divides <see cref="Size"/> by a <see cref="float"/> producing <see cref="SizeF"/>.
        /// </summary>
        /// <param name="left">Dividend of type <see cref="Size"/>.</param>
        /// <param name="right">Divisor of type <see cref="int"/>.</param>
        /// <returns>Result of type <see cref="SizeF"/>.</returns>
        public static SizeF operator /(Size left, float right)
            => new SizeF(left.width / right, left.height / right);
        */

        /// <summary>
        /// Tests whether two <see cref='Size'/> objects are identical.
        /// </summary>
        public static bool operator ==(Size sz1, Size sz2) => sz1.Width == sz2.Width && sz1.Height == sz2.Height;

        /// <summary>
        /// Tests whether two <see cref='Size'/> objects are different.
        /// </summary>
        public static bool operator !=(Size sz1, Size sz2) => !(sz1 == sz2);

        /// <summary>
        /// Converts the specified <see cref='Size'/> to a <see cref='System.Drawing.Point'/>.
        /// </summary>
        public static explicit operator Point(Size size) => new(size.Width, size.Height);

        /// <summary>
        /// Tests whether this <see cref='Size'/> has zero width and height.
        /// </summary>
        public readonly bool IsEmpty => width == 0 && height == 0;

        /// <summary>
        /// Represents the horizontal component of this <see cref='Size'/>.
        /// </summary>
        public int Width
        {
            readonly get => width;
            set => width = value;
        }

        /// <summary>
        /// Represents the vertical component of this <see cref='Size'/>.
        /// </summary>
        public int Height
        {
            readonly get => height;
            set => height = value;
        }

        /// <summary>
        /// Performs vector addition of two <see cref='Size'/> objects.
        /// </summary>
        public static Size Add(Size sz1, Size sz2) =>
            new(unchecked(sz1.Width + sz2.Width), unchecked(sz1.Height + sz2.Height));

        /* TODO: Uncomment if SizeF is copied over
        /// <summary>
        /// Converts a SizeF to a Size by performing a ceiling operation on all the coordinates.
        /// </summary>
        public static Size Ceiling(SizeF value) =>
            new Size(unchecked((int)Math.Ceiling(value.Width)), unchecked((int)Math.Ceiling(value.Height)));
        */

        /// <summary>
        /// Contracts a <see cref='Size'/> by another <see cref='Size'/> .
        /// </summary>
        public static Size Subtract(Size sz1, Size sz2) =>
            new(unchecked(sz1.Width - sz2.Width), unchecked(sz1.Height - sz2.Height));

        /* TODO: Uncomment if SizeF is copied over
        /// <summary>
        /// Converts a SizeF to a Size by performing a truncate operation on all the coordinates.
        /// </summary>
        public static Size Truncate(SizeF value) => new Size(unchecked((int)value.Width), unchecked((int)value.Height));

        /// <summary>
        /// Converts a SizeF to a Size by performing a round operation on all the coordinates.
        /// </summary>
        public static Size Round(SizeF value) =>
            new Size(unchecked((int)Math.Round(value.Width)), unchecked((int)Math.Round(value.Height)));
        */

        /// <summary>
        /// Tests to see whether the specified object is a <see cref='Size'/>  with the same dimensions
        /// as this <see cref='Size'/>.
        /// </summary>
        public readonly override bool Equals(object? other) => other is Size size && Equals(size);

        /// <summary>
        /// Tests to see whether the specified object is a <see cref='Size'/>  with the same dimensions
        /// as this <see cref='Size'/>.
        /// </summary>
        public readonly bool Equals(Size other) => this == other;

        /// <summary>
        /// Returns a hash code.
        /// </summary>
        public readonly override int GetHashCode()
        {
            // Original: HashCode.Combine(Width, Height);

            unchecked
            {
                return Width.GetHashCode() ^ Height.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref='Size'/>.
        /// </summary>
        public readonly override string ToString() => $"{{Width={width}, Height={height}}}";

        /// <summary>
        /// Multiplies <see cref="Size"/> by an <see cref="int"/> producing <see cref="Size"/>.
        /// </summary>
        /// <param name="size">Multiplicand of type <see cref="Size"/>.</param>
        /// <param name="multiplier">Multiplier of type <see cref='int'/>.</param>
        /// <returns>Product of type <see cref="Size"/>.</returns>
        private static Size Multiply(Size size, int multiplier) =>
            new(unchecked(size.width * multiplier), unchecked(size.height * multiplier));

        /* TODO: Uncomment if SizeF is copied over
        /// <summary>
        /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="SizeF"/>.
        /// </summary>
        /// <param name="size">Multiplicand of type <see cref="Size"/>.</param>
        /// <param name="multiplier">Multiplier of type <see cref="float"/>.</param>
        /// <returns>Product of type SizeF.</returns>
        private static SizeF Multiply(Size size, float multiplier) =>
            new SizeF(size.width * multiplier, size.height * multiplier);
        */
    }
}
