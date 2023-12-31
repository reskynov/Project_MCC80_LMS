﻿using Client.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IGeneralRepository<T, X>
         where T : class
    {
        Task<ResponseHandler<IEnumerable<T>>> GetAll();
        Task<ResponseHandler<T>> Get(X id);
        Task<ResponseHandler<T>> Post(T entity);
        Task<ResponseHandler<T>> Put(X id, T entity);
        Task<ResponseHandler<T>> Delete(X id);
    }
}
