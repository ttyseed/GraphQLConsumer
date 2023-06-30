namespace GraphQLClientAPI.Collection
{
    public class ResponseSuperheroCollectionType
    {
        public List<ResponseSuperheroType> Superheroes { get; set; }
    }

    public class ResponseSuperheroType
    {
        public string Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public List<Models.Superpower> Superpowers { get; set; } 
    }
}
