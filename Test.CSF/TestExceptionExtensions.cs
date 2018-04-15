//
// TestExceptionExtensions.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2015 CSF Software Limited
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using NUnit.Framework;
using CSF;
using System.Reflection;
using System.Runtime.Serialization;

namespace Test.CSF
{
  [TestFixture]
  public class TestExceptionExtensions
  {
    #region tests

    [Test]
    [ExpectedException(typeof(TargetInvocationException))]
    public void TestFixStackTraceUsingSerialization()
    {
      try
      {
        InvalidOperationException fixedException = null;
        try
        {
          throw new InvalidOperationException("Oh nose!");
        }
        catch(InvalidOperationException ex)
        {
#pragma warning disable CS0618 // Type or member is obsolete
          fixedException = ExceptionExtensions.FixStackTraceUsingSerialization(ex); ;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        if(fixedException != null)
        {
          throw new TargetInvocationException(fixedException);
        }
      }
      catch(TargetInvocationException ex)
      {
        Assert.IsInstanceOf<InvalidOperationException>(ex.InnerException, "Inner exception");
        throw;
      }
    }

    [Test]
    public void TestFixStackTraceUsingSerializationCustomException()
    {
      CustomException fixedException = null;

      try
      {
        throw new CustomException();
      }
      catch(CustomException ex)
      {
#pragma warning disable CS0618 // Type or member is obsolete
        fixedException = ExceptionExtensions.FixStackTraceUsingSerialization(ex); ;
#pragma warning restore CS0618 // Type or member is obsolete
      }

      Assert.IsNull(fixedException);
    }

    [Test]
    [ExpectedException(typeof(TargetInvocationException))]
    public void TestFixStackTrace()
    {
      try
      {
        InvalidOperationException fixedException = null;
        try
        {
          throw new InvalidOperationException("Oh nose!");
        }
        catch(InvalidOperationException ex)
        {
#pragma warning disable CS0618 // Type or member is obsolete
          fixedException = ex.FixStackTrace();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        if(fixedException != null)
        {
          throw new TargetInvocationException(fixedException);
        }
      }
      catch(TargetInvocationException ex)
      {
        Assert.IsInstanceOf<InvalidOperationException>(ex.InnerException, "Inner exception");
        throw;
      }
    }

    [Test]
    [ExpectedException(typeof(TargetInvocationException))]
    public void TestFixStackTraceCustomExceptionWithConstructor()
    {
      try
      {
        CustomSerializableException fixedException = null;
        try
        {
          throw new CustomSerializableException();
        }
        catch(CustomSerializableException ex)
        {
#pragma warning disable CS0618 // Type or member is obsolete
          fixedException = ex.FixStackTrace();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        if(fixedException != null)
        {
          throw new TargetInvocationException(fixedException);
        }
      }
      catch(TargetInvocationException ex)
      {
        Assert.IsInstanceOf<CustomSerializableException>(ex.InnerException, "Inner exception");
        throw;
      }
    }

    [Test]
    [ExpectedException(typeof(TargetInvocationException))]
    public void TestTryFixStackTrace()
    {
      bool success = false;

      try
      {
        CustomSerializableException fixedException = null;
        try
        {
          throw new CustomSerializableException();
        }
        catch(CustomSerializableException ex)
        {
#pragma warning disable CS0618 // Type or member is obsolete
          success = ex.TryFixStackTrace(out fixedException);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        if(fixedException != null)
        {
          throw new TargetInvocationException(fixedException);
        }
      }
      catch(TargetInvocationException ex)
      {
        Assert.IsInstanceOf<CustomSerializableException>(ex.InnerException, "Inner exception");
        Assert.IsTrue(success, "Success of fix");
        throw;
      }
    }

    [Test]
    public void TestTryFixStackTraceFailure()
    {
      if(!Util.ReflectionHelper.IsMono())
      {
        Assert.Ignore("This test is not valid when not running on the open source Mono framework.  When executing " +
                      "against the official .NET framework, the 'private framework method' implementation for fixing " +
                      "stack traces will work and thus 'TryFixStackTrace' will not fail as intended.");
      }

      bool success = false;
      CustomException fixedException = null;

      try
      {
        throw new CustomException();
      }
      catch(CustomException ex)
      {
        success = ex.TryFixStackTrace(out fixedException);
      }

      Assert.IsFalse(success);
    }

    #endregion

    #region contained custom exception

    private class CustomException : Exception {}

    private class CustomSerializableException : Exception
    {
      public CustomSerializableException() {}

      public CustomSerializableException(SerializationInfo ser, StreamingContext con) : base(ser, con) {}
    }

    #endregion
  }
}

