using System.Collections.Generic;
using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Test.Model
{
    public class AccountItem
    {
        public WalletType walletType;
        public List<Account> accounts;

        public AccountItem(WalletType walletType, List<Account> accounts)
        {
            this.walletType = walletType;
            this.accounts = accounts;
        }
    }
}