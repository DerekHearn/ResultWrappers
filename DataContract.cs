using APPI.Meetball;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//disable XML comment warnings
#pragma warning disable 1591

namespace ResultWrappers
{
	/// <summary>
	/// timezones were always local with c# dateTimes
	/// use this for conversions
	/// </summary>
	public class Date
	{
		private static DateTime nineteenSeventy
		{
			get
			{
				if (_n == null || _n == DateTime.MinValue)
				{
					_n = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				}
				return _n;
			}
		}
		private static DateTime _n;

		public static long ToUTCMiliseconds(DateTime date)
		{
			 return (long)(date - nineteenSeventy).TotalMilliseconds;
		}

		public static string ToString(DateTime date)
		{
			return "/Date(" + ToUTCMiliseconds(date).ToString() + ")/";
		}

		public static DateTime UTCDateFromMiliseconds(long mili)
		{
			return nineteenSeventy.AddMilliseconds(mili);
		}
	}

	[DataContract]
	public struct MeetballConvo
	{
		[DataMember]
		public string ConvoName;

		[DataMember]
		public string MostRecentMessageTime;

		[DataMember]
		public Message[] Messages;

		[DataMember]
		public int Participants;

		public MeetballConvo(string convoName, DateTime mostRecentMessageTime, 
			Message[] messages, int participants)
		{
			ConvoName = convoName;
			MostRecentMessageTime = Date.ToString(mostRecentMessageTime);
			Messages = messages;
			Participants = participants;
		}

		public MeetballConvo(APPI.Meetball.Structs.Convo c)
			: this(c.convoName, c.mostRecentMessageTime, 
			Message.cast(c.messages), c.participants) { }

		public static implicit operator MeetballConvo(APPI.Meetball.Structs.Convo c)
		{
			return new MeetballConvo(c);
		}
	}

	[DataContract]
	public struct Convo
	{
		[DataMember]
		public string ConvoName;

		[DataMember]
		public string MostRecentMessageTime;

		[DataMember]
		public Message[] Messages;

		public Convo(string convoName, DateTime mostRecentMessageTime, Message[] messages)
		{
			ConvoName = convoName;
			MostRecentMessageTime = Date.ToString(mostRecentMessageTime);
			Messages = messages;
		}

		public Convo(APPI.Meetball.Structs.Convo c)
			: this(c.convoName, c.mostRecentMessageTime, Message.cast(c.messages)) { }

		public static implicit operator Convo(APPI.Meetball.Structs.Convo c)
		{
			return new Convo(c);
		}
	}

	[DataContract]
	public class Message
	{
		[DataMember]
		public int SenderID;

		[DataMember]
		public string Handle;

		[DataMember]
		public string DisplayName;

		[DataMember]
		public string MessageText;

		[DataMember]
		public Dictionary<string, string> Data;

		[DataMember]
		public string CreateDate;

		public Message(string message, string handle, string displayName, 
			int senderID, DateTime createDate)
		{
			this.SenderID = senderID;
			this.Handle = handle;
			this.DisplayName = displayName;
			this.MessageText = message;
			this.CreateDate = Date.ToString(createDate);
		}

		public Message(APPI.Meetball.Structs.Message m)
			: this(m.message, m.handle, m.displayName, m.senderID, m.createDate) 
		{
			if (m is APPI.Meetball.Structs.MessageMB)
				initMessageMBDict(m as APPI.Meetball.Structs.MessageMB);
		}

		public Message(APPI.Meetball.Structs.MessageMB mmb)
			: this(mmb.message, mmb.handle, mmb.displayName, mmb.senderID, mmb.createDate)
		{
			initMessageMBDict(mmb);
		}

		private void initMessageMBDict(APPI.Meetball.Structs.MessageMB mmb)
		{
			Data = new Dictionary<string, string>();
			Data.Add("MeetballID", mmb.meetballID.ToString());
		}

		public static implicit operator Message(APPI.Meetball.Structs.Message m)
		{
			return new Message(m);
		}

		public static implicit operator Message(APPI.Meetball.Structs.MessageMB mmb)
		{
			return new Message(mmb);
		}

		public static Message[] cast(APPI.Meetball.Structs.Message[] items)
		{
			var length = items.Length;
			var array = new Message[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[i];
			}

			return array;
		}
	}

	[DataContract]
	public struct ConvoHeader
	{
		[DataMember]
		public int ConvoID;

