using AutoMapper;
using Library.Domain.Entities;
using Library.ViewModels.JournalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.AutoMapperConfig
{
    public class AutoMapperForJournal
    {
        public Journal Mapp(JournalUpdateViewModel journal)
        {
            Mapper.Initialize(m => m.CreateMap<JournalUpdateViewModel, Journal>());
            return Mapper.Map<JournalUpdateViewModel, Journal>(journal);
        }
        public JournalUpdateViewModel Mapp(Journal journal)
        {
            Mapper.Initialize(m => m.CreateMap<Journal, JournalUpdateViewModel>());
            return Mapper.Map<Journal, JournalUpdateViewModel>(journal);
        }
    }
}