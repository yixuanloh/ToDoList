﻿using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ToDoList.Authorization.Roles;
using ToDoList.Authorization.Users;
using ToDoList.MultiTenancy;
using ToDoList.ToDoLists;
using ToDoList.ToDoItems;

namespace ToDoList.EntityFrameworkCore
{
    public class ToDoListDbContext : AbpZeroDbContext<Tenant, Role, User, ToDoListDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<TDList> ToDoLists { get; set; }
        public virtual DbSet<TDItem> ToDoItems { get; set; }
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
            : base(options)
        {
        }
    }
}
