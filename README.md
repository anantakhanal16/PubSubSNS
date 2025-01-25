PubSubSns CDK Project
This project demonstrates a simple serverless Publish-Subscribe (Pub/Sub) architecture using AWS CDK. It includes an SNS topic, a Lambda function, an API Gateway, and SQS queues.

Architecture Details
API Gateway

An API Gateway is created with a POST method at the /publish endpoint.
This API triggers the Lambda function.
Lambda Function

A .NET 6 Lambda function (MyLambdaFunction) is set up to publish messages to the SNS topic.
The function is invoked by the API Gateway.
SNS Topic

An SNS topic (MySnsTopic) is created.
The Lambda function has permissions to publish messages to this topic.
SQS Queues

Two SQS queues (MyQueue1 and MyQueue2) are created.
Both queues are subscribed to the SNS topic.
Messages published to the SNS topic are delivered to both SQS queues.
Message Flow
A user sends a POST request to the /publish endpoint on the API Gateway.
The API Gateway triggers the Lambda function.
The Lambda function publishes a message to the SNS topic.
The SNS topic broadcasts the message to the subscribed SQS queues (MyQueue1 and MyQueue2).
Summary of Components
API Gateway: /publish endpoint for publishing messages.
Lambda Function: Publishes messages to the SNS topic.
SNS Topic: Routes messages to the subscribed SQS queues.
SQS Queues: Receives and stores messages from the SNS topic.
This description highlights the relationship between the components and their roles. Let me know if you'd like any further adjustments!
