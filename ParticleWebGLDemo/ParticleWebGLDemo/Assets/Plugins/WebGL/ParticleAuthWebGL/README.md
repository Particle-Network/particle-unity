# Unity WebGL & Particle Auth

1. Copy `ParticleAuthWebGL` to `Assets/Plugins/WebGL`.
2. Bind `ParticleAuth.cs` to GameObject, named "ParticleAuth", then add `config` property value.
3. Custom WebGL templates, add function `SendMessage` in `index.html`, refer to the following code:

Here is an example config json string, the params refer to [Particle Docs](https://docs.particle.network/developers/auth-service/sdks/web)
```
{"projectId":"34c6b829-5b89-44e8-90a9-6d982787b9c9","clientKey":"c6Z44Ml4TQeNhctvwYgdSv6DBzfjf6t6CB0JDscR","appId":"64f36641-b68c-4b19-aa10-5c5304d0eab3","chainName":"Ethereum","chainId":1}
``` 

```js
//add SendMessage function
var _unityInstance = null;
function SendMessage(objectName, methodName, value) {
    _unityInstance?.SendMessage(objectName, methodName, value);
}

createUnityInstance(canvas, config, (progress) => {
        // loading progress
    }).then((unityInstance) => {
        // set global unityInstance
        _unityInstance = unityInstance;
    });
```

Function params refer to [Particle Docs](https://docs.particle.network/developers/auth-service/sdks/web)

Learn more about [WebGL templates](https://docs.unity3d.com/2020.3/Documentation/Manual/webgl-templates.html).

Learn more about [Interaction with browser scripting](https://docs.unity3d.com/cn/2023.2/Manual/webgl-interactingwithbrowserscripting.html).

## Interaction

```C#

// login
var userInfo = await ParticleAuth.Instance.Login(options);

// logout
var result = await ParticleAuth.Instance.Logout();

// check is logged in
var result = ParticleAuth.Instance.IsLoggedIn();

// get address
var address = ParticleAuth.Instance.GetWalletAddress();

// get user info
var userInfo = ParticleAuth.Instance.GetUserInfo();

// get security account
var securityAccount = await ParticleAuth.Instance.GetSecurityAccount();

// set language
ParticleAuth.Instance.SetLanguage(language)

// set fiat coin
ParticleAuth.Instance.SetFiatCoin(fiatCoin)

// set auth theme
ParticleAuth.Instance.SetAuthTheme(options)

// open wallet
ParticleAuth.Instance.OpenWallet();

// open buy
ParticleAuth.Instance.OpenBuy(options);

// switch chain
var result = await ParticleAuth.Instance.SwitchChain(options);

// evm: send transaction
var result = await ParticleAuth.Instance.EVMSendTransaction(options);

// evm: personal sign
var result = await ParticleAuth.Instance.EVMPersonalSign(options);

// evm: personal sign uniq
var result = await ParticleAuth.Instance.EVMPersonalSignUniq(options);

// evm: sign typed data
var result = await ParticleAuth.Instance.EVMSignTypedData(options);

// evm: sign typed data uniq
var result = await ParticleAuth.Instance.EVMSignTypedDataUniq(options);

// solana: send transaction
var result = await ParticleAuth.Instance.SolanaSignAndSendTransaction(options);

// solana: sign message
var result = await ParticleAuth.Instance.SolanasSignMessage(options);

```
