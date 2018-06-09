using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Katas
{
    public class CheckProject
    {
        [Fact]
        public void ShouldWork()
        {
            true.Should().BeTrue();
        }
    }
}
