namespace GraphQLClientAPI.GraphQLIO
{
    public class StringOperationFilterInput
    {
        public List<StringOperationFilterInput> and { get; set; }
        public List<StringOperationFilterInput> or { get; set; }
        public string eq { get; set; }
        public string neq { get; set; }
        public string contains { get; set; }
        public string ncontains { get; set; }
        public List<string> In { get; set; }
        public List<string> nin { get; set; }
        public string startsWith { get; set; }
        public string nstartsWith { get; set; }
        public string endsWith { get; set; }
        public string nendsWith { get; set; }
    }
}
