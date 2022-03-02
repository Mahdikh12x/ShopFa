﻿using _0_Framework.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long , Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
