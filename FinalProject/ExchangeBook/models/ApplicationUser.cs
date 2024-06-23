﻿namespace ExchangeBook.models
{
    public class ApplicationUser
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int RoleEntityId { get; set; }
    }

}
