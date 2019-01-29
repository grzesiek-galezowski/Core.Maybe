using System;

namespace Functional.Maybe.Either
{
    /// <summary>
    /// A functional monadic concept Either to make validation code more expressive and easier to maintain.
    /// <typeparam name="TL">Type of "Left" item</typeparam>
    /// <typeparam name="TR">Type of "Right" item</typeparam>
    /// </summary>
    public class Either<TL, TR>
    {
        private readonly TL _left;
        private readonly TR _right;
        private readonly bool _isLeft;

        /// <summary>
        /// Constructs new <see cref="Either{TL, TR}"/> with left part defined
        /// </summary>
        public Either(TL left)
        {
            _left = left;
            _isLeft = true;
        }

        /// <summary>
        /// Constructs new <see cref="Either{TL, TR}"/> with right part defined
        /// </summary>
        /// <param name="right"></param>
        public Either(TR right)
        {
            _right = right;
            _isLeft = false;
        }

        private Either(TL left, TR right, bool isLeft)
        {
            _isLeft = isLeft;

            if (isLeft)
            {
                _left = left;
            }
            else
            {
                _right = right;
            }
        }

        /// <summary>
        ///  Constructs new <see cref="Either{TL, TR}"/> with left part defined.
        ///  Suitable for those Either instances, where TL and TR are same.
        /// </summary>
        public static Either<TL, TR> CreateLeft(TL left)
        {
            return new Either<TL, TR>(left, default, true);
        }

        /// <summary>
        ///  Constructs new <see cref="Either{TL, TR}"/> with right part defined.
        ///  Suitable for those Either instances, where TL and TR are same.
        /// </summary>
        public static Either<TL, TR> CreateRight(TR right)
        {
            return new Either<TL, TR>(default, right, false);
        }

        /// <summary>
        /// Executes left or right function depending on the Either state.
        /// </summary>
        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc)
        {
            if (leftFunc == null)
            {
                throw new ArgumentNullException(nameof(leftFunc));
            }

            if (rightFunc == null)
            {
                throw new ArgumentNullException(nameof(rightFunc));
            }

            return _isLeft ? leftFunc(_left) : rightFunc(_right);
        }

        /// <summary>
        /// Executes left or right function depending on the Either state.
        /// </summary>
        public T Match<T>(Func<T> leftFunc, Func<T> rightFunc)
        {
            if (leftFunc == null)
            {
                throw new ArgumentNullException(nameof(leftFunc));
            }

            if (rightFunc == null)
            {
                throw new ArgumentNullException(nameof(rightFunc));
            }

            return _isLeft ? leftFunc() : rightFunc();
        }

        /// <summary>
        /// Executes left or right action depending on the Either state.
        /// </summary>
        public void Match(Action<TL> leftAction, Action<TR> rightAction)
        {
            if (leftAction == null)
            {
                throw new ArgumentNullException(nameof(leftAction));
            }

            if (rightAction == null)
            {
                throw new ArgumentNullException(nameof(rightAction));
            }

            if (_isLeft)
            {
                leftAction(_left);
            }
            else
            {
                rightAction(_right);
            }
        }

        /// <summary>
        /// Executes left or right action depending on the Either state.
        /// </summary>
        public void Match(Action leftAction, Action rightAction)
        {
            if (leftAction == null)
            {
                throw new ArgumentNullException(nameof(leftAction));
            }

            if (rightAction == null)
            {
                throw new ArgumentNullException(nameof(rightAction));
            }

            if (_isLeft)
            {
                leftAction();
            }
            else
            {
                rightAction();
            }
        }

        public TL LeftOrDefault() => Match(l => l, r => default);
        public TR RightOrDefault() => Match(l => default, r => r);
        public TL LeftOrDefault(TL value) => Match(l => l, r => value);
        public TR RightOrDefault(TR value) => Match(l => value, r => r);

        public static implicit operator Either<TL, TR>(TL left) => new Either<TL, TR>(left);
        public static implicit operator Either<TL, TR>(TR right) => new Either<TL, TR>(right);
    }

    public static class EitherExtensions
    {
        public static Either<TL, TR> ToEitherLeft<TL, TR>(this TL left) => new Either<TL, TR>(left);
        public static Either<TL, TR> ToEitherRight<TL, TR>(this TR right) => new Either<TL, TR>(right);
    }
}
