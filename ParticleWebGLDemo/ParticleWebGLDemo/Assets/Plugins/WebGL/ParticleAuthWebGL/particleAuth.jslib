mergeInto(LibraryManager.library, {
    InitParticleAuth: function (options) {
        const config = UTF8ToString(options);
        console.log('InitParticleAuth', config);
        const script = document.createElement('script');
        script.src = 'https://static.particle.network/sdks/web/auth/1.2.1/auth.min.js';
        script.onload = () => {
            new particleAuth.ParticleNetwork(JSON.parse(config));
        };
        document.head.appendChild(script);
    },

    SetParticleLanguage: function (language) {
        const config = UTF8ToString(language);
        window.particle.setLanguage(config);
    },

    SetParticleFiatCoin: function (fiatCoin) {
        const config = UTF8ToString(fiatCoin);
        window.particle.setFiatCoin(config);
    },

    SetParticleAuthTheme: function (options) {
        const config = UTF8ToString(options);
        window.particle.setAuthTheme(JSON.parse(config));
    },
    
    SetParticleERC4337: function (enable) {
         window.particle.setERC4337(enable);
    },

    LoginWithParticle: async function (options) {
        const config = UTF8ToString(options);
        try {
            const userInfo = await window.particle.auth.login(config ? JSON.parse(config) : undefined);
            SendMessage('ParticleAuth', 'OnLogin', JSON.stringify(userInfo));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnLogin', JSON.stringify({ error }));
        }
    },

    LogoutParticle: async function () {
        try {
            await window.particle.auth.logout();
        } catch (error) {
            // ignore
        }
        SendMessage('ParticleAuth', 'OnLogout');
    },

    IsParticleLoggedIn: function () {
        const isLogin = window.particle.auth.isLogin();
        return isLogin ? 1 : 0;
    },

    GetParticleUserInfo: function () {
        const userInfo = window.particle.auth.getUserInfo();
        const userInfoStr = userInfo ? JSON.stringify(userInfo) : '';
        const bufferSize = lengthBytesUTF8(userInfoStr) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(userInfoStr, buffer, bufferSize);
        return buffer;
    },

    OpenParticleAccountAndSecurity: async function () {
        await window.particle.auth.openAccountAndSecurity();
    },

    GetParticleSecurityAccount: async function () {
        try {
            const securityAccount = await window.particle.auth.getSecurityAccount();
            const securityAccountStr = JSON.stringify(securityAccount);
            SendMessage('ParticleAuth', 'OnGetSecurityAccount', securityAccountStr);
        } catch (error) {
            SendMessage('ParticleAuth', 'OnGetSecurityAccount', JSON.stringify({ error }));
        }
    },

    OpenParticleWallet: function () {
        window.particle.openWallet();
    },

    OpenParticleBuy: function (options) {
        const config = UTF8ToString(options);
        window.particle.openBuy(config ? JSON.parse(config) : undefined);
    },

    GetParticleWalletAddress: function () {
        const chainType = window.particle.getChain().name.toLowerCase() === 'solana' ? 'solana' : 'evm_chain';
        const wallet =  window.particle.auth.getWallet(chainType);
        const walletAddress = wallet ? wallet.public_address : '';
        const bufferSize = lengthBytesUTF8(walletAddress) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(walletAddress, buffer, bufferSize);
        return buffer;
    },

    ParticleSwitchChain: async function (options) {
        const config = UTF8ToString(options);
        try {
            const result = await window.particle.auth.switchChain(JSON.parse(config));
            SendMessage('ParticleAuth', 'OnSwitchChain', JSON.stringify(result));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnSwitchChain', JSON.stringify({ error }));
        }
    },

    ParticleEVMSendTransaction: async function (options) {
        const tx = UTF8ToString(options);
        try {
            const txHash = await window.particle.evm.sendTransaction(JSON.parse(tx));
            SendMessage('ParticleAuth', 'OnEVMSendTransaction', JSON.stringify({ txHash }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnEVMSendTransaction', JSON.stringify({ error }));
        }
    },

    ParticleEVMPersonalSign: async function (options) {
        const message = UTF8ToString(options);
        try {
            const signature = await window.particle.evm.personalSign(message);
            SendMessage('ParticleAuth', 'OnEVMPersonalSign', JSON.stringify({ signature }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnEVMPersonalSign', JSON.stringify({ error }));
        }
    },

    ParticleEVMPersonalSignUniq: async function (options) {
        const message = UTF8ToString(options);
        try {
            const signature = await window.particle.evm.personalSignUniq(message);
            SendMessage('ParticleAuth', 'OnEVMPersonalSignUniq', JSON.stringify({ signature }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnEVMPersonalSignUniq', JSON.stringify({ error }));
        }
    },

    ParticleEVMSignTypedData: async function (options) {
        const typedData = UTF8ToString(options);
        try {
            const signature = await window.particle.evm.signTypedData({ version: 'V4', data: JSON.parse(typedData) });
            SendMessage('ParticleAuth', 'OnEVMSignTypedData', JSON.stringify({ signature }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnEVMSignTypedData', JSON.stringify({ error }));
        }
    },

    ParticleEVMSignTypedDataUniq: async function (options) {
        const typedData = UTF8ToString(options);
        try {
            const signature = await window.particle.evm.signTypedDataUniq(JSON.parse(typedData));
            SendMessage('ParticleAuth', 'OnEVMSignTypedDataUniq', JSON.stringify({ signature }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnEVMSignTypedDataUniq', JSON.stringify({ error }));
        }
    },

    ParticleSolanaSignAndSendTransaction: async function (options) {
        const tx = UTF8ToString(options);
        try {
            const txHash = await window.particle.solana.signAndSendTransaction(tx);
            SendMessage('ParticleAuth', 'OnSolanaSignAndSendTransaction', JSON.stringify({ txHash }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnSolanaSignAndSendTransaction', JSON.stringify({ error }));
        }
    },

    ParticleSolanasSignMessage: async function (options) {
        const message = UTF8ToString(options);
        try {
            const signature = (await window.particle.solana.signMessage(message)).toString('base64');
            SendMessage('ParticleAuth', 'OnSolanasSignMessage', JSON.stringify({ signature }));
        } catch (error) {
            SendMessage('ParticleAuth', 'OnSolanasSignMessage', JSON.stringify({ error }));
        }
    },
});
