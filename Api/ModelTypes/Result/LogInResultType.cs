using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a successful login attempt.
    /// </summary>
    public class LogInResultType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="LogInResultModel"/> to this result
        /// type.
        /// </summary>
        public static readonly Func<LogInResultModel, LogInResultType> FromModel =
            model => new LogInResultType(model);

        private readonly LogInResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public LogInResultType(LogInResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public int Id => this.model.Id;

        /// <summary>
        /// The registered email address of the user.
        /// </summary>
        public string Email => this.model.Email;
    }
}
