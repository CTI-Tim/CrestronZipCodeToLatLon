<img src="https://img.shields.io/badge/Language-SimplPlus-green?style=for-the-badge"> <img src="https://img.shields.io/badge/Language-SIMPL-forest?style=for-the-badge"> <img src="https://img.shields.io/badge/Language-C Sharp-blue?style=for-the-badge"> <img src="https://img.shields.io/badge/Platform-Crestron 4 series-blue?style=for-the-badge"><img src="https://img.shields.io/badge/Platform-Crestron 3 series-lightblue?style=for-the-badge"> <img src="https://img.shields.io/badge/CTI-Examples-blue?style=for-the-badge">  <img src="https://img.shields.io/badge/Use-Educational-green?style=for-the-badge"> <img src="https://img.shields.io/badge/Copyright-Crestron-blue?style=for-the-badge"> <img src="https://img.shields.io/badge/License-Restricted-orange?style=for-the-badge"> <img src="https://img.shields.io/badge/Support-NONE-red?style=for-the-badge">

# Get Location and GMT from Postal Code for Crestron 3 and 4 series

This is a C# library and Simpl Plus module that wraps the library to get the Lat and Long as well as GMT Offset from the postal code.   This creates signals that the astronomical clock in Crestron Simpl wants for the location to calculate sunrise and sunset.    This only works for US and Canada.

I made this because first I am lazy and tired of having to look up all this stuff for locations, and secondly have an example for programmers to see how a library can be used to look up information from an embedded resource file by leveraging reflection.  

Two C# projects are in this repository. 4 series made with VS2022 and 3 series made with 2008Pro. The code is identical in both except for the SimplSharpString for the 3 series parameter for the method called.  4 Series does not require this in this case.   



When compiling the Simpl Plus  for both 3 and 4 series you will get a warning

```
`Compiling Library for 4-Series Control System: '4SeriesZipcodeToLatLon.dll'...`
`[C:\Users\tgray\Documents\GitHub\CrestronZipCodeToLatLon\Simpl and S+ Example\Zip2LatLong.usp] * WARNING 1019 * (Line 56) - S+ Module [C:\Users\tgray\Documents\GitHub\CrestronZipCodeToLatLon\Simpl and S+ Example\Zip2LatLong.usp]: Logic excluded as a result of #if_series3 directive`
`Total Error(s): 0`
`Total Warning(s): 1`
`Linking (4-Series)...`
`Total Nonvolatile Ram used (PRO4): 0 bytes`
`No errors found: SIMPL Windows Symbol Definition updated
```



This is not a problem  the compiler is simply telling you that it excluded code from the process due to the #IF_3SERIES compiler directive.  Yes you could just use the 3 series on the 4 series and call it done,  this is also serving as an example on how you can have a S+ compiled for both processor targets while also not having the same C# library used.   There are times that the more modern C# will be more efficient and a lot easier to code in than the old .net 3.5



## The Code and how this works



I added a csv file to the project as an embedded resource and to copy always. this makes the csv file a part of the compiled dll file that is wrapped into the clz  opening this is not just pointing at a file we actually need to reflect the file to gain access.  Because it's a part of t he assembly we have to create an object of ourselves by using GetExecutingAssembly() and then digging through to find the resource.  WE could hardcode the path, but I am using a lambda to leverage the EndsWith() method on strings so it just gives me the resource with that filename.



After that we simply open the resource using a stream and streamreader to dig through looking for the line that has a matching zipcode string and when it does sets the public variables to contain the proper information and then breaks out of the loop.  Because I used using statements the whole thing cleans up the resources and even closes the file when this happens so we do not have to.   One last thing that is happening is that the astronomical clock symbol can not use a decimal Latitude and Longitude, we have to convert them to DEG,Min and set North and East flags.  a private method does this conversion for us.



## License

Crestron example code is licensed to Crestron dealers and Crestron Service Providers (CSPs) under a limited non-exclusive, non-transferable Software Development Tools License Agreement. Crestron product operating system software is licensed to Crestron dealers, CSPs, and end-users under a separate End-User License Agreement. Both of these Agreements can be found on the Crestron website at www.crestron.com/legal/software-license-agreement. The product warranty can be found at www.crestron.com/legal/sales-terms-conditions-warranties. The specific patents that cover Crestron products are listed at www.crestron.com/legal/patents. Certain Crestron products contain open source software. For specific information, visit www.crestron.com/legal/opensource-software. Crestron, the Crestron logo, Crestron Virtual Control, VC-4, 4-Series, 4-Series Control System, Crestron Studio, Crestron Toolbox, Crestron XiO Cloud, SIMPL+, and VT-Pro e are either trademarks or registered trademarks of Crestron Electronics, Inc. in the United States and/or other countries. Microsoft Visual Studio and Active Directory is either a trademark or a registered trademark of Microsoft Corporation in the United States and/or other countries. Other trademarks, registered trademarks, and trade names may be used in this document to refer to either the entities claiming the marks and names or their products. Crestron disclaims any proprietary interest in the marks and names of others. Crestron is not responsible for errors in typography or photography. ©2024 Crestron Electronics, Inc.











 