using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a user creation operation.
    /// </summary>
    public class CreateUserResultType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="CreateUserResultModel"/> to this result
        /// type.
        /// </summary>
        public static readonly Func<CreateUserResultModel, CreateUserResultType> FromModel =
            model => new CreateUserResultType(model);

        private readonly CreateUserResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public CreateUserResultType(CreateUserResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the newly created user.
        /// </summary>
        public int Id => this.model.Id;
    }
}
