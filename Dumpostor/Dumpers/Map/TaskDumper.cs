using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace Dumpostor.Dumpers.Map;

public sealed class TaskDumper : IMapDumper
{
    public enum TaskLength
    {
        Common,
        Long,
        Short,
    }

    public string FileName => "tasks.json";

    public string Dump(ShipStatus shipStatus)
    {
        var tasks = new Dictionary<int, TaskInfo>();

        void Handle(NormalPlayerTask task, TaskLength length)
        {
            task.Arrow.DestroyImmediate();
            task.Initialize();

            var taskConsoles = new List<TaskConsole>();

            foreach (var console in shipStatus.AllConsoles.Where(console => console.TaskTypes.Contains(task.TaskType)))
            {
                taskConsoles.Add(new TaskConsole
                {
                    Id = console.ConsoleId,
                    Room = console.Room,
                    Position = console.transform.position,
                    UsableDistance = console.UsableDistance,
                });
            }

            tasks.Add(task.Index, TaskInfo.From(task, length, taskConsoles));
        }

        foreach (var task in shipStatus.CommonTasks)
        {
            Handle(task, TaskLength.Common);
        }

        foreach (var task in shipStatus.ShortTasks)
        {
            Handle(task, TaskLength.Short);
        }

        foreach (var task in shipStatus.LongTasks)
        {
            Handle(task, TaskLength.Long);
        }

        return JsonSerializer.Serialize(
            tasks.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value),
            DumpostorPlugin.JsonSerializerOptions
        );
    }

    public sealed class TaskInfo
    {
        public required string Type { get; init; }
        public required TaskTypes TaskType { get; init; }
        public required TaskLength Length { get; init; }
        public required List<TaskConsole> Consoles { get; init; }

        public static TaskInfo From(NormalPlayerTask task, TaskLength length, List<TaskConsole> consoles)
        {
            return new TaskInfo
            {
                Type = task.GetIl2CppType().Name,
                TaskType = task.TaskType,
                Length = length,
                Consoles = consoles,
            };
        }
    }

    public sealed class TaskConsole
    {
        public required int Id { get; init; }
        public required SystemTypes Room { get; init; }
        public required Vector2 Position { get; init; }
        public required float UsableDistance { get; init; }
    }
}
