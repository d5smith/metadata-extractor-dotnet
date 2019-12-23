// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.QuickTime;
using Xunit;

namespace MetadataExtractor.Tests.Formats.Mov
{
    /// <summary>Unit tests for <see cref="QuickTimeMetadataReader"/>.</summary>
    public sealed class MovMetadataReaderTest
    {

        [Fact]
        public void ExtractMetadataUsingStream()
        {
            using var stream = TestDataUtil.OpenRead("Data/with-gps.mov");
            Validate(QuickTimeMetadataReader.ReadMetadata(stream));
        }

        private static void Validate(IEnumerable<Directory> metadata)
        {
            var directory = metadata.OfType<QuickTimeMetadataHeaderDirectory>().FirstOrDefault();

            var gpsTag = directory.Tags.Where(t => t.Name.Equals("GPS Location")).FirstOrDefault();
            Assert.NotNull(gpsTag);
            Assert.Equal("+32.0929+034.8620+028.516/", gpsTag.Description);

            gpsTag = directory.Tags.Where(t => t.Type.Equals(QuickTimeMetadataHeaderDirectory.TagGpsLocation)).FirstOrDefault();
            Assert.NotNull(gpsTag);
            Assert.Equal("+32.0929+034.8620+028.516/", gpsTag.Description);

            var creationDateTag = directory.Tags.Where(t => t.Name.Equals("Creationdate")).FirstOrDefault();
            Assert.NotNull(creationDateTag);
            Assert.Equal("2019-07-24T11:25:40+0300", creationDateTag.Description);

            creationDateTag = directory.Tags.Where(t => t.Type.Equals(QuickTimeMetadataHeaderDirectory.TagCreationDate)).FirstOrDefault();
            Assert.NotNull(creationDateTag);
            Assert.Equal("2019-07-24T11:25:40+0300", creationDateTag.Description);

            var softwareTag = directory.Tags.Where(t => t.Name.Equals("Software")).FirstOrDefault();
            Assert.NotNull(softwareTag);
            Assert.Equal("12.3", softwareTag.Description);

            softwareTag = directory.Tags.Where(t => t.Type.Equals(QuickTimeMetadataHeaderDirectory.TagSoftware)).FirstOrDefault();
            Assert.NotNull(softwareTag);
            Assert.Equal("12.3", softwareTag.Description);
        }
    }
}
