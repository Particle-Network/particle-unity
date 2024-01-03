using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public class LoginPageConfig
    {
        [JsonProperty(PropertyName = "projectName")]
        public string ProjectName;

        [JsonProperty(PropertyName = "description")] 
        public string Description;
        
        [JsonProperty(PropertyName = "imagePath")]
        public string ImagePath;
        

        public LoginPageConfig(string projectName,
            string description,string imagePath )
        {
            this.ProjectName = projectName;
            this.Description = description;
            this.ImagePath = imagePath;
        }
    }
}