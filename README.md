# Lord of the Rings SDK

# Project Name - Developer Instructions

Welcome to the Project Name repository! This README provides you with instructions on how to use the provided Postman collection to interact with the API's `GenerateCode` endpoint.

## Getting Started

1. Clone the Repository:
   ```bash
   git clone https://github.com/fabiodemartino/sdkFromOpenApi
   
2. Open the Solution and build
3. Play the solution so that the Swagger API page will display in a browser instance

3. Access the API:		
   The endpoint to test is https://localhost:7205/api/v1/GenerateCode and it takes a file to upload.

1. The API should now be running and accessible at https://localhost:7205;http://localhost:5051 (or the specified port).

## Using the Postman Collection
Locate the Postman Collection:
Inside the postman folder in this repository, you will find a Postman collection file named SDK Builder.postman_collection.json.

Import the Collection into Postman:

Open Postman.
Click on "Import" in the top-left corner.
Select the ProjectNameApi.postman_collection.json file from the postman folder and import it.

Select a file to upload into the API via Postman the files are located in the Assets folder 

Send a Request to the API:

Open the imported Postman collection.
Select the GenerateCode request.
Choose the "Project Name Local" environment.
Update any request parameters if needed.

Click "Send" to make the request to the api/v1/GenerateCode endpoint.
View the Response:

Postman will display the response from the API, including generated code or relevant data.

