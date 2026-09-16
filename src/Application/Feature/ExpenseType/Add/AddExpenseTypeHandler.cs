using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;
using FluentValidation;

namespace Application.Feature.ExpenseType.Add;

public record AddExpenseTypeCommand(string ExpenseTypeName);


public class AddExpenseTypeHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public async Task<ExpenseTypeDataResponse> HandleAsync(AddExpenseTypeCommand command)
    {
        var result = await expenseTypeRepository.AddExpenseType(command);   
        
        return new ExpenseTypeDataResponse(result.ExpenseTypeId, result.ExpenseName, true);
    }
}


public class AddExpenseTypeValidator: AbstractValidator<AddExpenseTypeCommand>
{
    public AddExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}