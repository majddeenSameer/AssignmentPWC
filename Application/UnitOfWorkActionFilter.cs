using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Infrastructure.Database;

namespace Application
{
    public class UnitOfWorkActionFilter : IActionFilter
    {
        private readonly Context _context;
        private readonly IDbContextTransaction _transaction;
        //private readonly IdentityContext _identityContext;
        //private readonly IDbContextTransaction _identityTransaction;
        public UnitOfWorkActionFilter(Context context
            //,IdentityContext identityContext
            )
        {
            _context = context;
            //_identityContext = identityContext;
            //_identityTransaction = _identityContext.Database.BeginTransaction();
            _transaction = _context.Database.BeginTransaction();

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.Exception == null)
            {
                try
                {
                    _transaction.Commit();
                    //_identityTransaction.Commit();
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex.Message);
                    _transaction.Rollback();
                    //_identityTransaction.Rollback();
                }

            }
            else
            {
                _transaction.Rollback();
                //_identityTransaction.Rollback();
            }

        }
    }
}
