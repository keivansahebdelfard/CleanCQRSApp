namespace MyApp.Application.Common.Results
{
    public sealed record Error(
       string Code,
       string Message
   );
}
