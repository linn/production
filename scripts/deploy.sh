#!/bin/bash
set -ev

# install aws cli
curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
unzip awscliv2.zip
sudo ./aws/install

# deploy on aws
if [ "${TRAVIS_BRANCH}" = "master" ]; then
  if [ "${TRAVIS_PULL_REQUEST}" = "false" ]; then
    # master - deploy to production
    echo deploy to production

    aws s3 cp s3://$S3_BUCKET_NAME/production/production.env ./secrets.env

    STACK_NAME=production
  	ENV_SUFFIX=
  else
    # pull request based on master - deploy to sys
    echo deploy to sys

    aws s3 cp s3://$S3_BUCKET_NAME/production/sys.env ./secrets.env

    STACK_NAME=production-sys
    ENV_SUFFIX=-sys
  fi
fi

# load the secret variables but hide the output from the travis log
source ./secrets.env > /dev/null 2>&1

# deploy the service to amazon
aws cloudformation deploy --stack-name $STACK_NAME --template-file ./aws/application.yml --parameter-overrides dockerTag=$TRAVIS_BUILD_NUMBER databaseHost=$DATABASE_HOST databaseName=$DATABASE_NAME databaseUserId=$DATABASE_USER_ID databasePassword=$DATABASE_PASSWORD rabbitServer=$RABBIT_SERVER rabbitPort=$RABBIT_PORT rabbitUsername=$RABBIT_USERNAME rabbitPassword=$RABBIT_PASSWORD appRoot=$APP_ROOT proxyRoot=$PROXY_ROOT authorityUri=$AUTHORITY_URI loggingEnvironment=$LOG_ENVIRONMENT loggingMaxInnerExceptionDepth=$LOG_MAX_INNER_EXCEPTION_DEPTH environmentSuffix=$ENV_SUFFIX --capabilities=CAPABILITY_IAM

echo "deploy complete"
