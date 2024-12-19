using SummerForum.Api.DataAccess.Repositories;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess;

public class UnitOfWork : IDisposable
{

	private readonly SummerForumDbContext _context;
	private IPostRepository _postRepository;
	private IUserRepository _userRepository;
	private IDiscussionRepository _discussionRepository;
	private IReplyRepository _replyRepository;
	private IDepartmentRepository _departmentRepository;
	private bool _disposed = false;


	public UnitOfWork(SummerForumDbContext context)
	{
		_context = context;
	}

	public IDepartmentRepository DepartmentRepository
	{
		get
		{
			if (_departmentRepository == null)
			{
				_departmentRepository = new DepartmentRepository(_context);
			}
			return _departmentRepository;
		}
	}

	public IPostRepository PostRepository
	{
		get
		{
			if(_postRepository == null)
			{
				_postRepository = new PostRepository(_context);
			}
			return _postRepository;
		}
	}

	public IUserRepository UserRepository
	{
		get
		{
			if(_userRepository == null)
			{
				_userRepository = new UserRepository(_context);
			}
			return _userRepository;
		}
	}

	public IDiscussionRepository DiscussionRepository
	{
		get
		{
			if(_discussionRepository == null)
			{
				_discussionRepository = new DiscussionRepository(_context);
			}
			return _discussionRepository;
		}
	}

	public IReplyRepository ReplyRepository
	{
		get
		{
			if(_replyRepository == null)
			{
				_replyRepository = new ReplyRepository(_context);
			}
			return _replyRepository;
		}
	}

	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}

	protected void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
		_disposed = true;
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}



}

