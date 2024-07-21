namespace RunDll
{
    public class RunResponse
    {
        public RunResponse()
        {

        }

        public RunResponse(object result)
        {
            this.Result = result;
        }

        public object Result { get; set; }
    }
}
