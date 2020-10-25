using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Music.dto;
using Music.db;


namespace Music.dao.impl
{
    class SqlUserDao : IUserDao
    {
        private static int NOT_PARAM = -1;
        public void create(DUser t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.UserOperation((int)UserOperation.CREATE, NOT_PARAM, t.Login, t.Password, t.Role);
                context.SaveChanges();
            }
        }

        public void delete(DUser t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.UserOperation((int)UserOperation.DELETE, t.Id);
                context.SaveChanges();
            }
        }

        public List<DUser> getAllRegisterUser()
        {
           
            using (TestDbContext context = new TestDbContext())
            {
                var usersFormDb = context.UserOperation((int)UserOperation.GET_ALL_REGISTERED);
                return usersFormDb.Select(x => new DUser
                {
                    Id = x.Id,
                    Login = x.Login,
                    Password = x.Password,
                    Role = x.Role

                }).ToList();
            }

        }

        public List<DUser> getAllUser()
        {
            throw new NotImplementedException();
        }

        public bool isExist(DUser user)
        {

            using (TestDbContext context = new TestDbContext())
            {
                var userFromDb = context.UserOperation((int)UserOperation.IS_EXIST, NOT_PARAM, user.Login, user.Password).GetEnumerator();
                return userFromDb.MoveNext();


            }

        }

        public bool isRegistered(DUser user)
        {
            using (TestDbContext context = new TestDbContext())
            {

                var userFromDb = context.UserOperation((int)UserOperation.IS_REGISTERED, NOT_PARAM, user.Login).GetEnumerator();

                return userFromDb.MoveNext();


            }

        }

        public DUser readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var userFromDb = context.UserOperation((int)UserOperation.READ, id).GetEnumerator();
                userFromDb.MoveNext();
                return userFromDb.Current;


            }

        }

        public DUser readByLogin(string name)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var userFromDb = context.UserOperation((int)UserOperation.READ_BY_LOGIN, NOT_PARAM, name).GetEnumerator();
                userFromDb.MoveNext();
                return userFromDb.Current;

            }

        }

        public DUser update(int id, DUser t)
        {

            using (TestDbContext context = new TestDbContext())
            {
                var userFromDb = context.UserOperation((int)UserOperation.UPDATE, id, t.Login, t.Password, t.Role).GetEnumerator();
                userFromDb.MoveNext();

                return userFromDb.Current;

            }

        }
    }
}
