# Introduction
This project is meant to do simple management of company's employees. 

# The project
## In a main console the user should be able to:
1.	Add employees
2.	Modify employees
3.	Show all employees
4.  Search employers

# Motivation
I've started this simple project so I would be able to show some of my habilities with C#.

# Habilities
|  **HABILITY**  |       **REASON**    |
|:-----------:|:-------------------:|
|Object Oriented Programming|Was used in a way that methods were created for each new functionality and classes were divided in different libraries, more than one file, making the app easier to expand and keeping it better organized|
|LINQ|An inportant library to search in databases, in this case, they were organized as a list. Good naming convention made LINQ usage a lot smoother. Example: `_employees.Where`|
|C# shortcuts and good name convention|Along with LINQ staementents like `.Where`, good name conventions and C# shortcuts (ex. inline conditional statements, anonymous functions) like `_employees.ForEach` (instead of the conventional `for (int i = 0; i < length; i++)` ) made the app easier to be read. For example, it's pretty simple to understand that `employees.ForEach(e => CreateDATFile(e))` will create a .DAT file for each employee|
|Exception handling|This hability made possible the handling of wrong user inputs, for example when he passed wrong ID's to be deleted|
|File IO, Serialization and Directory/File manipulation|Three very important habilities that could not be forgotten, since, like in big softwares, keeping logs or temp files are crucial for the application. In this case, just so I could exercise more, I created the dataset based on that|
|Good practicies|Naming, separating classes in different files, DRY (dont-repeat-yourself) and more, were followed so the software can be easily read and understood by other programmers|
|Azure DevOps and Git|Both services were used so I could get more familiar with them and their benefits|
