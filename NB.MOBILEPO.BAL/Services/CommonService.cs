using Microsoft.AspNetCore.Http;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NB.MOBILEPO.BAL.Services
{
    public class CommonService : ICommonService
    {
        private readonly HttpContext _httpContext;
        protected readonly MobilePoDbContext _dbContext;
        private readonly long _currentUserId = 0;
        private readonly long _currentSupplierId = 0;
        public long CurrentSupplierId { get { return _currentSupplierId; } private set { value = _currentSupplierId; } }
        public long CurrentUserId { get { return _currentUserId; } private set { value = _currentUserId; } }
        
        public CommonService(
            MobilePoDbContext dbContext, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContext = httpContextAccessor.HttpContext;
            _currentUserId = Convert.ToInt64(_httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            _currentSupplierId = Convert.ToInt64(_httpContext.User.Claims.FirstOrDefault(e => e.Type == "SupplierId")?.Value);
        }

        ~CommonService()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public bool IsSameSupplierUser(long? supplierId)
        {
            // _currentSupplierId is 0 when the user is Admin
            return _currentSupplierId == 0 ?
                true : 
                supplierId == null ? 
                false :
                supplierId == _currentSupplierId;
        }
        public bool SaveDbChanges()
        {
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
