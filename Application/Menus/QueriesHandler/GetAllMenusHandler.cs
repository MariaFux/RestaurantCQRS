using Application.Enums;
using Application.Menus.Queries;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.QueriesHandler
{
    internal class GetAllMenusHandler : IRequestHandler<GetAllMenus, OperationResult<List<Menu>>>
    {
        private readonly DataContext _dataContext;
        public GetAllMenusHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<List<Menu>>> Handle(GetAllMenus request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Menu>>();
            try
            {
                var menu = await _dataContext.Menus.ToListAsync();
                result.Payload = menu;
            }
            catch (Exception ex)
            {
                var error = new Error { Code = ErrorCode.UnknownError, Message = $"{ex.Message}" };
                result.IsError = true;
                result.Errors.Add(error);
            }
            return result;
        }
    }
}
