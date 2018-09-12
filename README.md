#### Easy.Commerce

This project aims to make available the Easy Commerce APIs, obtaining content from the mongo database.

#### Official documentation

Login the [website](https://dev.easycommerce.com.br)

Go to [documentation](https://dev.easycommerce.com.br/document)

#### Environments

| Environment    | URL																					|
|----------------|--------------------------------------------------------------------------------------|
| **DEV**        | [api-dev.easycommerce.com.br](https://api-dev.easycommerce.com.br/docs/)   |
| **STG**		 | [api-stg.easycommerce.com.br](https://api-stg.easycommerce.com.br/docs/)   |
| **PRD**        | [api.easycommerce.com.br](https://api.easycommerce.com.br/docs/)           |

#### Available APIs
| API                        | Status     
|:---------------------------|:-------
|`GetBasketById`      		 | ✔️ |  
|`CreateBasket`     		 | ✔️ |    
|`AddBasketItem` 			 | ✔️ |    
|`RemoveItem` 				 | ✔️ |   
|`ClearBasket`      		 | ✔️ |   

#### Installation
This API whas built with .Net Core --version 2.0.0
Make it sure you have this version or later installed.
If not follow [this instructions](https://www.microsoft.com/net/download/core)

Then, git clone this repository
```bash
  https://easycommerce.visualstudio.com/Projects/_git/easycommerce
```

After cloning, run:
```bash
 cd easycommerce
 dotnet restore
 dotnet build
```

#### Developping
On a dev environment run (inside the project's root folder):
```bash
  dotnet run
```

Enjoy ;D

## License

MIT © [Easy Commerce](easycommerce.com.br)
