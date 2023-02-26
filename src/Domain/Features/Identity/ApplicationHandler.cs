using Domain.Features.Identity.Enums;

namespace Domain.Features.Identity;

public class ApplicationHandler :
    Seedwork.Entity,
    Dtat.Seedwork.Abstractions.IEntityHasIsActive,
    Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
    public ApplicationHandler(string name, string path) : base()
    {
        Name = name;
        Path = path;

        UpdateDateTime =
            InsertDateTime;
    }

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string Name { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]
    public bool IsActive { get; set; }
    // **********

    // **********
    public AccessTypeEnum AccessType { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (Name = nameof(Resources.DataDictionary.Title),
        ResourceType = typeof(Resources.DataDictionary))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string? Title { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Path))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.Path,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string Path { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Description))]
    public string? Description { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDateTime))]

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
        (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public System.DateTimeOffset UpdateDateTime { get; private set; }
    // **********

    public void SetUpdateDateTime()
    {
        UpdateDateTime =
            Dtat.DateTime.Now;
    }
}
