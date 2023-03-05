using System.Linq;

namespace Dtat.Net.Mail;

public static class Utility
{
	static Utility()
	{
	}

	/// <summary>
	/// تبدیل متن نامه به گونه‌ای که برای متن ایمیل مناسب باشد
	/// </summary>
	/// <param name="text">متن نامه</param>
	/// <returns></returns>
	public static string CompatibleTextForMailBody(string text)
	{
		if (text == null)
		{
			return string.Empty;
		}

		text =
			text
			.Replace(oldValue: System.Convert.ToChar(13).ToString(), newValue: "<br />") // Return Key.
			.Replace(oldValue: System.Convert.ToChar(10).ToString(), newValue: string.Empty) // Return Key.
			.Replace(oldValue: System.Convert.ToChar(9).ToString(), newValue: "&nbsp;&nbsp;&nbsp;&nbsp;"); // TAB Key.

		return text;
	}

	/// <summary>
	/// ارسال نامه
	/// </summary>
	/// <param name="subject">موضوع</param>
	/// <param name="body">متن</param>
	/// <param name="mailSetting">تنظیمات</param>
	/// <returns></returns>
	public static void Send
		(
			string subject,
			string body,
			Abstractions.IMailSetting mailSetting,
			System.Net.Mail.MailPriority priority = System.Net.Mail.MailPriority.High
		)
	{
		Send(sender: null,
			recipients: null,
			subject: subject,
			body: body,
			priority: priority,
			attachmentPathNames: null,
			deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None,
			mailSetting: mailSetting);
	}

	/// <summary>
	/// ارسال نامه
	/// </summary>
	/// <param name="recipient">دریافت کننده</param>
	/// <param name="subject">موضوع</param>
	/// <param name="body">متن</param>
	/// <param name="priority">اهمیت</param>
	/// <param name="mailSetting">تنظیمات</param>
	/// <returns></returns>
	public static void Send
		(
			System.Net.Mail.MailAddress recipient,
			string subject,
			string body,
			Abstractions.IMailSetting mailSetting,
			System.Net.Mail.MailPriority priority = System.Net.Mail.MailPriority.Normal
		)
	{
		// **************************************************
		var recipients =
			new System.Net.Mail.MailAddressCollection();

		if (recipient != null)
		{
			recipients.Add(item: recipient);
		}
		// **************************************************

		Send(sender: null,
			recipients: recipients,
			subject: subject,
			body: body,
			priority: priority,
			attachmentPathNames: null,
			deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.None,
			mailSetting: mailSetting);
	}

