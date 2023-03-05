namespace Dtat.Net.Mail.Abstractions;

public interface IMailSetting
{
	bool Enabled { get; }



	int SmtpClientTimeout { get; }

	int SmtpClientPortNumber { get; }

	bool SmtpClientSslEnabled { get; }

	string? SmtpClientHostAddress { get; }



	string? SmtpUsername { get; }

	string? SmtpPassword { get; }

	bool UseDefaultCredentials { get; }



	string? SenderDisplayName { get; }

	string? SenderEmailAddress { get; }



	string? SupportDisplayName { get; }

	string? SupportEmailAddress { get; }



	string? BccAddresses { get; }

	string? EmailSubjectTemplate { get; }
}
