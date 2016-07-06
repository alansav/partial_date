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

            Assert.Equal(2016, sut.Year);
            Assert.Equal(3, sut.Month);
            Assert.Equal(15, sut.Day);
        }

        [Fact]
        public void ToDateTimeReturnsCorrectDateTime()
        {
            var partialDate = new PartialDate(2016, 3, 15);

            var dateTime = partialDate.ToDateTime();

            Assert.Equal(partialDate.Year, dateTime.Year);
            Assert.Equal(partialDate.Month, dateTime.Month);
            Assert.Equal(partialDate.Day, dateTime.Day);
        }

        [Theory]
        [InlineData(2016, 3, 32)]
        [InlineData(2016, 13, 31)]
        [InlineData(10000, 3, 15)]
        [InlineData(2016, 3, 0)]
        [InlineData(2016, 0, 15)]
        [InlineData(0, 3, 15)]
        [InlineData(null, 2, 30)]
        [InlineData(2001, 2, 29)]
        [InlineData(null, null, null)]
        public void ThrowsExceptionWhenInitialisedWithInvalidValues(int? year, int? month, int? day)
        {
            Assert.Throws<ArgumentException>(() => new PartialDate(year, month, day));
        }

        [Fact]
        public void InitialisesCorrectlyForPotentialLeapYear()
        {
            var sut = new PartialDate(null, 2, 29);
            Assert.Equal(null, sut.Year);
            Assert.Equal(2, sut.Month);
            Assert.Equal(29, sut.Day);
        }

        [Fact]
        public void EqualsOperatorReturnsTrueWhenAllElementsMatch()
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(2016, 3, 15);

            Assert.Equal(a, b);
        }

        [Theory]
        [InlineData(2000, 3, 15)]
        [InlineData(2016, 4, 15)]
        [InlineData(2016, 3, 16)]
        public void EqualsOperatorReturnsFalseWhenDifferent(int? year, int? month, int? day)
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
        [InlineData(null, 3, 15)]
        [InlineData(2016, null, 15)]
        [InlineData(2016, 3, null)]
        public void IsCompleteDateReturnsFalse(int? year, int? month, int? day)
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
        [InlineData(2015, 3, 15)]
        [InlineData(2015, 4, 15)]
        [InlineData(2016, 3, 16)]
        public void GetHashCodeReturnsDifferentValues(int? year, int? month, int? day)
        {
            var a = new PartialDate(2016, 3, 15);
            var b = new PartialDate(year, month, day);

            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }
    }
}
