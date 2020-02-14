#!/bin/bash
set -ev

dotnet restore

# upgrade node to latest version
if [ "$CI" ] && [ "$TRAVIS" ]
then 
	source ~/.nvm/nvm.sh; 
	nvm install 10;
	nvm use 10;
fi

cd ./src/Service.Host
rm -rf ./node_modules
npm install
npm run build
cd ../..
