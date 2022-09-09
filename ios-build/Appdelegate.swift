//
//  Appdelegate.swift
//  Unity-iPhone
//
//  Created by link on 2022/6/30.
//
import ParticleAuthService
import ParticleNetworkBase
import ParticleConnect
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
        
        // if use ParticleConnect
        if ParticleConnect.handleUrl(url) {
            return true
            // if use ParticleAuthService
        } else if ParticleAuthService.handleUrl(url) {
            return true
        }
        return true
        // if use ParticleAuthService
        // return ParticleAuthService.handleUrl(url)
    }
}


