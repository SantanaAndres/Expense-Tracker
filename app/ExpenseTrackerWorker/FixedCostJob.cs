using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseRecord;
using Application.Feature.ExpenseRecord.Add;
using Application.Helper;
using Application.Helper.Exceptions;
using Quartz;
using Wolverine;

namespace ExpenseTrackerWorker;

public class FixedCostJob(IFixedCostRepository fixedCostRepository, IMessageBus messageBus, ILogger<FixedCostJob> fixedCostLog): IJob
{
    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        fixedCostLog.LogInformation("Fixed cost job started");
        
        var fixedCostId = context.MergedJobDataMap.GetInt("ScheduleId");
        
        var fixedCost = await fixedCostRepository.GetFixedCostById(fixedCostId);
        
        if (fixedCost == null)
        {
            fixedCostLog.LogError("Fixed cost not found Id {FixedCostId}, error time {ErrorTime}", fixedCostId, TimeProvider.System.GetLocalNow());
            throw new NotFoundException("Fixed cost not found");
        }
        
        fixedCostLog.LogInformation("Fixed cost found Id {FixedCostId}", fixedCostId);
        
        var amountExpenses = fixedCost.AmountExpenses.FirstOrDefault(amountExpense => amountExpense.AmountExpenseId.ToString() == context.MergedJobDataMap.GetString("AmountExpenseId"));

        if (amountExpenses is null)
        {
            fixedCostLog.LogError("FixedCost found {FixedCostId} but amount expense not found Id {AmountExpenseId}, error time {ErrorTime}", fixedCostId, context.MergedJobDataMap.GetString("AmountExpenseId"), TimeProvider.System.GetLocalNow());
            throw new NotFoundException("Amount expense not found");
        }
        
        fixedCostLog.LogInformation("Fixed cost id: {FixedCostId}, amount expense id: {AmountExpenseId}", fixedCostId, context.MergedJobDataMap.GetString("AmountExpenseId"));
        
        var command = new AddExpenseRecordCommand(fixedCost.UserId, amountExpenses.ToRequest(), TimeProvider.System.GetLocalNow());

        await messageBus.InvokeAsync<ExpenseRecordResponse>(command, cancellationToken);
    }
}