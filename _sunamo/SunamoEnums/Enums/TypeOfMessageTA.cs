namespace SunamoThisApp;


/// <summary>
///     Error/Warning are in tbLastErrorOrWarning, other in tbLastOtherMessage
///     Musí být zde kvůli cl které je withoutDep
/// </summary>
public enum TypeOfMessageTA
{
    /// <summary>
    ///     tbLastErrorOrWarning
    /// </summary>
    Error,
    /// <summary>
    ///     tbLastErrorOrWarning
    /// </summary>
    Warning,
    Information,
    /// <summary>
    ///     Returned if from text cant determine value
    /// </summary>
    Ordinal,
    Appeal,
    Success
}