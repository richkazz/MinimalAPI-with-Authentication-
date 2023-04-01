﻿using Domain.Models;
using MediatR;

namespace Application.Posts.Queries
{
    public class GetPostById : IRequest<Post>
    {
        public int PostId { get; set; }
    }
}
