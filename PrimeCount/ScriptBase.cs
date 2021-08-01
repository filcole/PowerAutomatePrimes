using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public abstract class ScriptBase
{
    // Context object
    public IScriptContext Context { get; set; }

    // CancellationToken for the execution
    public CancellationToken CancellationToken { get; }

    // Helper: Creates a StringContent object from the serialized JSON
    public static StringContent CreateJsonContent(string serializedJson)
    {
        return new StringContent(serializedJson);
    }

    // Abstract method for your code
    public abstract Task<HttpResponseMessage> ExecuteAsync();
}
