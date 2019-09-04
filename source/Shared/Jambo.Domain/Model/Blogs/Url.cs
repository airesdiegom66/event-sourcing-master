﻿namespace Jambo.Domain.Model.Blogs
{
    using Jambo.Domain.Exceptions;
    using System.Runtime.Serialization;

    public class Url
    {
        public string Text { get; private set; }

        public Url(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException("The url field is required");

            this.Text = text;
        }

        public static Url Create(string text)
        {
            return new Url(text);
        }

        public override string ToString()
        {
            return Text.ToString();
        }
    }
}
