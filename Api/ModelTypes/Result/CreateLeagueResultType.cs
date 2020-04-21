using System;

using Application.Models.Result;

namespace Api.ModelTypes.Result
{
    /// <summary>
    /// Data returned as the result of a league creation operation.
    /// </summary>
    public class CreateLeagueResultType
    {
        /// <summary>
        /// Converts an instance of the result model<see cref="CreateLeagueResultModel"/> to
        /// this result type.
        /// </summary>
        public static readonly Func<CreateLeagueResultModel, CreateLeagueResultType>
            FromModel = model => new CreateLeagueResultType(model);

        private readonly CreateLeagueResultModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateLeagueResultType"/> class.
        /// </summary>
        /// <param name="model">The model to wrap.</param>
        public CreateLeagueResultType(CreateLeagueResultModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The unique identifier of the newly created league.
        /// </summary>
        public int Id => this.model.Id;
    }
}
