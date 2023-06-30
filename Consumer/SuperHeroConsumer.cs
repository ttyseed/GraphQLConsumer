using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQLClientAPI.Collection;
using GraphQLClientAPI.GraphQLInput;
using GraphQLClientAPI.GraphQLIO;
using GraphQLClientAPI.Models;

namespace GraphQLClientAPI.Consumer
{
    public class SuperHeroConsumer
    {
        private readonly IGraphQLClient _client;

        public SuperHeroConsumer(IGraphQLClient client)
        {
            _client = client;
        }


        public async Task<List<ResponseSuperheroType>> GetAllSuperHero()
        {
            var query = new GraphQLRequest
            {
                Query = @"
               query{
  superheroes{
    id,
    name,
    description,
    superpowers{
      superPower,
      description
    }
  }
}"
            };
            var response = await _client.SendQueryAsync<ResponseSuperheroCollectionType>(query);
            return response.Data.Superheroes;
        }

        public async Task<string> CreateHero(Superhero ownerToCreate)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                mutation($newSuperHero:SuperheroInput!){
                    createHero(newSuperHero:$newSuperHero) {
                        name 
                    } 
                }",
                Variables = new { newSuperHero = ownerToCreate }
            };
            var response = await _client.SendMutationAsync<HeroOutput>(query);
            return response.Data.createHero.Name;
        }

        public async Task<List<ResponseSuperheroType>> GetSuperheroesWithOrders()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                        query{
                         superheroes(order: {name: DESC }){
                            name,
                            description,
                            superpowers{
                                superPower,
                                description
                            }
                            },
                          }"
            };
            var response = await _client.SendQueryAsync<ResponseSuperheroCollectionType>(query);
            return response.Data.Superheroes;
        }


        public async Task<List<ResponseSuperheroType>> GetSuperheroesWithOrdersVariable(SuperheroSortInput createHero)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                       query($SuperheroSortInput:  [SuperheroSortInput!]){
                        superheroes(order: $SuperheroSortInput){
                    name,
    description,
    superpowers{
      superPower,
      description
    }
  },
                          }",
                Variables = new { SuperheroSortInput = createHero }
            };
            var response = await _client.SendQueryAsync<ResponseSuperheroCollectionType>(query);
            return response.Data.Superheroes;
        }


        public async Task<ResponseSuperheroType> GetSuperheroesFilter(SuperheroFilterInput createHero)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                      query($superheroFilterInput: SuperheroFilterInput!){
  superheroes(where:$superheroFilterInput ){
id,
    name,
    description,
    superpowers{
      superPower,
      description
    }
  },
                          }",
                Variables = new { superheroFilterInput = createHero }
            };
            var response = await _client.SendQueryAsync<ResponseSuperheroCollectionType>(query);
            return response.Data.Superheroes.FirstOrDefault();
        }

        public async Task<string> UpdateHero(SuperheroDtoVM superheroDtoVM)
        {
            var query = new GraphQLRequest
            {
                Query = @"
              mutation($newSuperHero: SuperheroUpdateInput!){
  updateHero(newSuperHero: $newSuperHero) {
    name
  }
}",
                Variables = new {newSuperHero = superheroDtoVM }
            };
            var response = await _client.SendMutationAsync<HeroOutput>(query);
            return response.Data.updateHero.Name;
        }

        public async Task<bool> RemoveHero(string Id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    mutation($id:String!){
                        deleteHero(id: $id) 
                }",
                Variables = new { id = Id, }
            };
            var response = await _client.SendMutationAsync<HeroOutput>(query);
            return response.Data.deleteHero;

        }
    }

}
