using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a user session creation operation.
    /// </summary>
    public class CreateUserSessionResultType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="CreateUserSessionResultModel"/> to this
        /// result type.
        /// </summary>
        public static readonly Func<CreateUserSessionResultModel, CreateUserSessionResultType>
            FromModel = model => new CreateUserSessionResultType(model);

        private readonly CreateUserSessionResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserSessionResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public CreateUserSessionResultType(CreateUserSessionResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the session.
        /// </summary>
        public int SessionId => this.model.SessionId;

        /// <summary>
        /// The unique identifier of the User.
        /// </summary>
        public int UserId => this.model.UserId;
    }
}