namespace Utility.ResultModel
{
    /// <summary>
    /// Represents a <see cref="Result{T,E}"/> that signifies a successful operation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the success value wrapped inside the <see cref="Result{T,E}"/>.
    /// </typeparam>
    public sealed class OkResult<T>
    {
        internal OkResult(T value)
        {
            this.Value = value;
        }

        internal T Value { get; }
    }
}
