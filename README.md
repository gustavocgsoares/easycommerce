#### Easy.Commerce

This project aims to make available the Easy Commerce APIs, obtaining content from the mongo database.

#### Official documentation when the project is running

Login the [website](localhost:81)

Go to [documentation](localhost:8080/docs/)

#### Environments

| Environment    | URL																					|
|----------------|--------------------------------------------------------------------------------------|
| **API**        | [localhost:8080/docs/](http://localhost:8080/docs/)   |
| **WEB**		     | [localhost:81](http://localhost:81)   |

#### Available APIs
| API                        | Status     
|:---------------------------|:-------
|`GetBasketById`      		 | ✔️ |  
|`CreateBasket`     		 | ✔️ |    
|`AddBasketItem` 			 | ✔️ |    
|`RemoveItem` 				 | ✔️ |  

#### Installation
This API whas built with .Net Core --version 2.0.0
Make it sure you have this version or later installed.
If not follow [this instructions](https://www.microsoft.com/net/download/core)

Then, git clone this repository
```bash
  https://github.com/gustavocgsoares/easycommerce.git
```

After cloning, run:
```bash
 cd easycommerce
```

Open Visual Studio 2018
 
#### Developping
On a dev environment run (inside the project's root folder):
set the docker-compose as Startup Project inside Visual Studio 2018
Press F5
Open the link http://localhost:81 to test

Enjoy ;D

## License

MIT © [Easy Commerce](easycommerce.com.br)
