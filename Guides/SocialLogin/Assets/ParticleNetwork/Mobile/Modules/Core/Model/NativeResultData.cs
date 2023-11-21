using Newtonsoft.Json;

public class NativeResultData
{
    public NativeResultData(bool isSuccess, string data)
    {
        this.isSuccess = isSuccess;
        this.data = data;
    }

    public bool isSuccess;
    public string data;
}

/*
  {
     "chain_name": "Solana",
     "chain_id": 1,
     "chain_id_name":"Mainnet"
 }
 */
class NativeChainInfo
{
    [JsonProperty(PropertyName = "chain_name")]
    public string chainName;

    [JsonProperty(PropertyName = "chain_id")]
    public int chainId;
}