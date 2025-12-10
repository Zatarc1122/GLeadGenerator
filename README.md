# Lead generator

This project is a result of a technical test given by Asseco SEE. The purpose of the test was to showcase the author's skills in .NET Core and Web technologies.

See the [wiki page](https://github.com/frakic/LeadGenerator/wiki/Requirements) for full requirements.

## Getting started

The following instructions will allow you to copy and run the project on a local machine for development and testing purposes.

### Prerequisites

It is strongly recommended that you install the following software before running the app:

- [Visual Studio](https://visualstudio.microsoft.com/) / VS Code
- [Docker Desktop](https://docs.docker.com/get-docker) with Docker Compose V2 enabled in settings
- [Node.js](https://nodejs.org/en/download)

### Installation

1.  Clone the repository onto a local machine using the command:

```bash
git clone https://github.com/frakic/LeadGenerator
```

2.  From there you can open **GLeadGenerator.sln** in Visual Studio.

3.  If you have Docker Desktop installed, Visual Studio should pull all of the required images and run docker-compose on start.

4.  The first time you run the app, you might see a message in your browser, warning you about an invalid security certificate. Click on Advanced... and then Accept the Risk and Continue

![Certificate error](https://i.ibb.co/R6Dx50p/certErr.png)

5.  Once the backend projects are running and you can see the Swagger UI in your browser, it's time to go into the frontent project folder:

```bash
cd /GLeadGenerator/GLeadGenerator.Web/ClientApp
```

6.  Install dependencies:

```bash
npm i
```

7.  And run the project:

```bash
npm run dev
```

8.  After that, just visit http://localhost:5004 and you should see the app

## Functionalities

1. **Database**: Upon successfully building and running all backend projects, you can manually inspect the database using MS SSMS or your preferred database management tool. The credentials can be found in _setup.sql_ script on line 5.
2. **Integration API**: Functions as a separate application with its own Swagger UI, which can be viewed by setting the "composeLaunchServiceName" property in _launchSettings.json_ file to "GLeadGenerator.integration.api" and running the project with docker-compose.
3. **Sending email**: Since this is a mock app, a fake SMTP service called [Ethereal Email](https://ethereal.email/login) is being used. If you want to check whether the emails are actually being delivered to the recipient, you can log in using the credentials provided in the _appsettings.Development.json_ file in the GLeadGenerator.Api project.
4. **Logging**: Application logs with exceptions and other useful information can be viewed by going into a running docker container _GLeadGenerator.Api_ and navigating to /var/log/LeadGenerator.Api folder.

## Troubleshooting

If you encounter any issues while trying to run or build the project, you can refer to the following troubleshooting tips:

1. **Dependency issues**: Make sure that you have installed all required dependencies by following the instructions in the Installation section. If you are encountering an error related to a missing dependency, try reinstalling the dependencies by running the relevant command(s) again.

2. **Database issues**: When the database container starts, it uses _entrypoint.sh_, _mssql-customize.sh_ and _setup.sql_ scripts, located in /GLeadGenerator.Database folder, in order to initialize the database. If you are unable to connect to the database on localhost, try deleting the database container, restarting docker and running docker-compose again. Also, make sure that the first two mentioned scripts have line endings set to LF, as opposed to CRLF.

3. **Port(s) unavailable**: LeadGenerator uses the following ports for all of its components: 5000, 5001, 5002, 5003, 5004. Make sure that you have nothing else running on these before running LeadGenerator.

