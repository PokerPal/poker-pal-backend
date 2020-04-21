using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of deletion of a user.
    /// </summary>
    public class DeleteUserResultType
    {
        /// <summary>
        /// Converts an instance of the model <see cref="DeleteUserResultModel"/> to this result
        /// type.
        /// </summary>
        public static readonly Func<DeleteUserResultModel, DeleteUserResultType> FromModel
            = model => new DeleteUserResultType(model);

        private readonly DeleteUserResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public DeleteUserResultType(DeleteUserResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the deleted user.
        /// </summary>
        public int Id => this.model.Id;
    }
}