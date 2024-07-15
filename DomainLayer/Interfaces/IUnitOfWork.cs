using System;

namespace DomainLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepo AuthorRepo { get; }
        IBookRepo BookRepo { get; }
        void commit();
    }
}