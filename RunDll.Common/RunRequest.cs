namespace RunDll
{
    public class RunRequest
    {
        public RunRequest()
        {

        }

        public RunRequest(string assembly, string type, string method, object config, object[] arguments)
        {
            this.Assembly = assembly;
            this.Type = type;
            this.Method = method;
            this.Config = config;
            this.Arguments = arguments;
        }

        public string Assembly { get; set; }

        public string Type { get; set; }

        public string Method { get; set; }

        public object Config { get; set; }

        public object[] Arguments { get; set; }
    }
}
