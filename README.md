QuestionsBase
=============
Introduction

I am preparing for an interview and I was looking for questions. There are lots of questions and answers regarding every aspect of the software development out there. The problem is that it is difficult to save those questions and to come back to them when you want to reveal them again and again. Also looking at the answers just after the question doesn’t help to remember the answers. The purpose of the article and the web app is to solve this for others who find it difficult to store their list of questions.

Background - Business Logic of the system 
To define a question you need to choose Question Type, Question Difficulty Level, the question itself and the answer. 

After you have entered questions, you can take a "test' which will be question after question with their answers but hidden. You can filter by difficulty level and question tipe.

There is no need of authentication. 

The code is using CodeFirst approach and migrations to create the DB.

Using the code
I will just remind you the command you need to execute when want to apply the migrations to your local db.

Firstly change the connection string. The connection strings in QuestionsBase web.config and Repository app.config must match. Repository project is the one which have the migrations and the entities defined, also the DBContext is there (QuestionBaseDbContext).

Create the Database with name matching the one in the connectionstring. Then go to Package Manager Console and execute 

Collapse | Copy Code
PM> Update-Database -TargetMigration $InitialDatabase -ProjectName Repository -Verbose
PM> Update-Database -TargetMigration InitialCreate -ProjectName Repository -Verbose
PM> Update-Database
with default project Repository.

I have defined two question types - MVC and OOP in the Configuration.cs, but you can remove them after from the UI.

After you have the database created as all the web project you can run it with build in IIS or to configure IIS.

Points of Interest 
RepositoryCommon is the class library project whre the most common operations with the DB are defined.

QuestionBase.IOC - Where the Autocac dependancys are defined

QuestionBase.Services - The services which are consumed by the controllers of the MVC application

QuestionsBase - MVC 5 Application - This is done in the most simple way. I have used the technique to add controllers with by using Entity Framework. Then I refactored the whole code by using interfaces to the service class, so this can be Unit Testable. There are some tests defined in QuestionsBase.Test. 

QuestionsBase.Test - Test project of the MVC app. I am using Rhino.Mocks as a mocking framework and NUnit for the tests.

Overall the project shows a good structure, which can be used for extensions, but most important can accomodate unit testing.

I hope you enjoy it and if you want extend it.

I am waiting for you comments.

