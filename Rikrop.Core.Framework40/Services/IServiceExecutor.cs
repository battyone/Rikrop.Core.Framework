using System;
using System.Threading.Tasks;

namespace Rikrop.Core.Framework.Services
{
    public interface IServiceExecutor<out TService>
    {
        Task Execute(Func<TService, Task> action);
        Task<TResult> Execute<TResult>(Func<TService, Task<TResult>> func);
    }
}