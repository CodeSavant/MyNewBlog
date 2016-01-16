using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode.Impl;

namespace SimpleBlog.Models
{
    public class User
    {
        private const int WorkFactor = 13;
        // this method is used to prevent timing attacks.
        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }

        // All members must be virtual because NHibernate needs it for the proxies.
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
            //PasswordHash = "IGNORE ME";

        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }


    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            // map to the users table on database.
            Table("users"); 

            // for the primary key. Id column has an identity that why we use the generators.identity
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            // map the rest of the properties of the users to the database table columns. 
            // for the username and email are case insesitive for the mysql database.
            // default name woud be used to map with column of same name.
            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x =>
            {
                x.Column("password_hash");
                x.NotNullable(true);
            });


        }
    }



}