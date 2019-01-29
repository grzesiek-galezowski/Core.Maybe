using Functional.Maybe.Either;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Functional.Maybe.Tests
{
    [TestClass]
    public class EitherTests
    {
        private readonly Either<int, string> _eitherLeft;
        private readonly Either<int, string> _eitherRight;
        private const int _eitherLeftValue = 5;
        private const string _eitherRightValue = "Five";

        public EitherTests()
        {
            _eitherLeft = new Either<int, string>(_eitherLeftValue);
            _eitherRight = new Either<int, string>(_eitherRightValue);
        }

        [TestMethod]
        public void NullCheckingTests()
        {
            Action<int> nullActionInt = null;
            Action<int> mockActionInt = x => { var y = 5; };

            Action<string> nullActionString = null;
            Action<string> mockActionString = x => { var y = 5; };

            Action nullAction = null;
            Action mockAction = () => { var a = 1; };

            AssertExtension.Throws<ArgumentNullException>(() => _eitherLeft.Match(nullAction, mockAction));
            AssertExtension.Throws<ArgumentNullException>(() => _eitherLeft.Match(mockAction, nullAction));
            _eitherLeft.Match(mockAction, mockAction);

            AssertExtension.Throws<ArgumentNullException>(() => _eitherRight.Match(nullAction, mockAction));
            AssertExtension.Throws<ArgumentNullException>(() => _eitherRight.Match(mockAction, nullAction));
            _eitherRight.Match(mockAction, mockAction);

            AssertExtension.Throws<ArgumentNullException>(() => _eitherRight.Match(nullActionInt, mockActionString));
            AssertExtension.Throws<ArgumentNullException>(() => _eitherRight.Match(mockActionInt, nullActionString));
            _eitherLeft.Match(mockActionInt, mockActionString);
        }

        [TestMethod]
        public void MatchActionTests()
        {
            var bool1 = false;
            var bool2 = false;
            var testInt = 0;
            var testString = null as string;

            Action resetTestValues = () =>
            {
                bool1 = false;
                bool2 = false;
                testInt = 0;
                testString = null;
            };

            Action setBool1Action = () => bool1 = true;
            Action setBool2Action = () => bool2 = true;

            Action<int> setTestInt = value => testInt = value;
            Action<string> setTestString = value => testString = value;

            _eitherLeft.Match(setBool1Action, setBool2Action);
            Assert.IsTrue(bool1);
            Assert.IsFalse(bool2);

            resetTestValues();
            _eitherRight.Match(setBool1Action, setBool2Action);
            Assert.IsFalse(bool1);
            Assert.IsTrue(bool2);

            resetTestValues();
            _eitherLeft.Match(setTestInt, setTestString);
            Assert.AreEqual(_eitherLeftValue, testInt);
            Assert.AreEqual(null, testString);

            resetTestValues();
            _eitherRight.Match(setTestInt, setTestString);
            Assert.AreEqual(0, testInt);
            Assert.AreEqual(_eitherRightValue, testString);

            resetTestValues();
        }

        [TestMethod]
        public void MatchFunctionTests()
        {
            var testInt = 0;
            var testString = null as string;

            Action resetTestValues = () =>
            {
                testInt = 0;
                testString = null;
            };

            Func<int, bool> funcTLT = x =>
            {
                testInt = x;
                return true;
            };

            Func<string, bool> funcTRT = x =>
            {
                testString = x;
                return false;
            };

            Assert.IsTrue(_eitherLeft.Match(funcTLT, funcTRT));
            Assert.AreEqual(_eitherLeftValue, testInt);
            Assert.AreEqual(null, testString);

            resetTestValues();
            Assert.IsFalse(_eitherRight.Match(funcTLT, funcTRT));
            Assert.AreEqual(_eitherRightValue, testString);
            Assert.AreEqual(0, testInt);

            resetTestValues();
            Assert.IsTrue(_eitherLeft.Match(() => true, () => false));
            Assert.IsFalse(_eitherRight.Match(() => true, () => false));
        }

        [TestMethod]
        public void LeftOrDefaultAndRightOrDefaultTests()
        {
            Assert.AreEqual(_eitherLeftValue, _eitherLeft.LeftOrDefault());
            Assert.AreEqual(_eitherRightValue, _eitherRight.RightOrDefault());

            Assert.AreEqual(default, _eitherRight.LeftOrDefault());
            Assert.AreEqual(default, _eitherLeft.RightOrDefault());

            Assert.AreEqual(29, _eitherRight.LeftOrDefault(29));
            Assert.AreEqual("Twenty nine", _eitherLeft.RightOrDefault("Twenty nine"));
        }

        [TestMethod]
        public void SameTLTRTests()
        {
            var eitherLeft = Either<string, string>.CreateLeft("Left defined");
            var eitherRight = Either<string, string>.CreateRight("Right defined");

            Assert.AreEqual("Left defined", eitherLeft.LeftOrDefault());
            Assert.AreEqual("Right defined", eitherRight.RightOrDefault());

            Assert.AreEqual(null, eitherRight.LeftOrDefault());
            Assert.AreEqual(null, eitherLeft.RightOrDefault());
        }

        [TestMethod]
        public void ExtensionMethodTests()
        {
            var eitherLeft = 29.ToEitherLeft<int, string>();
            var eitherRight = "Twenty nine".ToEitherRight<int, string>();

            Assert.AreEqual(29, eitherLeft.LeftOrDefault());
            Assert.AreEqual(null, eitherLeft.RightOrDefault());

            Assert.AreEqual("Twenty nine", eitherRight.RightOrDefault());
            Assert.AreEqual(0, eitherRight.LeftOrDefault());
        }
    }


    class AssertExtension
    {
        public static void Throws<T>(Action action) where T : Exception
        {
            var exceptionThrown = false;
            try
            {
                action.Invoke();
            }
            catch (T)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
            {
                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but not thrown", typeof(T))
                    );
            }
        }
    }
}
