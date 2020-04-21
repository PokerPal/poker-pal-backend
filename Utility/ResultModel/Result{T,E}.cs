using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Utility.ResultModel
{
    /// <summary>
    /// Represents the result of an operation that could fail.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by a successful operation.</typeparam>
    /// <typeparam name="E">The type of the error occurring if the operation fails.</typeparam>
    public sealed class Result<T, E>
    {
        /// <summary>
        /// The successful result value, if <c>IsOk == true</c>, otherwise <c>default</c>.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// The error value, if <c>IsOk == false</c>, otherwise <c>default</c>.
        /// </summary>
        public E Error { get; private set; }

        /// <summary>
        /// Gets whether the operation completed successfully.
        /// </summary>
        public bool IsOk { get; private set; }

        /// <summary>
        /// Implicitly converts a value of type <c>T</c> to a <c>Result{T,E}</c> representing a
        /// success.
        /// </summary>
        /// <param name="okValue">The value to wrap inside the successful result.</param>
        /// <returns>The created result wrapping <c>okValue</c>.</returns>
        public static implicit operator Result<T, E>(T okValue)
        {
            return Ok(okValue);
        }

        /// <summary>
        /// Implicitly converts a value of type <c>E</c> to a <c>Result{T,E}</c> representing a
        /// failure.
        /// </summary>
        /// <param name="errValue">The value to wrap inside the failed result.</param>
        /// <returns>The created result wrapping <c>errValue</c>.</returns>
        public static implicit operator Result<T, E>(E errValue)
        {
            return Err(errValue);
        }

        /// <summary>
        /// Implicitly converts an <see cref="OkResult{T}"/> to a <see cref="Result{T,E}"/>.
        /// </summary>
        /// <param name="okResult">The value to convert.</param>
        /// <returns>The converted result.</returns>
        public static implicit operator Result<T, E>(OkResult<T> okResult)
        {
            return Ok(okResult.Value);
        }

        /// <summary>
        /// Implicitly converts an <see cref="ErrResult{E}"/> to a <see cref="Result{T,E}"/>.
        /// </summary>
        /// <param name="errResult">The value to convert.</param>
        /// <returns>The converted result.</returns>
        public static implicit operator Result<T, E>(ErrResult<E> errResult)
        {
            return Err(errResult.Error);
        }

        /// <summary>
        /// Creates an instance of <see cref="Result{T,E}" /> representing a successful operation.
        /// </summary>
        /// <param name="value">The success value.</param>
        /// <returns>The successful result.</returns>
        public static Result<T, E> Ok(T value)
        {
            return new Result<T, E>
            {
                IsOk = true,
                Value = value,
                Error = default,
            };
        }

        /// <summary>
        /// Creates an instance of <see cref="Result{T,E}" /> representing a failed operation.
        /// </summary>
        /// <param name="error">The error value representing the failure.</param>
        /// <returns>The failed result.</returns>
        public static Result<T, E> Err(E error)
        {
            return new Result<T, E>
            {
                IsOk = false,
                Value = default,
                Error = error,
            };
        }

        /// <summary>
        /// Creates a result from a value by checking if the value is <c>null</c>, and returning an
        /// empty error if so.
        /// </summary>
        /// <param name="value">The value to wrap inside the result if non-null.</param>
        /// <returns>The result.</returns>
        public static Result<T, Empty> FromNullable(T value)
        {
            return value == null ? Result<T, Empty>.Err(new Empty()) : Result<T, Empty>.Ok(value);
        }

        /// <summary>
        /// Creates a result from a value by checking if the value is <c>null</c>, and returning
        /// the provided error if so. If the error value is expensive to compute, consider using
        /// <see cref="FromNullableOrElse" /> instead.
        /// </summary>
        /// <param name="value">The value to wrap inside the result if non-null.</param>
        /// <param name="error">The error value to use if <paramref name="value"/> is <c>null</c>.
        /// </param>
        /// <returns>The result.</returns>
        public static Result<T, E> FromNullableOr(T value, E error)
        {
            return value == null ? Err(error) : Ok(value);
        }

        /// <summary>
        /// Creates a result from a value by checking if the value is <c>null</c>, and returning
        /// the result of the provided error function if so. This function, as opposed to
        /// <see cref="FromNullableOr" />, is useful when the error value is expensive to
        /// compute; the function <c>error</c> is only called in the case where the value is
        /// <c>null</c>.
        /// </summary>
        /// <param name="value">The value to wrap inside the result if non-null.</param>
        /// <param name="error">The function to compute the error value to use if
        /// <paramref name="value"/> is <c>null</c>.</param>
        /// <returns>The result.</returns>
        public static Result<T, E> FromNullableOrElse(T value, Func<E> error)
        {
            return value == null ? Err(error()) : Ok(value);
        }

        /// <summary>
        /// Converts a result of type <c>Result{T,E}</c> to a result of new type <c>Result{U,E}</c>.
        /// This works by converting the wrapped <c>T</c> value to a <c>U</c> using the provided
        /// function <c>mapping()</c>, if this result is a success. If this result is a failure,
        /// the error is copied to the new result.
        /// </summary>
        /// <param name="mapping">The conversion function from a <c>T</c> to a <c>U</c>.</param>
        /// <typeparam name="U">The success type of the new result.</typeparam>
        /// <returns>The new result.</returns>
        public Result<U, E> Map<U>(Func<T, U> mapping)
        {
            if (!this.IsOk)
            {
                return new Result<U, E>
                {
                    IsOk = false,
                    Value = default,
                    Error = this.Error,
                };
            }

            return Result.Ok(mapping(this.Value));
        }

        /// <summary>
        /// Perform an action involving the success value inside this result, if this result is
        /// successful.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The unchanged result.</returns>
        public Result<T, E> OnOk(Action<T> action)
        {
            return this.Map(v =>
            {
                action(v);
                return v;
            });
        }

        /// <summary>
        /// Converts a result of type <c>Result{T,E}</c> to a result of a new type
        /// <c>Result{T,F}</c>. This works by converting the wrapped <c>E</c> error value to an
        /// <c>F</c> using the provided function <c>mapping()</c>, if this result is a failure.
        /// If this result is a success, the success value is copied to the new result.
        /// </summary>
        /// <param name="mapping">The conversion function from an <c>E</c> to an <c>F</c>.</param>
        /// <typeparam name="F">The error type of the new result.</typeparam>
        /// <returns>The new result.</returns>
        public Result<T, F> MapErr<F>(Func<E, F> mapping)
        {
            if (this.IsOk)
            {
                return new Result<T, F>
                {
                    IsOk = true,
                    Value = this.Value,
                    Error = default,
                };
            }

            return Result.Err(mapping(this.Error));
        }

        /// <summary>
        /// Perform an action involving the error value inside this result, if this result is
        /// failed.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The unchanged result.</returns>
        public Result<T, E> OnErr(Action<E> action)
        {
            return this.MapErr(e =>
            {
                action(e);
                return e;
            });
        }

        /// <summary>
        /// If this result was a success, runs the next operation <c>next()</c> on the result value.
        /// Otherwise, passes on this result's error value. This is useful for control flow based
        /// on Result values.
        /// </summary>
        /// <param name="next">The next operation to run.</param>
        /// <typeparam name="U">The successful result type of the operation.</typeparam>
        /// <returns>
        /// The result of <c>next()</c> if this result was successful, otherwise this result's
        /// error value.
        /// </returns>
        public Result<U, E> AndThen<U>(Func<T, Result<U, E>> next)
        {
            return this.IsOk ? next(this.Value) : Result.Err(this.Error);
        }

        /// <summary>
        /// If this result was a success, runs the next operation <c>nextAsync()</c> on the result
        /// value. Otherwise, passes on this result's error value. This is useful for asynchronous
        /// control flow based on Result values.
        /// </summary>
        /// <param name="nextAsync">The next operation to run.</param>
        /// <typeparam name="U">The successful result type of the next operation.</typeparam>
        /// <returns>
        /// The result of <c>nextAsync()</c> if this result was successful, otherwise this result's
        /// error value.
        /// </returns>
        public async Task<Result<U, E>> AndThenAsync<U>(Func<T, Task<Result<U, E>>> nextAsync)
        {
            return this.IsOk
                ? await nextAsync(this.Value)
                : Result.Err(this.Error);
        }

        /// <summary>
        /// Unwraps a <see cref="Result{T,E}" /> to produce a value of type <c>T</c>, by providing
        /// a default to be used if the result is unsuccessful.
        /// If the default value is expensive to compute, consider using <see cref="OrElse" />
        /// instead.
        /// </summary>
        /// <param name="other">
        /// The default value, to be used if this result is unsuccessful.
        /// </param>
        /// <returns>
        /// The wrapped <c>T</c> value, or the default provided if there is none.
        /// </returns>
        public T Or(T other)
        {
            return this.IsOk ? this.Value : other;
        }

        /// <summary>
        /// Unwraps a <see cref="Result{T,E}" /> to produce a value of type <c>T</c>, by computing
        /// a default to be used if the result is unsuccessful. This function, as opposed to
        /// <see cref="Or" />, is useful when the default value is expensive to compute; the
        /// function <c>other</c> is only called in the case where this result is unsuccessful.
        /// </summary>
        /// <param name="other">
        /// A function that computes the default value, to be used if this result is unsuccessful.
        /// </param>
        /// <returns>
        /// The wrapped <c>T</c> value, or the default computed if there is none.
        /// </returns>
        public T OrElse(Func<T> other)
        {
            return this.IsOk ? this.Value : other();
        }

        /// <summary>
        /// Converts this entire result instance to a new type using the given mapping function
        /// <c>wrapping()</c>. Useful mainly for method chaining.
        /// </summary>
        /// <param name="wrapping">The mapping function.</param>
        /// <typeparam name="U">The new type this result is being mapped to.</typeparam>
        /// <returns>The result of the wrapping.</returns>
        public U Wrap<U>(Func<Result<T, E>, U> wrapping)
        {
            return wrapping(this);
        }

        /// <summary>
        /// Like <see cref="Wrap{U}" />, but applies a different wrapping function depending on
        /// whether this result is a success or a failure. This is like using both
        /// <see cref="Map{U}" /> and <see cref="MapErr{F}"/>, but acts on the
        /// <see cref="Result{T,E}"/> as a whole rather than the inner values.
        /// </summary>
        /// <param name="wrapOk">The wrapping function to use if this result is a success.</param>
        /// <param name="wrapErr">The wrapping function to use if this result is a failure.</param>
        /// <typeparam name="U">The new type this result is being mapped to.</typeparam>
        /// <returns>The result of the wrapping.</returns>
        public U WrapSplit<U>(Func<Result<T, E>, U> wrapOk, Func<Result<T, E>, U> wrapErr)
        {
            return this.IsOk ? wrapOk(this) : wrapErr(this);
        }

        /// <summary>
        /// Unsafely unwrap the success value of this result. This will return <c>default</c> if
        /// this result is a failure.
        /// </summary>
        /// <returns>The success value, or <c>default</c>.</returns>
        public T Unwrap()
        {
            return this.IsOk ? this.Value : default;
        }
    }
}
