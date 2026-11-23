namespace SunamoThisApp.Delegates;

/// <summary>
/// Handles a reported status message, see <see cref="ThisApp.StatusSet"/>.
/// </summary>
/// <param name="typeOfMessage">Type of the message.</param>
/// <param name="message">Final text of the message, already formatted and trimmed.</param>
public delegate void SetStatusDelegate(TypeOfMessageTA typeOfMessage, string message);
