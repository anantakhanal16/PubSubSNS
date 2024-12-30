using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SNS.Subscriptions;
using Amazon.CDK.AWS.SQS;
using Constructs;

namespace PubSubSns
{
    public class PubSubSnsStack : Stack
    {
        internal PubSubSnsStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create SNS Topic
            var snsTopic = new Topic(this, "MySnsTopic", new TopicProps
            {
                DisplayName = "TestTopic"
            });

            // Create Lambda Function
            var lambdaFunction = new Function(this, "MyLambdaFunction", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset(@"C:\Users\ananta\OneDrive\Desktop\sns\PubSubSNS\src\SnsIntegrations\bin\Release\net8.0\linux-x64\publish"),
                Handler = "SnsIntegrations::SnsIntegrations.Function::FunctionHandler"
            });

            // Grant the Lambda permission to publish to the SNS topic
            snsTopic.GrantPublish(lambdaFunction);

            // Create API Gateway and integrate it with the Lambda
            var api = new RestApi(this, "MyApi", new RestApiProps
            {
                RestApiName = "ApiGateWay",
                Description = "API Gateway with Lambda integrations"
            });

            var lambdaIntegration = new LambdaIntegration(lambdaFunction);
            var resource = api.Root.AddResource("publish");
            resource.AddMethod("POST", lambdaIntegration);

            // Create two SQS queues
            var sqsQueue1 = new Queue(this, "MyQueue1", new QueueProps
            {
                QueueName = "SNS_sub_1",
                RetentionPeriod = Duration.Days(4), // Message retention period
                VisibilityTimeout = Duration.Seconds(30), // Visibility timeout
                ReceiveMessageWaitTime = Duration.Seconds(20) // Long polling
            });

            var sqsQueue2 = new Queue(this, "MyQueue2", new QueueProps
            {
                QueueName = "SNS_sub_2",
                RetentionPeriod = Duration.Days(4), // Message retention period
                VisibilityTimeout = Duration.Seconds(30), // Visibility timeout
                ReceiveMessageWaitTime = Duration.Seconds(20) // Long polling
            });

            // Add subscriptions for both SQS queues to the SNS topic
            snsTopic.AddSubscription(new SqsSubscription(sqsQueue1));
            snsTopic.AddSubscription(new SqsSubscription(sqsQueue2));
        }
    }
}
