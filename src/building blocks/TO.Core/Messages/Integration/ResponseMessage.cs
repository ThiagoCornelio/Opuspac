
using FluentValidation.Results;

namespace TO.Core.Messages.Integration;

public class ResponseMessage : Message
{
    public ResponseMessage(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    //Utilizado para  realizar as validações nas APIs
    public ValidationResult ValidationResult { get; set; }
}
