using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using LamdaWithSNS.Dto;
using System.Text.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LamdaWithSNS;

public class Function
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly string _snsTopicArn;
    public Function()
    {
        _snsClient = new AmazonSimpleNotificationServiceClient();
        _snsTopicArn = Environment.GetEnvironmentVariable("SNS_TOPIC_ARN");
    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SNS event object and can be used 
    /// to respond to SNS messages.
    /// </summary>
    /// <param name="evnt">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
    {
        foreach (var record in evnt.Records)
        {
            await ProcessRecordAsync(record, context);
        }
    }


    private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed record {record.Sns.Message}");

      
        await Task.CompletedTask;
    }

    //public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    //{
    //    try
    //    {
    //        // Parse the incoming message
    //        var requestBody = JsonSerializer.Deserialize<MessageRequest>(request.Body);

    //        if (string.IsNullOrEmpty(requestBody?.Message))
    //        {
    //            return new APIGatewayProxyResponse
    //            {
    //                StatusCode = 400,
    //                Body = "Message cannot be empty."
    //            };
    //        }

    //        // Publish the message to SNS
    //        var publishRequest = new PublishRequest
    //        {
    //            TopicArn = _snsTopicArn,
    //            Message = requestBody.Message
    //        };

    //        await _snsClient.PublishAsync(publishRequest);

    //        return new APIGatewayProxyResponse
    //        {
    //            StatusCode = 200,
    //            Body = "Message published to SNS successfully."
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        context.Logger.LogError($"Error: {ex.Message}");
    //        return new APIGatewayProxyResponse
    //        {
    //            StatusCode = 500,
    //            Body = "An error occurred while publishing the message."
    //        };
    //    }
    //}
   
}