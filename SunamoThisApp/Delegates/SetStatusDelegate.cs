namespace SunamoThisApp.Delegates;

/// <summary>
/// Delegate for setting application status with a message type and text.
/// </summary>
/// <param name="typeOfMessage">The type of the status message.</param>
/// <param name="message">The status message text.</param>
public delegate void SetStatusDelegate(TypeOfMessageTA typeOfMessage, string message);
