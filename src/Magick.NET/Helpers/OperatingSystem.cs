// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal static partial class OperatingSystem
    {
        public static bool Is64Bit =>
            IntPtr.Size == 8;
        public static bool IsArm64 =>
            System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture == System.Runtime.InteropServices.Architecture.Arm64;
    }
}
