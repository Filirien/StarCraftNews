namespace StarCraft.Tests
{
    using AutoMapper;
    using StarCraftNews.Web.Infrastructure.Mapping;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }
    }
}