		[DataMember]
		public int AppUserID;

		[DataMember]
		public string DisplayName;

		[DataMember]
		public string Handle;

		[DataMember]
		public string ConvoName;

		[DataMember]
		public bool NewMessage;

		[DataMember]
		public string LastMessageTime;

		public ConvoHeader(int convoID, int appUserID, string displayName, string handle,
			string convoName, bool newMessage, DateTime lastMessageTime)
		{
			ConvoID = convoID;
			AppUserID = appUserID;
			DisplayName = displayName;
			Handle = handle;
			ConvoName = convoName;
			NewMessage = newMessage;
			LastMessageTime = Date.ToString(lastMessageTime);
		}

		public ConvoHeader(APPI.Meetball.Structs.ConvoHeader ch)
			: this(ch.convoID, ch.appUserID, ch.displayName, ch.handle, 
			ch.convoName, ch.newMessage, ch.lastMessageTime) { }

		public static implicit operator ConvoHeader(APPI.Meetball.Structs.ConvoHeader ch)
		{
			return new ConvoHeader(ch);
		}

		public static ConvoHeader[] cast(APPI.Meetball.Structs.ConvoHeader[] items)
		{
			var length = items.Length;
			var array = new ConvoHeader[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[i];
			}

			return array;
		}

	}

	[DataContract]
	public struct MeetballActivity
	{
		[DataMember]
		string ActionMessage;

		[DataMember]
		string ActionTime;

		public MeetballActivity(string editAction, DateTime timeOfEdit)
		{
			ActionMessage = editAction;
			ActionTime = Date.ToString(timeOfEdit);
		}

		public MeetballActivity(APPI.Meetball.Structs.MeetballActivity ma)
			: this(ma.editAction, ma.timeOfEdit) { }

		public static implicit operator MeetballActivity
			(APPI.Meetball.Structs.MeetballActivity ma)
		{
			return new MeetballActivity(ma);
		}

		public static MeetballActivity[] cast(APPI.Meetball.Structs.MeetballActivity[] items)
		{
			var length = items.Length;
			var array = new MeetballActivity[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[i];
			}

			return array;
		}
	}

	[DataContract]
	public struct Comment
	{
		[DataMember]
		public int SenderID;

		[DataMember]
		public String CommentDate;

		[DataMember]
		public string Message;

		public Comment(int apid, DateTime cdate, string comment)
		{
			SenderID = apid;
			CommentDate = Date.ToString(cdate);
			Message = comment;
		}

		public Comment(APPI.Meetball.Structs.MeetballCommentItem result) :
			this(result.AppUserID, result.CommentDate, result.Comment) { }

		public static implicit operator Comment(
			APPI.Meetball.Structs.MeetballCommentItem mci)
		{
			return new Comment(mci);
		}

		public static Comment[] cast(APPI.Meetball.Structs.MeetballCommentItem[] items)
		{
			var length = items.Length;
			var array = new Comment[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[i];
			}
			
			return array;
		}
	}

	[DataContract]
	public struct GetWebMBResult
	{
		[DataMember]
		public string MeetballGPXWKT;

		[DataMember]
		public string OwnerGPXWKT;

		[DataMember]
		public string LocationName;

		[DataMember]
		public string MeetballAddress;

		[DataMember]
		public string MeetballCity;

		[DataMember]
		public string MeetballState;

		[DataMember]
		public string MeetballName;

		[DataMember]
		public string MeetballDescription;

		[DataMember]
		public string OwnerDisplayName;

		[DataMember]
		public string StartDate;

		[DataMember]
		public string EndDate;

		[DataMember]
		public int MeetballID;

		[DataMember]
		public int OwnerID;

		public GetWebMBResult(string mbGPXWKT, string ownerGPXWKT, string locationName, 
			string mbAddress, string mbCity, string mbState, string mbName, 
			string description, string ownerDisplayName, 
			DateTime startDate, DateTime endDate, int meetballID, int ownerID)
		{
			MeetballGPXWKT = mbGPXWKT;
			OwnerGPXWKT = ownerGPXWKT;
			LocationName = locationName;
			MeetballAddress = mbAddress;
			MeetballCity = mbCity;
			MeetballState = mbState;
			MeetballName = mbName;
			MeetballDescription = description;
			OwnerDisplayName = ownerDisplayName;
			StartDate = Date.ToString(startDate);
			EndDate = Date.ToString(endDate);
			MeetballID = meetballID;
			OwnerID = ownerID;
		}

