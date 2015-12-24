﻿using System.Web.Optimization;

namespace SimpleBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/admin.css"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/admin.css"));

            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive")
                .Include("~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive")
                .Include("~/scripts/bootstrap.js"));




        }
    }
}