### To Do:
Save the instance of the nonogram the creator has edited to the database
and then generate a fresh blank board with clues for the nonogram for a solver

cosmetic changes to cells and board itself

ISSUE:

1.cells wraps when the grid is too large for the screen. Also, the click function breaks.
ex. app breaks at when gridsize is 21 by 21. 

**Click function break ISSUE was resolve by moving the input fields out of the for loop 




# Nonogram Puzzle Builder

#### A webapp for creating and playing pixel based number puzzles called Nonograms or Picross. 

## Authored by:
Athea DeLing, Bai Jaitrong, Quin Asselin, January 2023

***

## Table of Contents
1. [Repository Description](#repository-description)
2. [Technologies Used](#technologies-used)
3. [Setup Instructions](#installation-and-setup)
4. [Known Bugs](#known-bugs)
5. [License Information](#license)

## Repository Description
The site prompts the user to enter the dimensions of the puzzle they would like to construct and they're then presented with a series of cells in a grid (of the specificied dimensions). When the user has finished building their puzzle, they're able to save it and view it again from a list on the Nonogram/Index page. This project was written and compiled in C-Sharp with a 3-person group over the course of 7-days.

## Technologies Used
This project was hand-built in tandem with a programming class taught by Epicodus. It contains use the technologies and programs listed below.

- C#
- ASP.NET
- Git
- Github
- Markdown
- MySQL Workbench w/MySQL
- Razor
- VSCode
- HTML5
- CSS3
- EF Core

## Installation And Setup

### Preparatory Installation Steps
1. To begin, the user must install a compatible version of .NET 6. An acceptable version can be found [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
2. Follow the prompts throught the installation of the program. Using default settings as they originally appear in the setup.
3. In the terminal (ex: Git Bash) install dotnet-script by runing the following command
```bash
$ dotnet tool install -g dotnet-script
```
this will install "dotnet-script"
4. Configure your local environment to use this. Details for which can be found [here](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-dotnet-script).
5. Then, install MySQL. Follow further detailed instructions on accomplishing that [here](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql).

### Repository Setup
1. Clone this repository.
2. Open your shell (e.g. Terminal or GitBash) and navigate to this project's production directory called "Parks".
3. Within the Parks folder, create a file titled appsettings.json
4. Open your file editor and navigate to appsettings.json
5. In the appsettings.json file, paste the following code:
```javascript
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=5001;database={database_name};uid=[uid];pwd=[pwd];"
  }
}
```
6. Replace [database_name], [uid], and [pwd] with your selected database name, as well as your created SQL username and password respectively (including the braces).
7. Additionally, you will need to install some packages in order to run this code. Within the same directory please run the following commands:
```bash
dotnet add package Microsoft.EntityFrameworkCore -v 6.0.0 
```
```bash
dotnet add package Pomelo.EntityFarmeworkCore.MySql -v 6.0.0
```
```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
```
```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
```

### Creating the Initial Database
*To create and instantiate a database with preseeded values we will be required to execute a brief series of commands*
1. Within bash/terminal navigate to the project's root directory which is housed in the Parks.Solution Folder.
2. Execute the proceeding commands the sequence displayed below:
```bash
dotnet ef migrations add Initial
```
then:
```bash
dotnet ef database update
```
*__To ensure no loss of data occurs, please use MySQl Workbench to determine an appropriate database name that is not already in use. __*
### Execute the Program
1. Open the terminal and navigate to the production directory titled "Parks.Solution."
2. Run `dotnet watch run` in the command line to start the project in development mode with a watcher.
3. Open the browser to _https:localhost:5001/_. If you cannot access localhost:5001 it is likely because you have not configured a .NET developer security certificate for HTTPS. To learn about this, review this lesson: [Redirecting to HTTPS and Issuing a Security Certificate](https://www.learnhowtoprogram.com/c-and-net/basic-web-applications/redirecting-to-https-and-issuing-a-security-certificate).

## Known Bugs
- If the grid constructed is sufficiently large, the process of the computer will be slowed down. There is not a limit currently on the size of the grid the user can request which may result in the construction of a very large array and a _very_ high hang time.
- Additionally, with grids that are larger than the size of the user's window, the grid will not currently scale properly to fit into the window and will move on to the line below. In the final version of this app, the size of the cells would scale down to fit the window, or generate a horizontal scroll bar in the case a scaled series of images would be prohibitively small.
- There is currently a working issue with some instances of the search functionality tied to the API and as such filtering database entries along certain criteria does not work as intended.
- Additionally, certain search queries using special characters (such as punctuation or a space) will need to be formatted by an API platform or customized by hand 


## License
*Athea DeLing, Bai Jaitrong, Quin Asselin, February 2023. Available for distribution, modification, inspection, and application under [GPLv3 License.](https://www.gnu.org/licenses/gpl-3.0.en.html)*