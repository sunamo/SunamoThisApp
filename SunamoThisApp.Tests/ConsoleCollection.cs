namespace SunamoThisApp.Tests;

/// <summary>
/// Test collection for tests that redirect the console or change static state, so they never run in parallel.
/// </summary>
[CollectionDefinition(Name, DisableParallelization = true)]
public class ConsoleCollection
{
    /// <summary>
    /// Name of the collection.
    /// </summary>
    public const string Name = "Console";
}