	/// <summary>
	/// ارسال نامه
	/// </summary>
	/// <param name="sender">فرستنده</param>
	/// <param name="recipients">گیرندگان</param>
	/// <param name="subject">موضوع</param>
	/// <param name="body">متن</param>
	/// <param name="priority">اهمیت</param>
	/// <param name="attachmentPathNames">فایل‌های الصاقی</param>
	/// <param name="deliveryNotification">اطلاع‌رسانی از ارسال</param>
	/// <param name="mailSetting">تنظیمات</param>
	/// <returns></returns>
	public static void Send
		(
			System.Net.Mail.MailAddress? sender,
			System.Net.Mail.MailAddressCollection? recipients,
			string? subject,
			string? body,
			Abstractions.IMailSetting mailSetting,
			System.Net.Mail.MailPriority priority = System.Net.Mail.MailPriority.Normal,
			System.Net.Mail.DeliveryNotificationOptions
				deliveryNotification = System.Net.Mail.DeliveryNotificationOptions.None,
			System.Collections.Generic.List<string>? attachmentPathNames = null
		)
	{
		// **************************************************
		System.Net.Mail.SmtpClient? smtpClient = null;
		System.Net.Mail.MailMessage? mailMessage = null;
		// **************************************************

		try
		{
			// **************************************************
			if (mailSetting == null)
			{
				var errorMessage =
					$"{nameof(mailSetting)} is null!";

				throw new System.Exception(message: errorMessage);
			}

			if (mailSetting.Enabled == false)
			{
				var errorMessage =
					$"{nameof(mailSetting)} is disabled!";

				throw new System.Exception(message: errorMessage);
			}
			// **************************************************

			// **************************************************
			// *** Mail Message Configuration *******************
			// **************************************************
			mailMessage =
				new System.Net.Mail.MailMessage();

			// **************************************************
			mailMessage.To.Clear();
			mailMessage.CC.Clear();
			mailMessage.Bcc.Clear();
			mailMessage.Attachments.Clear();
			mailMessage.ReplyToList.Clear();
			// **************************************************

			// **************************************************
			if (sender == null)
			{
				if (string.IsNullOrWhiteSpace
					(value: mailSetting.SenderEmailAddress))
				{
					var errorMessage =
						$"{nameof(mailSetting.SenderEmailAddress)} is null!";

					throw new System.Exception(message: errorMessage);
				}

				if (string.IsNullOrWhiteSpace
					(value: mailSetting.SenderDisplayName))
				{
					sender =
						new System.Net.Mail.MailAddress
							(address: mailSetting.SenderEmailAddress,
							displayName: mailSetting.SenderEmailAddress,
							displayNameEncoding: System.Text.Encoding.UTF8);
				}
				else
				{
					sender =
						new System.Net.Mail.MailAddress
							(address: mailSetting.SenderEmailAddress,
							displayName: mailSetting.SenderDisplayName,
							displayNameEncoding: System.Text.Encoding.UTF8);
				}
			}

			mailMessage.From = sender;
			mailMessage.Sender = sender;

			// Note: Below Code Obsoleted!
			//mailMessage.ReplyTo = sender;

			mailMessage.ReplyToList.Add(item: sender);
			// **************************************************

			// **************************************************
			if (recipients != null)
			{
				// Note: Wrong Usage!
				//mailMessage.To = recipients;

				foreach (var mailAddress in recipients)
				{
					mailMessage.To.Add(item: mailAddress);
				}
			}
			else
			{
				if (string.IsNullOrWhiteSpace
					(value: mailSetting.SupportEmailAddress))
				{
					var errorMessage =
						$"{nameof(mailSetting.SupportEmailAddress)} is null!";

					throw new System.Exception(message: errorMessage);
				}

				System.Net.Mail.MailAddress? mailAddress = null;

				if (string.IsNullOrWhiteSpace
					(value: mailSetting.SupportDisplayName))
				{
					mailAddress =
						new System.Net.Mail.MailAddress
							(address: mailSetting.SupportEmailAddress,
							displayName: mailSetting.SupportEmailAddress,
							displayNameEncoding: System.Text.Encoding.UTF8);
				}
				else
				{
					mailAddress =
						new System.Net.Mail.MailAddress
							(address: mailSetting.SupportEmailAddress,
							displayName: mailSetting.SupportDisplayName,
							displayNameEncoding: System.Text.Encoding.UTF8);
				}

				mailMessage.To.Add(item: mailAddress);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace
				(value: mailSetting.BccAddresses) == false)
			{
				var bccAddresses =
					mailSetting.BccAddresses
					.Replace(" ", ",")
					.Replace(";", ",")
					.Replace("|", ",")
					.Replace("،", ",");

				while (mailSetting.BccAddresses.Contains(value: ",,"))
				{
					bccAddresses =
						bccAddresses.Replace(oldValue: ",,", newValue: ",");
				}

				var bccAddressesArray =
					bccAddresses
					.Split(separator: ',').Distinct();

				foreach (var bccAddress in bccAddressesArray)
				{
					var founded =
						mailMessage.To
						.Where(current => string.Compare
							(strA: current.Address, strB: bccAddress, ignoreCase: true) == 0)
						.Any();

					if (founded == false)
					{
						var mailAddress = new System.Net
							.Mail.MailAddress(address: bccAddress);

						mailMessage.Bcc.Add(item: mailAddress);
					}
				}

				// Note: [BccAddresses] must be separated with comma character (",")
				//mailMessage.Bcc.Add(item: mailSetting.BccAddresses);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(value: subject))
			{
				var errorMessage = $"Subject is null!";

				throw new System.Exception(message: errorMessage);
			}

			mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

			if (string.IsNullOrWhiteSpace
				(value: mailSetting.EmailSubjectTemplate))
			{
				mailMessage.Subject = subject;
			}
			else
			{
				mailMessage.Subject = string.Format
					(format: mailSetting.EmailSubjectTemplate, arg0: subject);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(value: body))
			{
				var errorMessage = $"Body is null!";

				throw new System.Exception(message: errorMessage);
			}

			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
			// **************************************************

			// **************************************************
			mailMessage.Priority = priority;
			mailMessage.DeliveryNotificationOptions = deliveryNotification;
			// **************************************************

			// **************************************************
			if ((attachmentPathNames != null) && (attachmentPathNames.Count > 0))
			{
				foreach (var attachmentPathName in attachmentPathNames)
				{
					if (System.IO.File.Exists(path: attachmentPathName))
					{
						var attachment = new System.Net.Mail
							.Attachment(fileName: attachmentPathName);

						mailMessage.Attachments.Add(item: attachment);
					}
				}
			}
			// **************************************************

			// **************************************************
			mailMessage.Headers.Add(name: "Dtat.Net.Mail_Version", value: "6.0.0");
			mailMessage.Headers.Add(name: "Dtat.Net.Mail_Url", value: "https://DTAT.ir");
			mailMessage.Headers.Add(name: "Dtat.Net.Mail_Author", value: "Mr. Dariush Tasdighi");
			mailMessage.Headers.Add(name: "Dtat.Net.Mail_Date", value: "1401/12/14 - 2023/03/05");
			// **************************************************
			// *** /Mail Message Configuration ******************
			// **************************************************

			// **************************************************
			// *** Smtp Client Configuration ********************
			// **************************************************
			if (string.IsNullOrWhiteSpace
				(value: mailSetting.SmtpClientHostAddress))
			{
				var errorMessage =
					$"{nameof(mailSetting.SmtpClientHostAddress)} is null!";

				throw new System.Exception(message: errorMessage);
			}

			if (mailSetting.SmtpClientPortNumber <= 0)
			{
				var errorMessage =
					$"{nameof(mailSetting.SmtpClientPortNumber)} should be greater than zero!";

				throw new System.Exception(message: errorMessage);
			}

			if (mailSetting.SmtpClientTimeout < 0)
			{
				var errorMessage =
					$"{nameof(mailSetting.SmtpClientTimeout)} should be greater than or equal to zero!";

				throw new System.Exception(message: errorMessage);
			}

			smtpClient =
				new System.Net.Mail.SmtpClient
				{
					Timeout =
						mailSetting.SmtpClientTimeout,

					Port =
						mailSetting.SmtpClientPortNumber,

					EnableSsl =
						mailSetting.SmtpClientSslEnabled,

					Host =
						mailSetting.SmtpClientHostAddress,

					UseDefaultCredentials =
						mailSetting.UseDefaultCredentials,

					DeliveryMethod =
						System.Net.Mail.SmtpDeliveryMethod.Network,
				};

			smtpClient.DeliveryMethod =
				System.Net.Mail.SmtpDeliveryMethod.Network;

			if (mailSetting.UseDefaultCredentials == false)
			{
				if (string.IsNullOrWhiteSpace(value: mailSetting.SmtpUsername))
				{
					var errorMessage =
						$"{nameof(mailSetting.SmtpUsername)} is null!";

					throw new System.Exception(message: errorMessage);
				}

				// Note: SmtpPassword can be null!

				var networkCredential =
					new System.Net.NetworkCredential
						(userName: mailSetting.SmtpUsername,
						password: mailSetting.SmtpPassword);

				smtpClient.Credentials = networkCredential;
			}
			// **************************************************
			// *** /Smtp Client Configuration *******************
			// **************************************************

			smtpClient.Send(message: mailMessage);
		}
		catch
		{
			throw;
		}
		finally
		{
			mailMessage?.Dispose();
			mailMessage = null;

			smtpClient?.Dispose();
			smtpClient = null;
		}
	}
}
