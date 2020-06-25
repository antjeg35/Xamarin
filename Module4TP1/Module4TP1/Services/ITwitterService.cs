using Module4TP1.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module4TP1.Services
{
    interface ITwitterService
    {
        Boolean Authenticate(User user);
        List<Tweet> Tweets { get; }
    }
}
