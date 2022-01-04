using Newtonsoft.Json;

namespace Truphone.Device.Service.CC.Common.Mapper
{
    public static class GenericMapper
    {
        public static T MapTo<T>(this object from) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(from));
        }
    }
}