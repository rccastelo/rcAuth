using rcAuthDomain.Interfaces;
using rcAuthDomain.Models;
using rcUtils;
using System;

namespace rcAuthDomain.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        public AuthModel Permit(AuthModel auth)
        {
            AuthModel ret = new AuthModel();

            ret.IsValidResponse = false;

            DateTime dt = DateTime.Now;

            if ((auth == null) || (auth.Entity == null)) {
                ret.AddMessage("As permissões do usuário não podem ser nulas");
                return ret;
            }

            if (!((auth.Entity.DateFrom.Date <= dt.Date) && (dt.Date <= auth.Entity.DateTo.Date))) {
                ret.AddMessage($"O usuário só pode acessar o sistema de {auth.Entity.DateFrom.Date} a {auth.Entity.DateTo.Date}");
                return ret;
            }

            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) {
                if (!auth.Entity.Weekend) {
                    ret.AddMessage("O usuário não pode acessar o sistema no final de semana");
                    return ret;
                }
            } else {
                if (!auth.Entity.Weekday) {
                    ret.AddMessage("O usuário não pode acessar o sistema durante a semana");
                    return ret;
                }
            }

            if (!((auth.Entity.StartTime <= dt.TimeOfDay) && (dt.TimeOfDay <= auth.Entity.EndTime))) {
                ret.AddMessage($"O usuário só pode acessar o sistema entre {auth.Entity.StartTime} e {auth.Entity.EndTime}");
                return ret;
            }

            ret.IsValidResponse = true;

            return ret;
        }
    }
}
