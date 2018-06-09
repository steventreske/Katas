using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Katas
{
    public interface IFizzBuzz
    {
        string Get();
        bool AppliedTo(int value);
    }

    public class Buzz : IFizzBuzz
    {
        public string Get()
        {
            return "Buzz";
        }

        public bool AppliedTo(int value)
        {
            return value % 5 == 0;
        }
    }

    public class Fizz : IFizzBuzz
    {
        public string Get()
        {
            return "Fizz";
        }

        public bool AppliedTo(int value)
        {
            return value % 3 == 0;
        }
    }

    public class FizzBuzz : IFizzBuzz
    {
        public string Get()
        {
            return "FizzBuzzCalculator";
        }

        public bool AppliedTo(int value)
        {
            return value % 3 == 0 && value % 5 == 0;
        }
    }

    public interface IDefaultFizzBuzz
    {
        string Get(int value);
    }

    public class Default : IDefaultFizzBuzz
    {
        public string Get(int value)
        {
            return value.ToString();
        }
    }

    public class FizzBuzzCalculator
    {
        private readonly IDefaultFizzBuzz _default;
        private readonly IEnumerable<IFizzBuzz> _fizzBuzzRules;

        public FizzBuzzCalculator(IEnumerable<IFizzBuzz> fizzBuzzRules, IDefaultFizzBuzz @default)
        {
            _fizzBuzzRules = fizzBuzzRules;
            _default = @default;
        }

        public string Get(int value)
        {
            var fizzBuzzRule = _fizzBuzzRules.FirstOrDefault(fbr => fbr.AppliedTo(value));
            return fizzBuzzRule == null 
                ? _default.Get(value) 
                : fizzBuzzRule.Get();
        }
    }

    public class FizzBuzzTest
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "Fizz")]
        [InlineData(4, "4")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(15, "FizzBuzzCalculator")]
        public void ShouldReturnCorrectValue(int inputValue, string expectedResult)
        {
            var sut = new FizzBuzzCalculator(new List<IFizzBuzz>
            {
                new FizzBuzz(),
                new Fizz(),
                new Buzz()
            }, new Default());
            var result = sut.Get(inputValue);
            result.Should().Be(expectedResult);
        }
    }
}
