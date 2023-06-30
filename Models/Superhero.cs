namespace GraphQLClientAPI.Models
{
    public class Superhero
    {

        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class Superpower
    {
        public string SuperPower { get; set; }
        public string Description { get;set; }  
    }

    public class SuperheroDtoVM
    {
        public string Id { get;set; }   
        public string Name { get; set; }        
        public string Description { get; set; } 
    }
}
