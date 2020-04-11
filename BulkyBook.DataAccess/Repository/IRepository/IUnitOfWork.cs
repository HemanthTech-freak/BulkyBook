using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }

        ICompanyRepository Company { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IProductRepository Product { get; }
        ISP_Call SP_Call { get; }

        void save();
    }
}
