using System;
using System.Collections.Generic;
using System.Linq;
using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Utils
{
    public class ParticleTools
    {
        public static List<String> GetSupportAuthTypeValues(SupportAuthType supportAuthTypes)
        {
            List<String> authTypeList = new List<string>();
            if (supportAuthTypes == SupportAuthType.NONE)
            {
                return authTypeList;
            }

            if (supportAuthTypes == SupportAuthType.ALL)
            {
                authTypeList.Add(SupportAuthType.ALL.ToString());
                return authTypeList;
            }

            foreach (SupportAuthType item in Enum.GetValues(typeof(SupportAuthType)))
            {
                if (item == SupportAuthType.ALL)
                {
                    continue;
                }

                if ((item & supportAuthTypes) != 0)
                {
                    if (!authTypeList.Contains(item.ToString()))
                    {
                        authTypeList.Add(item.ToString());
                    }
                }
            }

            return authTypeList;
        }
    }
}