﻿using StaticVoid.Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StaticVoid.Blog.Site
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes, IEnumerable<Redirect> redirects)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (var redirect in redirects)
            {
                routes.Add(new Route(redirect.OldRoute.TrimStart('/', '~'), new RedirectRouteHandler(redirect.NewRoute.TrimStart('/', '~'), redirect.IsPermanent)));
            }

			routes.MapRoute(
				name: "Feed",
				url: "feed.{action}",
				defaults: new { controller = "Feed", action = "Atom" }
			);

			routes.MapRoute(
				name: "Post",
				url: "{year}/{month}/{day}/{title}",
				defaults: new { controller = "Post", action = "Display" }
			);

			routes.MapRoute(
				name: "Preview",
				url: "Preview/{id}",
				defaults: new { controller = "Post", action = "Preview" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
		}
	}
}