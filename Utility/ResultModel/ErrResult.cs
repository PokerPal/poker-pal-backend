using System;

namespace Utility.ResultModel
{
    /// <summary>
    /// Represents a <see cref="Result{T,E}"/> that signifies a failed operation.
    /// </summary>
    /// <typeparam name="E">
    /// The type of the error wrapped inside the <see cref="Result{T,E}"/>.
    /// </typeparam>
    public sealed class ErrResult<E>
    {
        internal ErrResult(E error)
        {
            this.Error = error;
        }

        internal E Error { get; }

        /// <summary>
        /// Converts a result of type <c>ErrResult{E}</c> to a result of a new type
        /// <c>ErrResult{F}</c>. This works by converting the wrapped <c>E</c> error value to an
        /// <c>F</c> using the provided function <c>mapping()</c>.
        /// </summary>
        /// <param name="mapping">The conversion function from an <c>E</c> to an <c>F</c>.</param>
        /// <typeparam name="F">The error type of the new result.</typeparam>
        /// <returns>The new result.</returns>
        public ErrResult<F> MapErr<F>(Func<E, F> mapping)
        {
            return Result.Err(mapping(this.Error));
        }

        /// <summary>
        /// Perform an action involving the error value inside this result.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The unchanged result.</returns>
        public ErrResult<E> OnErr(Action<E> action)
        {
            return this.MapErr(e =>
            {
                action(e);
                return e;
            });
        }
    }
}
