namespace Floristai.Middleware
{
    [AttributeUsage(AttributeTargets.All)]
    public class LoggingAttribute : Attribute
    {
        public LoggingEvent Event { get; set; }

        public LoggingAttribute(LoggingEvent ev)
        {
            Event = ev;
        }
    }

    public enum LoggingEvent { Login }
}
