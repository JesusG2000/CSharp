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
    class CreateComment : ICommand
    {
        private ICommentService commentService = ServiceFactory.getInstance().GetCommentService();
        public object Execute(object request)
        {
            commentService.create((Comment)request);
            return null;
        }
    }
}
