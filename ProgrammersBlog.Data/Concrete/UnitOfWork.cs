﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete.EntityFramework.Repositories;

namespace ProgrammersBlog.Data.Concrete
{
   public class UnitOfWork:IUnitOfWork
   {
       private readonly ProgrammersBlogContext _context;
       private EfArticleRepository _articleRepository;
       private EfCatedosyRepository _categoryRepository;
       private EfCommentRepository _commentRepository;
       private EfRoleRepository _roleRepository;
       private EfUserRepository _userRepository;
       public UnitOfWork(ProgrammersBlogContext context)
       {
           _context = context;
       }

     

       public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);
       public ICategoryRepository Categories => _categoryRepository ?? new EfCatedosyRepository(_context);
       public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);
       public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_context);
       public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
   }
}
