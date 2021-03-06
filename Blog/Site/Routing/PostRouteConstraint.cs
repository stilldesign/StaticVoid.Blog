﻿using StaticVoid.Blog.Data;
using StaticVoid.Blog.Site.Services;
using StaticVoid.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StaticVoid.Blog.Site.Routing
{
    public class PostRouteConstraint: IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var postRepo = DependencyResolver.Current.GetService<IRepository<Post>>();
            var blogRepo = DependencyResolver.Current.GetService<IRepository<Data.Blog>>();
            var httpContextService = DependencyResolver.Current.GetService<IHttpContextService>();

            return postRepo.IsUrlAPost(blogRepo.GetCurrentBlog(httpContextService).Id, httpContext.Request.Url.LocalPath.TrimStart('/'));
        }
    }
}