		public GetWebMBResult(APPI.Meetball.Structs.GetWebMBResult result) : 
			this(result.MeetballGPXWKT, result.OwnerGPXWKT, result.locationName, 
			result.MeetballAddress, result.MeetballCity, result.MeetballState, 
			result.MeetballName, result.description, result.OwnerDisplayName, 
			result.StartDate, result.EndDate, result.MeetballID, result.OwnerID) { }

		public static implicit operator GetWebMBResult(
			APPI.Meetball.Structs.GetWebMBResult gwmbr)
		{
			return new GetWebMBResult(gwmbr);
		}
	}

	[DataContract]
	public struct CreateMeetballResult
	{
		[DataMember]
		public string SiteLink;

		[DataMember]
		public string MeetballName;

		[DataMember]
		public string LocationName;

		[DataMember]
		public int MeetballID;

		public CreateMeetballResult(string siteLink, string meetballName,
			string locationName, int meetballID)
		{
			SiteLink = siteLink;
			MeetballName = meetballName;
			LocationName = locationName;
			MeetballID = meetballID;
		}
	}

	[DataContract]
	public struct VerifyPhoneNumberGetActivityCount
	{
		[DataMember]
		public int ActivityCount;

		[DataMember]
		public GetFriendsResult User;

		[DataMember]
		public string SessionID;

		public VerifyPhoneNumberGetActivityCount(int activityCount, 
			GetFriendsResult user, string sessionID)
		{
			ActivityCount = activityCount;
			User = user;
			SessionID = sessionID;
		}
	}

	[DataContract]
	public struct GetActivityFeedResult
	{
		[DataMember]
		public MeetballAttendees[] OtherAttendees;

		[DataMember]
		public string StartDate;

		[DataMember]
		public string EndDate;

		[DataMember]
		public string CreateDate;

		/// <summary>
		/// date for the front end to sort the list of MBs by
		/// </summary>
		[DataMember]
		public string SortDate;

		[DataMember]
		public string GPXWKT;

		[DataMember]
		public string Address;

		[DataMember]
		public string City;

		[DataMember]
		public string State;

		[DataMember]
		public string LocationName;
		
		[DataMember]
		public string MeetballName;

		[DataMember]
		public string Description;

		[DataMember]
		public string MicroSite;

		[DataMember]
		public int MeetballID;

		[DataMember]
		public int Accuracy;

		[DataMember]
		public int OwnerID;

		[DataMember]
		public bool IsOwner;

		[DataMember]
		public bool Accepted;

		[DataMember]
		public bool HasPicture;

		[DataMember]
		public bool NewUpdates;

		[DataMember]
		public bool NewMessages;

		[DataMember]
		public bool IsBroadcasting;

		[DataMember]
		public bool Private;

		public GetActivityFeedResult(MeetballAttendees[] otherAttendees,
			DateTime startDate, DateTime endDate, DateTime createDate, DateTime sortDate, 
			string gpxwkt, string address, string city, string state, 
			string locationName, string meetballName, string description,
			string microSite, int meetballID, int accuracy, int ownerID, 
			bool isOwner, bool accepted, bool hasPicture, 
			bool newUpdates, bool newMessages, bool isBroadcasting, bool isPrivate)
		{
			OtherAttendees = otherAttendees;
			StartDate = Date.ToString(startDate);
			EndDate = Date.ToString(endDate);
			SortDate = Date.ToString(sortDate);
			CreateDate = Date.ToString(createDate);
			GPXWKT = gpxwkt;
			Address = address;
			City = city;
			State = state;
			LocationName = locationName;
			MeetballName = meetballName;
			Description = description;
			MicroSite = microSite;
			MeetballID = meetballID;
			Accuracy = accuracy;
			OwnerID = ownerID;
			IsOwner = isOwner;
			Accepted = accepted;
			HasPicture = hasPicture;
			NewUpdates = newUpdates;
			NewMessages = newMessages;
			IsBroadcasting = isBroadcasting;
			Private = isPrivate;
		}

