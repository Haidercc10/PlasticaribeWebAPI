using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using PlasticaribeAPI.Controllers;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;
using Xunit;

namespace PlasticaribeAPI.Tests;

public class ClientesControllerTests
{
    [Fact]
    public async Task GetClientesResumenReporteOT_returns_only_the_projected_customer_fields()
    {
        var expectedId = 101L;
        var source = new[]
        {
            new Clientes
            {
                Cli_Id = expectedId,
                Cli_Nombre = "Cliente de prueba",
                usua_Id = 22,
                Cli_Email = "not-returned@example.test"
            }
        };
        var options = new DbContextOptionsBuilder<dataContext>().Options;
        using var context = new dataContext(options, new ConfigurationBuilder().Build())
        {
            Clientes = new TestDbSet<Clientes>(source)
        };
        var controller = new ClientesController(context);

        var response = await controller.GetClientesResumenReporteOT();

        var customers = Assert.IsAssignableFrom<IEnumerable<ClienteReporteOTDto>>(response.Value);
        var customer = Assert.Single(customers);
        Assert.Equal(expectedId, customer.Cli_Id);
        Assert.Equal("Cliente de prueba", customer.Cli_Nombre);
        Assert.Equal(22, customer.usua_Id);
        Assert.Equal(3, typeof(ClienteReporteOTDto).GetProperties().Length);
    }

    private sealed class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly TestAsyncEnumerable<TEntity> _query;

        public override IEntityType EntityType => throw new NotSupportedException();

        public TestDbSet(IEnumerable<TEntity> source)
        {
            _query = new TestAsyncEnumerable<TEntity>(source);
        }

        Type IQueryable.ElementType => ((IQueryable<TEntity>)_query).ElementType;
        Expression IQueryable.Expression => ((IQueryable<TEntity>)_query).Expression;
        IQueryProvider IQueryable.Provider => ((IQueryable<TEntity>)_query).Provider;

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => _query.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _query.AsEnumerable().GetEnumerator();

        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken) =>
            _query.GetAsyncEnumerator(cancellationToken);
    }

    private sealed class TestAsyncEnumerable<TEntity> : IAsyncEnumerable<TEntity>, IQueryable<TEntity>
    {
        private readonly IQueryable<TEntity> _inner;

        public TestAsyncEnumerable(IEnumerable<TEntity> source) => _inner = source.AsQueryable();
        public TestAsyncEnumerable(Expression expression) => _inner = new EnumerableQuery<TEntity>(expression);

        Type IQueryable.ElementType => _inner.ElementType;
        Expression IQueryable.Expression => _inner.Expression;
        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<TEntity>(_inner.Provider);

        public IEnumerator<TEntity> GetEnumerator() => _inner.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new TestAsyncEnumerator<TEntity>(GetEnumerator());
    }

    private sealed class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var elementType = expression.Type.GetGenericArguments()[0];
            return (IQueryable)Activator.CreateInstance(
                typeof(TestAsyncEnumerable<>).MakeGenericType(elementType),
                expression)!;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new TestAsyncEnumerable<TElement>(expression);

        public object? Execute(Expression expression) => _inner.Execute(expression);
        public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default) =>
            throw new NotSupportedException("Async scalar operations are not used by this test.");
    }

    private sealed class TestAsyncEnumerator<TEntity> : IAsyncEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> _inner;

        public TestAsyncEnumerator(IEnumerator<TEntity> inner)
        {
            _inner = inner;
        }

        public TEntity Current => _inner.Current;
        public ValueTask<bool> MoveNextAsync() => ValueTask.FromResult(_inner.MoveNext());
        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
