namespace mptc.dgc.sample.azurefunction
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string Email { get; set; }
    }
}
