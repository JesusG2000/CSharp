using Music.dto;
using Music.controller;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.db;

namespace Music.command
{
    class DeleteUser : ICommand
    {
        private IUserService userService = ServiceFactory.getInstance().GetUserService();
      
        public object Execute(object request)
        {
            using (TestDbContext context = new TestDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DUser user = (DUser)request;
                       
                        userService.delete(user);

                       

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return null;
        }
    }
}
