﻿using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Core.Maybe.NTests
{
  public class MaybeDictionarySpecification
  {
    [Test]
    public void ShouldReturnNothingWhenLookingUpNullableValueDictionaryWithNonExistentKey()
    {
      var d = new Dictionary<string, string?>();

      Maybe<string> lookup = d.LookupObject("A");

      lookup.Should().Be(Maybe<string>.Nothing);
    }

    [Test]
    public void ShouldReturnNothingWhenLookingUpNullValueWithExistingKey()
    {
      var key = "A";
      var d = new Dictionary<string, string?>()
      {
        {key, null }
      };

      Maybe<string> lookup = d.LookupObject(key);

      lookup.Should().Be(Maybe<string>.Nothing);
    }
  }
}