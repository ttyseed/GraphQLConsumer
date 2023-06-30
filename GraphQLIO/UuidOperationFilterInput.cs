namespace GraphQLClientAPI.GraphQLIO
{
    public class UuidOperationFilterInput
    {
        public Guid? eq { get; set; }
        public Guid? neq { get; set; }
        public List<Guid> @in { get; set; }
        public List<Guid> nin { get; set; }
        public Guid? gt { get; set; }
        public Guid? ngt { get; set; }
        public Guid? gte { get; set; }
        public Guid? ngte { get; set; }
        public Guid? lt { get; set; }
        public Guid? nlt { get; set; }
        public Guid? lte { get; set; }
        public Guid? nlte { get; set; }
    }
}
