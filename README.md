Dear Talha 
I have used Entity Framework core In Memory Database because i am on linux and sql server installation might takes some time, Where as EF Core is same as Sql Server RDBMS
This is the project architecture structure
![image](https://github.com/user-attachments/assets/85e5151e-1566-4a8c-920d-a1cfb709f07f)

Now when you move to the **WmsIntegration.API** directory you needs to execute these three commands
``` 
    dotnet clean 
    dotnet build
    dotnet run
```
Here you can check the seeding were done 
![image](https://github.com/user-attachments/assets/db5950ae-bbb7-4310-8c73-96533edf8d84)

Here is Swagger documentation for the API's Swagger URL :- http://localhost:5077/swagger/index.html
![image](https://github.com/user-attachments/assets/8de1e263-18e8-4e1a-bdf1-6d7e6e8e96f4)

We can see what data hase been seeds using the Swagger API Explorer all of them which are Rest API to fetch the records from the Databases
![image](https://github.com/user-attachments/assets/1fd8650d-c886-4156-af44-b38451640be5)

Now we have a scheduler which can we make through custom coding using Polly and while loop but in this case i prefer HangFire which i have recently used and it is also working with EF Core In Memory Database

Now we need to move to the folder directory **WmsIntegration.Scheduler** and we need to run these commands again
```
   dotnet clean
  dotnet build
  dotnet run
```

And then we can go to this URL localhost:5000 to check if the server is still up
And when we go to this url localhost:5000/hangfire

![image](https://github.com/user-attachments/assets/1155b70f-6b8d-42d8-9c0e-29b1d23b54eb)

The service will be there it will run after everyone hour if we need to change the cron expression we can do this from here means it will run after every one hour

![image](https://github.com/user-attachments/assets/8ee0a991-81b4-431c-8bfc-4a7b9ce5150a)

We can change this cron expression through code or through Database which make it configurable in a separate table
Now we have Test cases to check here is the directory for the test cases **WmsIntegration.Tests**
move to this directory and run command     ``` dotnet test  ``` to make n Unit test cases and mock http executable in the bellow screenshot it was successfull because the api server is up what if i make api server down than it will gaves the error here is the screenshot attached
![image](https://github.com/user-attachments/assets/11825c3c-08a8-4979-8085-a662a3818ca8)
![image](https://github.com/user-attachments/assets/4e55fb38-232a-4dd5-a754-7b45b801a382)






