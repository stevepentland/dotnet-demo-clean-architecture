using CleanArchitecture.Application.Reminders.Commands.SetReminder;
using CleanArchitecture.Application.Reminders.Queries.GetReminder;
using CleanArchitecture.Application.Reminders.Queries.ListReminders;
using CleanArchitecture.Domain.Reminders;

using ErrorOr;

using FluentAssertions;

using MediatR;

namespace TestCommon.Reminders;

public static class MediatorExtensions
{
    public static async Task<Reminder> SetReminder(
        this IMediator mediator,
        SetReminderCommand? command = null)
    {
        command ??= ReminderCommandFactory.CreateSetReminderCommand();
        var result = await mediator.Send(command);

        result.IsError.Should().BeFalse();
        result.Value.AssertCreatedFrom(command);

        return result.Value;
    }

    public static async Task<ErrorOr<List<Reminder>>> ListReminders(
        this IMediator mediator,
        ListRemindersQuery? query = null)
    {
        return await mediator.Send(query ?? ReminderQueryFactory.CreateListRemindersQuery());
    }

    public static async Task<ErrorOr<Reminder>> GetReminder(
        this IMediator mediator,
        GetReminderQuery? query = null)
    {
        query ??= ReminderQueryFactory.CreateGetReminderQuery(Guid.NewGuid());
        return await mediator.Send(query);
    }
}
