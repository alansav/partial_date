using System;
using Xunit;

namespace Savage
{
    public class PartialDateTests
    {
        [Fact]
        public void InitialisesWithAllElements()
        {
            var sut = new PartialDate(2016, 3, 15);

            Assert.Equal((short)2016, sut.Year);
            Assert.Equal((byte)3, sut.Month);
            Assert.Equal((byte)15, sut.Day);
        }

        [Fact]
        public void ToDateTimeReturnsCorrectDateTime()
        {
            var partialDate = new PartialDate(2016, 3, 15);

            var dateTime = partialDate.ToDateTime();

            Assert.Equal(2016, dateTime.Year);
            Assert.Equal(3, dateTime.Month);
            Assert.Equal(15, dateTime.Day);
        }

        [Theory]
        [InlineData((short)2016, (byte)3, (byte)32)]
        [InlineData((short)2016, (byte)13, (byte)31)]
        [InlineData((short)10000, (byte)3, (byte)15)]
        [InlineData((short)2016, (byte)3, (byte)0)]
        [InlineData((short)2016, (byte)0, (byte)15)]
        [InlineData((short)0, (byte)3, (byte)15)]
        [InlineData(null, (byte)2, (byte)30)]
        [InlineData((short)2001, (byte)2, (byte)29)]
        [InlineData(null, null, null)]
        public void ThrowsExceptionWhenInitialisedWithInvalidValues(short? year, byte? month, byte? day)
        {
            Assert.Throws<ArgumentException>(() => new PartialDate(year, month, day));
        }

        [Fact]
        public void InitialisesCorrectlyForPotentialLeapYear()
        {
            var sut = new PartialDate(null, 2, 29);
            Assert.Null(sut.Year);
            Assert.Equal((byte)2, sut.Month);
            Assert.Equal((byte)29, sut.Day);
        }

        [Fact]
        public void EqualsOperatorReturnsTrueWhenAllElementsMatch()
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(2016, 3, 15);

            Assert.Equal(a, b);
        }

        [Theory]
        [InlineData((short)2000, (byte)3, (byte)15)]
        [InlineData((short)2016, (byte)4, (byte)15)]
        [InlineData((short)2016, (byte)3, (byte)16)]
        public void EqualsOperatorReturnsFalseWhenDifferent(short? year, byte? month, byte? day)
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(year, month, day);

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void EqualsOperatorReturnsFalseWhenNullArgumentProvided()
        {
            var a = new PartialDate(2016, 3, 15);

            Assert.False(a.Equals(null));
        }

        [Fact]
        public void EqualsOperatorReturnsFalseWhenArgumentIsDifferentType()
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new DateTime(2016, 3, 15);

            Assert.False(a.Equals(b));
        }

        [Fact]
        public void EqualsOperatorReturnsTrueWhenNotAllArgumentsArePresent()
        {
            var a = new PartialDate(2016, null, null);
            var b = new PartialDate(2016, null, null);

            Assert.True(a.Equals(b));
        }

        [Theory]
        [InlineData(null, (byte)3, (byte)15)]
        [InlineData((short)2016, null, (byte)15)]
        [InlineData((short)2016, (byte)3, null)]
        public void IsCompleteDateReturnsFalse(short? year, byte? month, byte? day)
        {
            var sut = new PartialDate(year, month, day);
            Assert.False(sut.IsCompleteDate);
        }

        [Fact]
        public void IsCompleteReturnsTrueWhenComplete()
        {
            var sut = new PartialDate(2016, 3, 15);
            Assert.True(sut.IsCompleteDate);
        }

        [Fact]
        public void ThrowsExceptionCallingToDateTimeOnPartialDate()
        {
            var sut = new PartialDate(2016, null, 15);
            Assert.Throws<InvalidOperationException>(() => sut.ToDateTime());
        }

        [Fact]
        public void GetHashCodeReturnsSameValueWhenDatesMatch()
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(2016, 3, 15);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Theory]
        [InlineData((short)2015, (byte)3, (byte)15)]
        [InlineData((short)2015, (byte)4, (byte)15)]
        [InlineData((short)2016, (byte)3, (byte)16)]
        public void GetHashCodeReturnsDifferentValues(short? year, byte? month, byte? day)
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(year, month, day);

            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }
    }
}
