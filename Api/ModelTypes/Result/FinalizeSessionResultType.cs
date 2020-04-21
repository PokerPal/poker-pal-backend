using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a session being finalized.
    /// </summary>
    public class FinalizeSessionResultType
    {
        /// <summary>
        /// Converts an instance of the result model<see cref="FinalizeSessionResultModel"/> to
        /// this result type.
        /// </summary>
        public static readonly Func<FinalizeSessionResultModel, FinalizeSessionResultType>
            FromModel = model => new FinalizeSessionResultType(model);

        private readonly FinalizeSessionResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalizeSessionResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public FinalizeSessionResultType(FinalizeSessionResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the session.
        /// </summary>
        public int Id => this.model.Id;
    }
}