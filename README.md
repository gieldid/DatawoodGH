<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![Linkedin][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<h3 align="center">DatwoodGH</h3>

  <p align="center">
    DatawoodGH is a library of grasshopper components made for the Datawood project at the robotlab Hva.
    It includes five grasshopper components and a RAPID file for the ABB robots at the lab.
    The grasshopper components are:
    <ol>
      <li><a href="https://github.com/gieldid/DatawoodGH/blob/main/Local/CsvCreator.cs">Csv creator</a></li>
      <li><a href="https://github.com/gieldid/DatawoodGH/blob/main/Local/PcdConverter.cs">Pointcloud converter</a></li>
      <li><a href="https://github.com/gieldid/DatawoodGH/blob/main/Network/FtpTransfer.cs">FTP transfer</a></li>
      <li><a href="https://github.com/gieldid/DatawoodGH/blob/main/Network/ApiCall.cs">Http call (POST or GET)</a></li>
      <li><a href="https://github.com/gieldid/DatawoodGH/blob/main/Network/SocketConnection/SocketClient.cs">Socket client</a></li>
    </ol>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#run">Installation</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

For basic usage of the grasshopper components download the latest <a href="https://github.com/gieldid/DatawoodGH/releases">release</a>. Before unzipping the release.zip 
right clickt it, click on properties and check the unblock box. Now you can unzip the release.zip file into your grasshopper libraries folder. When running grasshopper you should
be able to see the components underneath the datawood tab.

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
* [Rhino3d](https://www.rhino3d.com/)
* [Grasshopper](https://www.rhino3d.com/6/new/grasshopper/)


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started
In this chapter we'll explain how you can get the project to run and do a simple scan and get some data into the csv file.

### Prerequisites
Before running the datawood project grasshopper file you'll have to install the following programs:

  
* [Rhino3d](https://www.rhino3d.com/) 
* [jSwan](https://www.food4rhino.com/en/app/jswan) download the zip file *1 
* [Woodintake](https://gitlab.techniek.hva.nl/robotlab/wood/woodintake) follow the readme instructions and run the server.
* Download the [DatawoodGH](https://github.com/gieldid/DatawoodGH/releases/tag/v1.2.1) release.zip *1

*1 right click > properties > check unblock and extraxt the zip file in the C:\Users\\"USERNAME"\AppData\Roaming\Grasshopper\Libraries folder.


### Run

[Download the gh file](https://gitlab.techniek.hva.nl/GielJurriens/datawoodgh/-/blob/main/Resources/Grasshopper/Data%20Wood_WIP.gh) and open it with grasshopper. Change the ip adresses to the correct robots and servers.
Make sure the servers are running for the [weightscale](https://github.com/gieldid/Datawood-webserver) and the [color camera](https://github.com/gieldid/pictureServerNodejs).

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Giel Jurriëns - [@giela1](https://twitter.com/giela1) - gieldid@gmail.com

Project Link: [https://github.com/gieldid/datawoodgh](https://github.com/gieldid/datawoodgh)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/gieldid/DatawoodGH.svg?style=for-the-badge
[contributors-url]: https://github.com/gieldid/DatawoodGH/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/gieldid/DatawoodGH.svg?style=for-the-badge
[forks-url]: https://github.com/gieldid/DatawoodGH/network/members
[stars-shield]: https://img.shields.io/github/stars/gieldid/DatawoodGH.svg?style=for-the-badge
[stars-url]: https://github.com/gieldid/DatawoodGH/stargazers
[issues-shield]: https://img.shields.io/github/issues/gieldid/DatawoodGH.svg?style=for-the-badge
[issues-url]: https://github.com/gieldid/DatawoodGH/issues
[license-shield]: https://img.shields.io/github/license/gieldid/DatawoodGH.svg?style=for-the-badge
[license-url]: https://github.com/gieldid/DatawoodGH/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/giel-jurriëns/
[release-url]:https://github.com/gieldid/DatawoodGH/releases
[product-screenshot]: Resources/Screenshots/datawoodGHPostcall.png
