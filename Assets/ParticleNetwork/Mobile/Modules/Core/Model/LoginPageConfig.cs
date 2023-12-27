using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public class LoginPageConfig
    {
        [JsonProperty(PropertyName = "projectName")]
        public string ProjectName;

        [JsonProperty(PropertyName = "description")] 
        public string Description;

        [JsonProperty(PropertyName = "imageType")]
        public ImageType ImageType;
        
        [JsonProperty(PropertyName = "data")]
        public string Data;
        

        public LoginPageConfig(string projectName,
            string description, ImageType imageType, string data )
        {
            this.ProjectName = projectName;
            this.Description = description;
            this.ImageType = imageType;
            this.Data = data;
        }
    }
}