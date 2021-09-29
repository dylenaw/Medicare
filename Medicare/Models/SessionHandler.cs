using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicare.Models
{
    public class SessionHandler
    {

        public static User GetUser(HttpSessionStateBase session)
        {
            return (User)session["user"];
        }        
        public static int GetUserId(HttpSessionStateBase session)
        {
            User user = GetUser(session);
            
            return user==null?-1:GetUser(session).Id;
        }
        
        public static void SetUser(HttpSessionStateBase session,User user)
        {
           session["user"]=user;
        }

        public static bool IsUserSignedIn(HttpSessionStateBase session)
        {
            
            return GetUser(session) != null;
        }        
        public static bool IsUserDoctor(HttpSessionStateBase session)
        {
            User user = GetUser(session);
            return user!=null && user.IsDoctor;
        }
        public static bool IsUserUnapprovedDoctor(HttpSessionStateBase session)
        {
            User user = GetUser(session);
            return user != null && user.IsDoctor && user.DoctorRegistration == null;
        }
        public static bool IsUserApprovedDoctor(HttpSessionStateBase session)
        {
            User user = GetUser(session);
            return user != null && user.IsDoctor && user.DoctorRegistration != null;
        }

        public static bool IsUserAdmin(HttpSessionStateBase session)
        {
            User user = GetUser(session);
            return user!=null && user.IsAdmin;
        } 
        public static void Abandon(HttpSessionStateBase session)
        {
            session.Abandon();
        }
        
    }
}