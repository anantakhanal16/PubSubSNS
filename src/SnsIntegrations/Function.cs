using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SnsIntegrations
{
    public class Function
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly string _snsTopicArn;

        public Function()
        {
            _snsClient = new AmazonSimpleNotificationServiceClient();
        }

    
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
              
               var messageBody= request.Body.ToString();
               
                if (string.IsNullOrEmpty(messageBody))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 400,
                        Body = "Message cannot be empty."
                    };
                }

                // Publish the message to SNS
                var publishRequest = new PublishRequest
                {
                    TopicArn = "arn:aws:sns:us-east-1:783188417198:PubSubSnsStack-MySnsTopicCB85459E-KepLc2tP40gc",
                    Message = messageBody
                };

                var response = await _snsClient.PublishAsync(publishRequest);

                context.Logger.LogInformation($"Message published to SNS with MessageId: {response.MessageId}");
                context.Logger.LogInformation($"Message published to SNS with MessageId: {response.HttpStatusCode}");
                context.Logger.LogInformation($"Message published to SNS with MessageId: {response.ResponseMetadata}");

                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = "Message published to SNS successfully."
                };
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error publishing message to SNS: {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = "An error occurred while publishing the message."
                };
            }
        }

        public  class MessageRequest
        {
            public  string? Message { get; set; }
        }
    }
}
