﻿using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class User : IUser, IEntity<int>
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public virtual List<IPost> Posts { get; set; }
	public virtual List<IReply> Replies { get; set; }
	public bool IsActive { get; set; }
}