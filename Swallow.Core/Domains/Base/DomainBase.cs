using System;

namespace Swallow.Core.Domains.Base
{
    public class DomainBase<TId>
    {
        public TId Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}