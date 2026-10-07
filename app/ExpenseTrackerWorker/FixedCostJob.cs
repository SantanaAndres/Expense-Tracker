using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseRecord;
using Application.Feature.ExpenseRecord.Add;
using Application.Helper;
using Application.Helper.Exceptions;
using Quartz;
using Wolverine;

namespace ExpenseTrackerWorker;

public class FixedCostJob(IFixedCostRepository fixedCostRepository, IMessageBus bus, ILogger<FixedCostJob> logger): IJob
{
    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running FixedCostJob");

        int? fixedCostId = context.MergedJobDataMap.GetInt("FixedCostId");
        Guid? amountRequestGuid = Guid.Parse(context.MergedJobDataMap.GetString("AmountRequestGuid"));

        if(fixedCostId is null || fixedCostId == 0)
        {
            logger.LogError("FixedCostId is null");
            throw new NullReferenceException("FixedCost not found");
        }

        if(amountRequestGuid is null)
        {
            logger.LogError("AmountRequestGuid is null");
            throw new NullReferenceException("AmountRequest Guid not found");
        }

        var fixedCostData = await fixedCostRepository.GetFixedCostById(fixedCostId.Value, cancellationToken);

        if(fixedCostData is null)
        {
            logger.LogError("FixedCost not found for Id: {fixedCostId}", fixedCostId);
            throw new NotFoundException("FixedCost not found");
        }

        logger.LogInformation("fixedCostData: {fixedCostData}", fixedCostData);

        var expenseRecord = fixedCostData.AmountExpenses.FirstOrDefault(expenses => expenses.ExpenseRecordGuid == amountRequestGuid);

        if(expenseRecord is null)
        {
            logger.LogError("Amount Expense record not found for guid: {amountRequestGuid}", amountRequestGuid);
            throw new NotFoundException("Expense record not found");
        }

        logger.LogInformation("expenseRecord: {expenseRecord}", expenseRecord);

        AddExpenseRecordCommand command = new AddExpenseRecordCommand(fixedCostData.UserId, expenseRecord.ToRequest(), TimeProvider.System.GetLocalNow());

        logger.LogInformation("Sending request to AddExpenseRecordCommand");

        logger.LogInformation("Command: {command}", command);

        var result = await bus.InvokeAsync<ExpenseRecordResponse>(command, cancellationToken);

        logger.LogInformation("Response: {response}", result);
    }
}
