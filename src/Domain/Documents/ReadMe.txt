Role					[0..1]..[0..N]					User

System.Guid Id											System.Guid Id

با نگاه بانک اطلاعاتی

														System.Guid RoleId

با نگاه شیء‌گرایی

														System.Guid RoleId
IList<User> Users										Role Role


IList<User> Users & Role Role -> Navigation Property

														System.Guid? RoleId

virtual IList<User> Users { get; private set; }			virtual Role? Role { get; set; }

- RoleConfiguration:

	builder
		.HasMany(current => current.Users)
		.WithOne(other => other.Role)
		.IsRequired(required: false)
		.HasForeignKey(other => other.RoleId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
PageCategory					[1]..[0..N]				Page

System.Guid Id											System.Guid Id

														[Required]
														System.Guid PageCategoryId

														[Required]
virtual IList<Page> Pages { get; private set; }			virtual PageCategory? PageCategory { get; set; }

	builder
		.HasMany(current => current.Pages)
		.WithOne(other => other.PageCategory)
		.IsRequired(required: true)
		.HasForeignKey(other => other.PageCategoryId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------
User					[1]..[0..N]							Page

System.Guid Id												System.Guid Id

															[Required]
															System.Guid CreatorUserId

															[Required]
virtual IList<Page> CreatedPages { get; private set; }		virtual User? CreatorUser { get; set; }

	builder
		.HasMany(current => current.CreatedPages)
		.WithOne(other => other.CreatorUser)
		.IsRequired(required: true)
		.HasForeignKey(other => other.CreatorUserId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;

															System.Guid VerifierUserId

virtual IList<Page> VerifiedPages { get; private set; }		virtual User? VerifierUser { get; set; }

	builder
		.HasMany(current => current.VerifiedPages)
		.WithOne(other => other.VerifierUser)
		.IsRequired(required: false)
		.HasForeignKey(other => other.VerifierUserId)
		.OnDelete(deleteBehavior:
			Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
		;
-------------------------------------------------------------------------------------------

