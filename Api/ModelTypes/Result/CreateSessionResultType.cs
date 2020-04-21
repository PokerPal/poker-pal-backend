using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a session creation operation.
    /// </summary>
    public class CreateSessionResultType
    {
        /// <summary>
        /// Converts an instance of the result model<see cref="CreateSessionResultModel"/> to
        /// this result type.
        /// </summary>
        public static readonly Func<CreateSessionResultModel, CreateSessionResultType>
            FromModel = model => new CreateSessionResultType(model);

        private readonly CreateSessionResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSessionResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public CreateSessionResultType(CreateSessionResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the newly created session.
        /// </summary>
        public int Id => this.model.Id;
    }
}
