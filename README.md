<p align="center"><img src="https://images.squarespace-cdn.com/content/v1/5befe34ed274cba684adc722/1544732087748-6J4NJEIF55GLLJXPUD54/Kazoku_+Definition+%284%29.png"></p>

# Kazoku Template Web API

## Description

API Template project, developed by [Kazoku IT AB](https://kazoku.se). 


## Features

- API versioning
- Easy Swagger configuration
- Improved console logging
- Health check endpoint

## Installation on Windows
1. Press the button `Use this template`.
2. Donwload the project to your machine. 
3. Open the project in your favorite IDE.
4. Navigate to your project folder with the terminal.
5. Run the project using `dotnet run` (requires dotnet to be installed on the PC).

#### Resources 
[Install .NET on Windows](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net60))

## Usage

Web API project is intended to be used as an easy start when building .NET web API's with some nice to have features already configured and coded.   

#### Web API name
In the top of the `program.cs` file you can find a variable named `apiName`. This controls how the web API is named. This name appears in the Swagger and in the logs. 

To set the Swagger web page title you can change that in the file `SwaggerOptions/ConfigureSwaggerOptions.cs` by changing the title property on line `44`.

#### Logging timestamp
If you would like to log in the UTC timezone you can comment out the line `46` in the file `program.cs`.

## Contributing

Can also be found here - [Contributing to this project](https://github.com/kazokuit/Kazoku.Template.WebApi/blob/main/CONTRIBUTING.md)

#### - Issues

- Screenshot the problem
- Open a new issue
- Give it a meaningful title
- Describe the issue clearly
- Upload the screenshot
- Add useful labels
- Submit issue

#### - Coding

- See the [issue list](https://github.com/kazokuit/Kazoku.Template.WebApi/issues))
- Assign yourself to an issue
- Open a new branch
- Create your _beautiful_ code
- Create a pull request

## Software

Recommended the to get going fast:

- Visual Studio 2022
- Web browser

## Credits

Special thanks to Linda Carlstad It-committee 2018/2019 for creating this website.
