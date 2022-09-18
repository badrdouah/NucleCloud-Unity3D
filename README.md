# [Nucle Cloud](https://nucle.cloud) Unity3D

In order to make life easier for you, we have created a Nucle Cloud Unity 3D plugin that you can download and use.
This tool will allow you instant access to the Nucle Cloud API service, you will be writing less lines of code and save a lot of time.

 
## Instalation 

Extract the nucle.cloud.unitypacakge in your project.   
<img src="https://i.imgur.com/z4hbVQ6.png" width="300"/>.  

To access the nucle cloud unity3D plugin interface.  
<img src="https://i.imgur.com/tODN6OE.png" width="300"/>.  

Nucle Cloud Unity3D plugin interface.   
<img src="https://www.nucle.cloud/media/Unity3DPlugin.png" height="360" width="300"/>.  


## Content
First thing to do when using the nucle cloud unity 3d plugin is to import the namespace like bellow

 `using Nucle.Cloud;`

## Scripting 

Check the .Net library [repository](https://github.com/nuclecloud/dotnet).

## Example

Create a new user and print its id. 
```
using Nucle.Cloud;

var projectId= "b943*************************c173";
var newUser = await User.Create(projectId, "ross88@gmail.com", "P@ssw0rd", "ross") ;
Debug.Log("New user id= " + newUser.id);
```

Login a user and print its token.
```
using Nucle.Cloud;

var projectId= "b943*************************c173";
var loginResult = await User.Login(projectId, "ross88@gmail.com", "P@ssw0rd");
Debug.Log("user Token= " + loginResult.userToken);
```
## GitHub 

You can always check the source code on [GitHub](https://github.com/nuclecloud/dotnet), report any bugs or contribute if you would like.

