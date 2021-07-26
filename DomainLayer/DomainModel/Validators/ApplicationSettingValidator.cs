using System.Diagnostics.CodeAnalysis;

namespace DomainModel.Validators
{
    using DomainModel.Models;
    using FluentValidation;

    /// <summary>
    /// Validator for <see cref="ApplicationSetting"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApplicationSettingValidator : AbstractValidator<ApplicationSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingValidator"/> class.
        /// </summary>
        public ApplicationSettingValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Only letters, numbers and - _ are allowed");
        }
    }
}