using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace CodeLuau
{
	public class Speaker
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int? Exp { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
		public List<Session> Sessions { get; set; }
		public RegisterResponse Register(IRepository repository)
		{
            if (string.IsNullOrEmpty(FirstName)) { return new RegisterResponse(RegisterError.FirstNameRequired); }
            if (string.IsNullOrEmpty(LastName)) { return new RegisterResponse(RegisterError.LastNameRequired); }
            if (string.IsNullOrEmpty(Email)) { return new RegisterResponse(RegisterError.EmailRequired); }
        
            if (!IsExperiencedSpeaker() ){ return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards); }
            return UserRegistration(repository);
        }

        private bool IsExperiencedSpeaker()
        {
            var employees = new List<string>() { "Pluralsight", "Microsoft", "Google" };
            var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

            return Exp > 10 || HasBlog || Certifications?.Count > 3 || employees.Contains(Employer) ||
                   (!domains.Contains(Email.Split('@').Last()) &&
                    (!(Browser?.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)));
        }

        private int GetRegistrationFee(int? experience)
        {
            int registrationFee;
            if (experience >= 9) { registrationFee = 0; }
            else if (experience >= 6) { registrationFee = 50; }
            else if (experience >= 4) { registrationFee = 100; }
            else if (experience >= 2) { registrationFee = 250; }
            else { registrationFee = 500; }
            return registrationFee;

        }

        private RegisterResponse UserRegistration(IRepository repository)
		{
            if (Sessions.Count() == 0) { return new RegisterResponse(RegisterError.NoSessionsProvided); }
			if (!ApprovedSpeaker()) { return new RegisterResponse(RegisterError.NoSessionsApproved); }

			int? speakerId = null;
			RegistrationFee = GetRegistrationFee(Exp);
			try
			{
				speakerId = repository.SaveSpeaker(this);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
            return new RegisterResponse((int)speakerId);
        }
        
        private bool ApprovedSpeaker()
        {
            bool approvedSpeaker = false;
            var old_technologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
            foreach (var session in Sessions)
            {
                foreach (var tech in old_technologies)
                {
                    session.Approved = session.Title.Contains(tech) || session.Description.Contains(tech) ? false : true;
                    if (!session.Approved) { break; } else { approvedSpeaker = true; }
                }
            }
            return approvedSpeaker;
        }
	}
 }
	

