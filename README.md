# dotnet-interview-excercise

Pair programming exercise for dotnet server side candidates. Feel free to use google and whatever other tools you would normally use.
Please note that you are not expected to solve everything.

## Case ##
Create a simple username and password authentication API. Service interface below.

    public interface IUserService
    {
        UserEntity CreateUser(string userId, string password);
        UserEntity UpdatePassword(string userId, string oldPassword, string newPassword);
        bool Authenticate(string userId, string password);
        List<UserEntity> ListUsers();
    }

Update the API by updating the **UserController** class to use new implementation of user service.

## Tasks ##

Start with task one and progress as far as time allows. Independent of how far down the tasklist you get do task '7. Commit' when getting close to out of time. Remember to leave 10 min for wrapup at the end. 
#### 1. Get a grasp of the existing code / plan ahead ####
Do whatever you are comfortable with. Some hints
* Feel free to ask questions at any time.
* Start by trying to get an overview of how the code hangs together.
* Discuss a plan for implementing interface and or changes that could be made to existing code.
    what is needed to pass tests?
    security considerations?`


#### 2. Fix failing integration tests for UserControllerTests class ####
For this task you only need to implement these methods of **IUserService**:
* **CreateUser**
* **Authenticate**

And corresponding endpoints in **UserController** class:
* **CreateUser**
* **Authenticate**

#### 3. Add change password ####
Add a simple implementation of the change password function defined in the interface and corresponding endpoint for updating user password.

#### 4. Security improvements part 1 ####
Add some password validation rules

#### 5. Security improvements part 2 ####
 Add hashing and salting of password stored in DB

#### 6. Fix failing test for AdminControllerTests ####
For this task you need to implement the **ListUsers** methods of **IUserService**:
And corresponding endpoint **ListUsers** in **AdminController** class:

#### 7. Commit ####

Independently of how far down the task list do this when time is close to running out. 

Create branch and commit changes. Branch name should be on the follownig format YYYY-MM-DD-SS where
* YYYY - Year
* MM - Month
* DD - Day
* SS - Serial number, given by interviewer 

*DO NOT USE PERSONAL INFO IN COMMITED DATA*

## Wrap up ##

* How did you feel it went?
* What did we do well and what do we need to improve?
* If you had another change what would you improve?
* How long do you think you need to complete the task?
