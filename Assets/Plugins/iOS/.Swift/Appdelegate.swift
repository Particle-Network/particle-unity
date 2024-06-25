//
//  Appdelegate.swift
//  Unity-iPhone
//
//  Created by link on 2022/6/30.

import ParticleConnect
import ParticleNetworkBase
import ParticleWalletGUI
import UIKit

@main
class AppDelegate: UIResponder, UIApplicationDelegate {
    var window: UIWindow?

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        let rootVC = UIStoryboard(name: "LaunchScreen-iPhone", bundle: nil).instantiateInitialViewController()
        window = UIWindow(frame: UIScreen.main.bounds)
        window?.rootViewController = rootVC
        window?.makeKeyAndVisible()

        UnityManager.shared.startGame()

        return true
    }

    func application(_ app: UIApplication, open url: URL, options: [UIApplication.OpenURLOptionsKey: Any] = [:]) -> Bool {
        // Because of default value of ParticleWalletGUI.supportWalletConnect is ture.
        // It means you support wallet connect as a wallet.
        // If you want other apps open your app from scheme, you should set the scheme parameter
        // otherwise you don't need to call ParticleWalletGUI.handleWalletConnectUrl,
        // just call ParticleConnect.handleUrl or ParticleAuthService.handleUrl as usual.
        let scheme = ""
        if ParticleWalletGUI.handleWalletConnectUrl(url, withScheme: scheme) {
            return true
        } else {
            // if integrate with ParticleConnect, call ParticleConnect.handleUrl(url)
            if ParticleConnect.handleUrl(url) {
                return true
            }
        }

        return true
    }
}
