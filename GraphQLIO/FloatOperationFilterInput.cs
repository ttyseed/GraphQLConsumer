namespace GraphQLClientAPI.GraphQLIO
{
    public class FloatOperationFilterInput
    {
        public float? Eq { get; set; }
        public float? Neq { get; set; }
        public List<float>? In { get; set; }
        public List<float>? Nin { get; set; }
        public float? Gt { get; set; }
        public float? Ngt { get; set; }
        public float? Gte { get; set; }
        public float? Ngte { get; set; }
        public float? Lt { get; set; }
        public float? Nlt { get; set; }
        public float? Lte { get; set; }
        public float? Nlte { get; set; }
    }
}
