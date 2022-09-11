namespace Toyin_group_api.Core.Entities
{

    public class Jwt
    {

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
