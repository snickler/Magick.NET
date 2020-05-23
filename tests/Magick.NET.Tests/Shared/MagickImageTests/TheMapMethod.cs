﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheMapMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        image.Map((IMagickImage<QuantumType>)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColorsIsNull()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("colors", () =>
                    {
                        image.Map((IEnumerable<MagickColor>)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColorsIsEmpty()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<ArgumentException>("colors", () =>
                    {
                        image.Map(Enumerable.Empty<MagickColor>());
                    });
                }
            }

            [TestMethod]
            public void ShouldUseTheColorsOfTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var colors = CreatePalleteImage())
                    {
                        image.Map(colors);

                        ColorAssert.AreEqual(MagickColors.Blue, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Green, image, 455, 396);
                        ColorAssert.AreEqual(MagickColors.Red, image, 505, 451);
                    }
                }
            }

            [TestMethod]
            public void ShouldUseTheColors()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var colors = new List<MagickColor>
                    {
                        MagickColors.Gold,
                        MagickColors.Lime,
                        MagickColors.Fuchsia,
                    };

                    image.Map(colors);

                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Lime, image, 455, 396);
                    ColorAssert.AreEqual(MagickColors.Gold, image, 505, 451);
                }
            }

            private IMagickImage<QuantumType> CreatePalleteImage()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(new MagickImage(MagickColors.Red, 1, 1));
                    images.Add(new MagickImage(MagickColors.Blue, 1, 1));
                    images.Add(new MagickImage(MagickColors.Green, 1, 1));

                    return images.AppendHorizontally();
                }
            }
        }
    }
}
