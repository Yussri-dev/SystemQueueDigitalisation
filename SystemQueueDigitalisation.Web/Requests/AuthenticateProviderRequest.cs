﻿namespace SystemQueueDigitalisation.Web.Requests
{
    public class AuthenticateProviderRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
