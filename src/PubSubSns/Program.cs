using Amazon.CDK;


namespace PubSubSns
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new PubSubSnsStack(app, "PubSubSnsStack", new StackProps
            {
            });
            app.Synth();
        }
    }
}
