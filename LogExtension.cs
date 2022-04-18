using Serilog.Context;
using System.Runtime.CompilerServices;

namespace SerilogExample
{
	public static class LoggerExtensions
	{
		public static void LogAppError(this ILogger logger, Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			using var prop = LogContext.PushProperty("Method", memberName);
			LogContext.PushProperty("FilePath", sourceFilePath);
			LogContext.PushProperty("LineNumber", sourceLineNumber);
			logger.LogError(exception, message);
		}

		public static void LogAppWarning(this ILogger logger, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			using var prop = LogContext.PushProperty("Method", memberName);
			LogContext.PushProperty("FilePath", sourceFilePath);
			LogContext.PushProperty("LineNumber", sourceLineNumber);
			logger.LogWarning(message);
		}
	}
}
