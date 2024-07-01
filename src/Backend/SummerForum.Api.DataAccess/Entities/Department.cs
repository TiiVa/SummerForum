﻿using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Department : IDepartment, IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public virtual List<IDiscussion> Discussions { get; set; }
}