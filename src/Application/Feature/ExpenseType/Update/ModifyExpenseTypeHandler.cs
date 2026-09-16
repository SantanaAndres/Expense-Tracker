using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;
using Application.Helper;
using Application.Helper.Exceptions;
using FluentValidation;

namespace Application.Feature.ExpenseType.Update;

public record ModifyExpenseTypeCommand(int Id, string ExpenseTypeName, bool IsActive);

public class ModifyExpenseTypeHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public async Task<ExpenseTypeDataResponse> HandleAsync(ModifyExpenseTypeCommand command)
    {
        var expenseType = await expenseTypeRepository.GetExpenseTypeById(command.Id);
        
        if (expenseType == null) throw new NotFoundException("ExpenseType not found");
        
        var result = await expenseTypeRepository.ModifyExpenseType(command);
        
        return result.ToDto();
    }
}

public class ModifyExpenseTypeValidator: AbstractValidator<ModifyExpenseTypeCommand>
{
    public ModifyExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}