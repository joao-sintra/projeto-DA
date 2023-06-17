﻿namespace CineGest.Migrations {
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CineGest.CinegestContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CineGest.CinegestContext";
        }

        protected override void Seed(CineGest.CinegestContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
