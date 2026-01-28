
    public class AccessLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string IpAddress { get; set; } = string.Empty;

        public DateTime AccessDate { get; set; } = DateTime.UtcNow;
    }
