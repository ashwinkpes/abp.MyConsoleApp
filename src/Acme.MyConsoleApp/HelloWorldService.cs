using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.MyConsoleApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.MyConsoleApp;

public class HelloWorldService : ITransientDependency
{
    public ILogger<HelloWorldService> Logger { get; set; }
    private readonly IRepository<TodoItem, Guid> _todoItemRepository;

    public HelloWorldService(IRepository<TodoItem, Guid> todoItemRepository)
    {
        Logger = NullLogger<HelloWorldService>.Instance;
        _todoItemRepository = todoItemRepository;
    }

    public Task SayHelloAsync()
    {
        Logger.LogInformation("Hello World!");

        var items = _todoItemRepository.GetListAsync().GetAwaiter().GetResult();
        List<TodoItemDto> result =  items
            .Select(item => new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text
            }).ToList();



        return Task.CompletedTask;
    }
}
