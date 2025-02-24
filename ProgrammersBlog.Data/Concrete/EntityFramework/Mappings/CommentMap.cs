﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
   public class CommentMap:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifeidByName).IsRequired();
            builder.Property(c => c.ModifeidByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Comments");
            builder.HasData(
                new Comment
                {
                    Id = 1,
                    ArticleId = 1,
                    Text = "Lorem ipsum dolor sit amet,consectetur adipiscing elit,sed do eiusmod tempor incididunt u",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedByName = "InitailCreate",
                    CreatedDate = DateTime.Now,
                    ModifeidByName = "InitailCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# makkaklle yorumu ",
                },
                new Comment
                {
                    Id = 2,
                    ArticleId = 2,
                    Text = "Lorem ipsum dolor sit amet,consectetur adipiscinct laborum.",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedByName = "InitailCreate",
                    CreatedDate = DateTime.Now,
                    ModifeidByName = "InitailCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ makkaklle yorumu ",
                },
                new Comment
                {
                    Id = 3,
                    ArticleId = 3,
                    Text = "Lorem ipsum dolor sit amet,consectetur adipiscing elit,sed do eiusmodin reculpa qui officia deserunt mollit anim id est laborum.",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedByName = "InitailCreate",
                    CreatedDate = DateTime.Now,
                    ModifeidByName = "InitailCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JS makkaklle yorumu ",
                }
                );

        }
    }
}