		public GetActivityFeedResult(APPI.Meetball.Structs.GetMeetballsStruct gms)
			: this(MeetballAttendees.cast(gms.OtherAttendees), 
			gms.StartDate, gms.EndDate, gms.CreateDate, gms.SortDate,
			gms.GPXWKT, gms.Address, gms.City, gms.State,
			gms.LocationName, gms.MeetballName, gms.Description,
			gms.MicroSite, gms.MeetballID, gms.Accuracy, gms.OwnerID,
			gms.IsOwner, gms.Accepted, gms.HasPicture, gms.NewUpdates,
			gms.NewMessages, gms.IsBroadcasting, gms.isPrivate) { }

		public static implicit operator GetActivityFeedResult(
			APPI.Meetball.Structs.GetMeetballsStruct gms)
		{
			return new GetActivityFeedResult(gms);
		}

		public static GetActivityFeedResult[] cast(
			APPI.Meetball.Structs.GetMeetballsStruct[] gms)
		{
			var length = gms.Length;
			var array = new GetActivityFeedResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = gms[i];
			}

			return array;
		}
	}

	[DataContract]
	public struct MeetballAttendeeInfo
	{
		[DataMember]
		public MeetballAttendees[] Attendees;

		[DataMember]
		public int AllAttendeesCount;

		public MeetballAttendeeInfo(MeetballAttendees[] attendees, int aaCount)
		{
			Attendees = attendees;
			AllAttendeesCount = aaCount;
		}

		public MeetballAttendeeInfo(APPI.Meetball.Structs.MeetballAttendeeInfo ma)
			:this(MeetballAttendees.cast(ma.attendees), ma.allAttendeeCount) {	}

		public static implicit operator MeetballAttendeeInfo(
			APPI.Meetball.Structs.MeetballAttendeeInfo ma)
		{
			return new MeetballAttendeeInfo(ma);
		}

	}

	[DataContract]
	public struct MeetballAttendees
	{
		[DataMember]
		string TrackingDate;

		[DataMember]
		string GPXWKT;

		[DataMember]
		string PhoneNumber;

		[DataMember]
		string DisplayName;

		[DataMember]
		string Handle;

		[DataMember]
		int AppUserID;

		[DataMember]
		bool IsAccepted;

		[DataMember]
		bool IsDeleted;

		[DataMember]
		bool NewMessageFrom;

		public MeetballAttendees(DateTime trackingDate, string gpxwkt,
			string phoneNumber, string displayName, string handle, int appUserID,
			bool isAccepted, bool isDeleted, bool newMessageFrom)
		{
			TrackingDate = Date.ToString(trackingDate);
			GPXWKT = gpxwkt;
			PhoneNumber = phoneNumber;
			DisplayName = displayName != null ? displayName : "";
			Handle = handle != null ? handle : "";
			AppUserID = appUserID;
			IsAccepted = isAccepted;
			IsDeleted = isDeleted;
			NewMessageFrom = newMessageFrom;
		}

		public MeetballAttendees(DateTime trackingDate, string gpxwkt,
			string phoneNumber, string displayName, string handle, int appUserID,
			bool isAccepted, bool isDeleted)
			: this(trackingDate, gpxwkt, phoneNumber, displayName, 
				handle, appUserID, isAccepted, isDeleted, false) { }

		public MeetballAttendees(APPI.Meetball.Structs.MeetballAttendee ma)
			:this(ma.TrackingDate, ma.GPXWKT, ma.PhoneNumber, ma.DisplayName, 
				ma.Handle, ma.AppUserID, ma.IsAccepted, ma.IsDeleted,
				(ma.IsOwner.HasValue && ma.NewFrom.HasValue ?
					(ma.NewFrom.Value) : false)) {	}

		public static implicit operator MeetballAttendees(
			APPI.Meetball.Structs.MeetballAttendee ma)
		{
			return new MeetballAttendees(ma);
		}

		public static MeetballAttendees[] cast(
			APPI.Meetball.Structs.MeetballAttendee[] ma)
		{
			var length = ma.Length;
			var array = new MeetballAttendees[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = ma[i];
			}

			return array;
		}
	}

	[DataContract]
	public struct StartUpResult
	{
		[DataMember]
		public GetActivityFeedResult[] meetballs;

		[DataMember]
		public int activityCount;

		[DataMember]
		public int InboxUnreadConvos;

		public StartUpResult(GetActivityFeedResult[] meetballs, 
			int activityCount, int inboxUnreadConvos)
		{
			this.meetballs = meetballs;
			this.activityCount = activityCount;
			InboxUnreadConvos = inboxUnreadConvos;
		}

		public StartUpResult(APPI.Meetball.Structs.GetMeetballsStruct[] gms, 
			int activityCount, int inboxUnreadConvos)
			: this(GetActivityFeedResult.cast(gms), activityCount, inboxUnreadConvos) { }
	}

	[DataContract]
	public struct GetFriendsResult
	{
		[DataMember]
		public string Handle;

		[DataMember]
		public string FirstName;

		[DataMember]
		public string LastName;

		[DataMember]
		public string DisplayName;

		[DataMember]
		public string PhoneNumber;

		[DataMember]
		public string Email;

		[DataMember]
		public string FacebookID;

		[DataMember]
		public int AppUserID;

		[DataMember]
		public int StatusID;

		[DataMember]
		public bool Activated;

		[DataMember]
		public bool Favorite;

		[DataMember]
		public string Avatar;

		public GetFriendsResult(string pHandle, string pFirstName, string pLastName, string pDisplayName,
				string pPhone, string pEmail, string pFacebookID, string avatar, int pAppUserID,
				int pStatusID, bool pActivated, bool pFavorite)
		{
			Handle = pHandle != null ? pHandle : "";
			FirstName = pFirstName;
			LastName = pLastName;
			DisplayName = pDisplayName != null ? pDisplayName : "";
			PhoneNumber = pPhone;
			Email = pEmail;
			Avatar = avatar;
			FacebookID = pFacebookID;
			AppUserID = pAppUserID;
			StatusID = pStatusID;
			Activated = pActivated;
			Favorite = pFavorite;
		}

		public GetFriendsResult(APPI.Meetball.Structs.GetFriendsStruct gfs)
			:this(gfs.Handle, gfs.FirstName, gfs.LastName, gfs.DisplayName,
				gfs.Phone, gfs.Email, gfs.FacebookId, gfs.Avatar, gfs.AppUserId,
				gfs.StatusId, gfs.Activated, gfs.Favorite) { }

		public static implicit operator GetFriendsResult(APPI.Meetball.Structs.GetFriendsStruct gfs)
		{
			return new GetFriendsResult(gfs);
		}

		public static GetFriendsResult[] cast(APPI.Meetball.Structs.GetFriendsStruct[] gfs)
		{
			var length = gfs.Length;
			var array = new GetFriendsResult[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = gfs[i];
			}

			return array;
		}

	}

	[DataContract]
	public struct FriendManagement
	{
		[DataMember]
		public GetFriendsResult[] Relations;

		[DataMember]
		public string Link;

		public FriendManagement(GetFriendsResult[] relations, string link)
		{
			Relations = relations;
			Link = link;
		}
	}

	[DataContract]
	public struct SearchByHandleResult
	{
		[DataMember]
		public string Handle;

		[DataMember]
		public string DisplayName;

		[DataMember]
		public int AppUserID;

		public SearchByHandleResult(string pHandle, string displayName, int pAppUserID)
		{
			Handle = pHandle;
			DisplayName = displayName != null ? displayName : "";
			AppUserID = pAppUserID;
		}
	}

	[DataContract]
	public struct AppUserStatsResult
	{
		[DataMember]
		public int MBThrownCount;

		[DataMember]
		public int MBAcceptedCount;

		public AppUserStatsResult(int mbThrownCount, int mbAcceptedCount)
		{
			MBThrownCount = mbThrownCount;
			MBAcceptedCount = mbAcceptedCount;
		}
	}

	[DataContract]
	public struct Venue
	{
		[DataMember]
		public string Name;

		[DataMember]
		public string GPXWKT;

		[DataMember]
		public string Address;

		[DataMember]
		public string City;

		[DataMember]
		public string State;

		[DataMember]
		public string Description;

		public Venue(string name, string gpxwkt, string address,
			string city, string state, string description)
		{
			Name = name;
			GPXWKT = gpxwkt;
			Address = address;
			City = city;
			State = state;
			Description = description;
		}

		public Venue(APPI.Meetball.Structs.Venue v)
			: this(v.Name, v.GPXWKT, v.Address, v.City, v.State, v.Description) { }

		public static Venue[] cast(APPI.Meetball.Structs.Venue[] items)
		{
			var length = items.Length;
			var array = new Venue[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = items[i];
			}

			return array;
		}

		public static implicit operator Venue(APPI.Meetball.Structs.Venue v)
		{
			return new Venue(v);
		}
	}

}


