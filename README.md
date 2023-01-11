<br/>
<p align="center">
  <h3 align="center">Proxies Traffic Reporter</h3>
</p>

![Downloads](https://img.shields.io/github/downloads/jfmachin/ProxiesTrafficReporter/total) ![Contributors](https://img.shields.io/github/contributors/jfmachin/ProxiesTrafficReporter?color=dark-green) ![Forks](https://img.shields.io/github/forks/jfmachin/ProxiesTrafficReporter?style=social) ![Stargazers](https://img.shields.io/github/stars/jfmachin/ProxiesTrafficReporter?style=social) ![Issues](https://img.shields.io/github/issues/jfmachin/ProxiesTrafficReporter) 

## About The Project

This a microservice developed in .net 6.0 that notify internet usage in several proxies for scraping web pages.

## Built With

.NET 6.0

Puppeteer

Docker

## Getting Started

This is an example of how you may give instructions on setting up your project locally. To get a local copy up and running follow these simple example steps. If you are in Cuba you have to set the VPN on in order to download Puppeteer Chromium.

### Prerequisites

Install dotnet 6.0

### Installation
1. Clone the repo
```sh
git clone https://github.com/jfmachin/ProxiesTrafficReporter.git
```
2. Locate on PTR.API folder
3. Create an appsettings.json with credentials
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ProxyMesh": {
    "user": "user",
    "pass": "pass",
    "webHookUrl": theMSTeamsUrlWebhook
  }
}
```
<b>RUNNING DIRECTLY</b>

4. dotnet run

<b>RUNNING WITH DOCKER</b>

4. Locate on the root folder (where the .sln is)

5. docker build -t [tagname] .

6. docker run --rm [containername]


## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.
* If you have suggestions for adding or removing projects, feel free to [open an issue](https://github.com/jfmachin/ProxiesTrafficReporter/issues/new) to discuss it, or directly create a pull request after you edit the *README.md* file with necessary changes.
* Please make sure you check your spelling and grammar.
* Create individual PR for each suggestion.

### Creating a Pull Request

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Authors

* **Javier Fernandez** - *Software Engineer* - [Javier Fernandez](https://github.com/jfmachin) - *Project Head*