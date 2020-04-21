namespace Utility.ResultModel
{
    /// <summary>
    /// Convenience counterpart to <see cref="Result{T,E}" />, allowing some methods to be called
    /// without specifying full type parameters.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Construct an <see cref="OkResult{T}"/>, representing a <see cref="Result{T,E}"/>
        /// that signifies a successful result.
        /// </summary>
        /// <param name="value">The success value to wrap inside.</param>
        /// <typeparam name="T">The type of the success value.</typeparam>
        /// <returns>The created <see cref="OkResult{T}"/>.</returns>
        public static OkResult<T> Ok<T>(T value)
        {
            return new OkResult<T>(value);
        }

        /// <summary>
        /// Construct an <see cref="ErrResult{E}"/>, representing a <see cref="Result{T,E}"/>
        /// that signifies a failed result.
        /// </summary>
        /// <param name="error">The error value to wrap inside.</param>
        /// <typeparam name="E">The type of the error value.</typeparam>
        /// <returns>The created <see cref="ErrResult{E}"/>.</returns>
        public static ErrResult<E> Err<E>(E error)
        {
            return new ErrResult<E>(error);
        }

        /// <summary>
        /// Specialised convenience implementation of <see cref="Ok{T}" /> for when the wrapped
        /// success value is an <see cref="Empty" />.
        /// </summary>
        /// <returns>An empty successful result.</returns>
        public static OkResult<Empty> Ok()
        {
            return Ok(new Empty());
        }

        /// <summary>
        /// Specialised convenience implementation of <see cref="Err{E}" /> for when the wrapped error
        /// value is an <see cref="Empty" />.
        /// </summary>
        /// <returns>An empty failed result.</returns>
        public static ErrResult<Empty> Err()
        {
            return Err(new Empty());
        }
    }
}
