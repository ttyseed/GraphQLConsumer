using GraphQLClientAPI.Consumer;
using GraphQLClientAPI.GraphQLIO;

namespace GraphQLClientAPI.GraphQLInput
{
    public class HeroOutput
    {
        public CreateHero createHero { get; set; }
        public UpdateHero updateHero { get; set; }
        public bool deleteHero { get; set; }
    }
}
