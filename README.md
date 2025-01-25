###### PubSubSns CDK Project


This project demonstrates a serverless architecture using AWS services, including Lambda, API Gateway, SNS, and SQS. It consists of two projects:

PubSubSns: Contains the CDK stack definitions for the infrastructure (SNS topic, Lambda function, API Gateway, and SQS queues).
SnsIntegrations: Contains the code for the Lambda function (Function.cs).

##Prerequisites
AWS Account

Ensure you have an active AWS account.
IAM User

Create an IAM user with necessary permissions to deploy AWS resources such as Lambda, SNS, SQS, and API Gateway.
AWS Credentials

Configure AWS credentials on your local machine using the AWS CLI:
aws configure on cmd 
 
AWS CDK
Install AWS CDK globally if not already installed:
npm install -g aws-cdk  

##Deployment Steps
1. Publish the Lambda Function
Navigate to the SnsIntegrations project.
Publish the Lambda function using the .NET CLI:
dotnet publish -c Release -r linux-x64 --self-contained true  
The published files will be located in the bin\Release\net8.0\linux-x64\publish directory.

2. Deploy the CDK Stack
Navigate to the PubSubSns project directory.
Run the following command to deploy the stack:
cdk deploy 
 
##Testing
Once the stack is deployed, you can test the setup:
API Gateway

Use Postman or any API client to send a POST request to the /publish endpoint provided by the API Gateway.
SNS and SQS

Verify that messages published by the Lambda function are delivered to the two SQS queues subscribed to the SNS topic.
AWS Console

Alternatively, invoke the Lambda function directly from the AWS Console and observe the flow.
File Structure
PubSubSns: Infrastructure definitions (stacks, resources).
SnsIntegrations/Function.cs: Code for the Lambda function that publishes messages to the SNS topic.
With this guide, you’ll be able to deploy and test the serverless Pub/Sub architecture seamlessly.
