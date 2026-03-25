using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handler;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction()
            {
                Title = request.Title,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Type = request.Type,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                UserId = request.UserId
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?> (transaction, 201, "Transação criada com sucesso.");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "A007 - Falha ao criar a transação.");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = context
                .Transactions
                .AsNoTracking()
                .FirstOrDefault(transaction =>
                    transaction.Id == request.Id
                    && transaction.UserId == request.UserId);

            if (transaction == null)
                return new Response<Transaction?>(null, 404, "A008 - Transação não encontrada.");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação excluída com sucesso.");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "A011 - Falha ao excluir a transação.");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(transaction =>
                    transaction.Id == request.Id
                    && transaction.UserId == request.UserId);

            return transaction is null
                ? new Response<Transaction?>(null, 404, "A009 - Transação não encontrada.")
                : new Response<Transaction?>(transaction, 200, "Transação obtida com sucesso.");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "A012 - Falha ao obter a transação.");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??=  DateTime.UtcNow.GetFirstDay();
            request.EndDate ??= DateTime.UtcNow.GetLastDay();

            var query = context
                .Transactions
                .AsNoTracking()
                .Where(transaction =>
                    transaction.CreatedAt >= request.StartDate
                    && transaction.CreatedAt <= request.EndDate
                    && transaction.UserId == request.UserId);

            var transactions = await query
               .Skip((request.PageNumber - 1) * request.PageSize)
               .Take(request.PageSize)
               .ToListAsync();

            var count = query.Count();
          
            return new PagedResponse<List<Transaction>?>(
                transactions,
                count,
                request.PageNumber,
                request.PageSize
                );
        }
        catch
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "A014 - Falha ao obter transações.");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(transaction =>
                    transaction.Id == request.Id
                    && transaction.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "A010 - Transação não encontrada.");

            transaction.Title = request.Title;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Amount = request.Amount;
            transaction.CategoryId = request.CategoryId;
            transaction.Type = request.Type;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso.");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "A013 - Falha ao atualizar a transação.");
        }
    }
}
