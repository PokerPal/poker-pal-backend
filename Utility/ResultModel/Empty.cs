namespace Utility.ResultModel
{
    /// <summary>
    /// Represents an empty value in a <see cref="Result{T,E}" />.
    /// </summary>
    public class Empty
    {
        /// <summary>
        /// Converts any value into an <see cref="Empty" />.
        /// </summary>
        /// <param name="val">The value to convert.</param>
        /// <returns>The new <see cref="Empty" />.</returns>
        public static Empty Into(object val)
        {
            return new Empty();
        }
    }
}
