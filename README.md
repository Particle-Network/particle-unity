
![](https://img.shields.io/badge/C%23-%F0%9F%92%AA-blue?style=round)
![GitHub](https://img.shields.io/github/license/silviopaganini/nft-market?style=round)


<img align="right" width="300" src="https://user-images.githubusercontent.com/2645876/189301811-ddd221a9-0e59-46eb-8f99-281307d45948.png"></img>

## **Prerequisites**

Install Unity 2021.3.35f1 or later. Earlier versions may also be compatible but will not be actively supported. 

*(iOS only)* Install the following:

- Xcode 15.0 or higher
- CocoaPods 1.14.3 or higher

Make sure that your Unity project meets these requirements:

- **For iOS** — targets iOS 14 or higher
- **For Android** — Minimum API Level 23 or higher,Targets API level 31 or higher，Pack apk must be with exporting project to Android Studio, [change Java SDK version to 11](https://stackoverflow.com/questions/66449161/how-to-upgrade-an-android-project-to-java-11)


- **For Windows and macOS** - 
Require unity package: 3D WebView for Windows and macOS (Web Browser), version 4.3.3
If you are familiar with web browser in windows platform, you can use other web browser instead.

## 📗 Docs

**Connect**: https://developers.particle.network/api-reference/connect/mobile/unity

**AuthCore**: https://developers.particle.network/api-reference/auth/mobile-sdks/unity

**GUI**: https://developers.particle.network/api-reference/wallet/mobile/unity

**AA**: https://developers.particle.network/api-reference/aa/sdks/mobile/unity

**Windows and macOS**: https://developers.particle.network/api-reference/auth/desktop-sdks/unity

## 🚀 Quick start with DEMO


### 💻 In Unity Editor, we provide a test mode, easy to develop and debug.

In scene: Mobile_WalletDemo

1. Download and open in Unity Editor.

2. Run and click `Connect Demo` button, click `Select Chain` button, select `Ethereum Sepolia`.

3. Click `Init` button to initialize the SDK.

4. Click `Login` button, in test mode, api will return a user info.

5. Click other button to test, you can see all logs in Console.

In scene: Window_AuthDemo

1. Download and open in Unity Editor.
 
2. Click `Particle Init` button to initialize the SDK

3. Click `Login` to login in web view.


### 📲 Export to Real Phone to test.

1. Export project and follow steps in Docs, install project to your phone.
   
2. Use the same steps to test `Auth Demo`.

3. With phone, you can test `Connect Demo` and `GUI Demo`

## 💼 Feedback

If you got some problems, please report bugs or issues.

You can also join our [Discord](https://discord.gg/2y44qr6CR2).
