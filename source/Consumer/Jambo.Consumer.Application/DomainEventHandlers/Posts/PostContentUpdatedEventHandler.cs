﻿using Jambo.Domain.Exceptions;
using Jambo.Domain.Model.Posts;
using Jambo.Domain.Model.Posts.Events;
using MediatR;
using System;

namespace Jambo.Consumer.Application.DomainEventHandlers.Posts
{
    public class PostContentUpdatedEventHandler : IRequestHandler<PostContentUpdatedDomainEvent>
    {
        private readonly IPostReadOnlyRepository postReadOnlyRepository;
        private readonly IPostWriteOnlyRepository postWriteOnlyRepository;

        public PostContentUpdatedEventHandler(
            IPostReadOnlyRepository postReadOnlyRepository,
            IPostWriteOnlyRepository postWriteOnlyRepository)
        {
            this.postReadOnlyRepository = postReadOnlyRepository ??
                throw new ArgumentNullException(nameof(postReadOnlyRepository));
            this.postWriteOnlyRepository = postWriteOnlyRepository ??
                throw new ArgumentNullException(nameof(postWriteOnlyRepository));
        }
        public void Handle(PostContentUpdatedDomainEvent domainEvent)
        {
            Post post = postReadOnlyRepository.GetPost(domainEvent.AggregateRootId).Result;

            if (post.Version != domainEvent.Version)
                throw new TransactionConflictException(post, domainEvent);

            post.Apply(domainEvent);
            postWriteOnlyRepository.UpdatePost(post).Wait();
        }
    }
}
