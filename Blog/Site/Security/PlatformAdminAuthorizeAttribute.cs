﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StaticVoid.Blog.Data;
using StaticVoid.Blog.Site.Models;
using StaticVoid.Repository;

namespace StaticVoid.Blog.Site.Security
{
    public class PlatformAdminAuthorizeAttribute : FilterAttribute { }

	//filter
	public class PlatformAdminAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Data.Blog> _blogRepository;
        private readonly IRepository<Securable> _securableRepository;
        private readonly ISecurityHelper _securityHelper;

        public PlatformAdminAuthorizeFilter(IRepository<User> userRepository, IRepository<Data.Blog> blogRepository, IRepository<Securable> securableRepository, ISecurityHelper securityHelper)
		{
            _userRepository = userRepository;
            _blogRepository = blogRepository;
            _securableRepository = securableRepository;
            _securityHelper = securityHelper;
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
            if (HttpContext.Current.User.Identity.IsAuthenticated && 
				_securityHelper.CurrentUser != null &&
                !String.IsNullOrWhiteSpace(_securityHelper.CurrentUser.ClaimedIdentifier))
            {
                var currentUser = _userRepository.GetCurrentUser(_securityHelper);
                if (currentUser != null)
                {
                    if (currentUser.IsPlatformAdmin(_securableRepository))
                    {
                        return; //no unauthorized
                    }
                }
            }
			filterContext.Result = new HttpUnauthorizedResult();
		}
	}
}