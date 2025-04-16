Dear Talha,

I have used Entity Framework Core In-Memory Database for this project since I am currently working on Linux, and setting up SQL Server might take some time. EF Core’s functionality closely resembles SQL Server’s RDBMS, so it serves well for development and testing purposes.

Project Architecture


Running the API
Navigate to the WmsIntegration.API directory and execute the following commands:

bash
Copy
Edit
dotnet clean
dotnet build
dotnet run
Once the application is running, you’ll see that the database seeding has been successfully performed:

![image](https://github.com/user-attachments/assets/db5950ae-bbb7-4310-8c73-96533edf8d84)

You can verify the seeded data using the Swagger API Explorer:

Swagger URL: http://localhost:5077/swagger/index.html
![image](https://github.com/user-attachments/assets/8de1e263-18e8-4e1a-bdf1-6d7e6e8e96f4)


This interface exposes all the available REST APIs that retrieve records from the in-memory database:



Scheduler (Hangfire)
We are using Hangfire for scheduling background jobs. While it is possible to implement a scheduler using a while loop and Polly for retry policies, I opted for Hangfire because of its robustness and recent integration in another project. It works well even with EF Core’s In-Memory Database.

To run the scheduler:

Navigate to the WmsIntegration.Scheduler directory.

Execute the following commands:

bash
Copy
Edit
dotnet clean
dotnet build
dotnet run
Visit http://localhost:5000 to ensure the server is running, and then navigate to http://localhost:5000/hangfire to access the Hangfire dashboard:

![image](https://github.com/user-attachments/assets/6782dab4-4739-4266-8e3c-af66e0d964e1)

![image](https://github.com/user-attachments/assets/1155b70f-6b8d-42d8-9c0e-29b1d23b54eb)


The scheduled job is configured to run every hour. If needed, the CRON expression can be adjusted directly from the dashboard or made configurable through a separate database table:


![image](https://github.com/user-attachments/assets/8ee0a991-81b4-431c-8bfc-4a7b9ce5150a)

Running Unit Tests
To execute the test cases, navigate to the WmsIntegration.Tests directory and run:

bash
Copy
Edit
dotnet test
These are NUnit test cases that mock HTTP requests. Here's a screenshot showing a successful test run when the API server is up and running:
![image](https://github.com/user-attachments/assets/11825c3c-08a8-4979-8085-a662a3818ca8)


However, if the API server is down, the tests will fail as expected:

![image](https://github.com/user-attachments/assets/4e55fb38-232a-4dd5-a754-7b45b801a382)
Let me know if you have any questions or need further assistance.

Best regards,
