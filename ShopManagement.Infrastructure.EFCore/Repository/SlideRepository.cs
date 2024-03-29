﻿using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository:BaseRepository<long,Slide>,ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            var slide = _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                Heading = x.Heading,
                Title = x.Title,
                BtnText = x.BtnText,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
            return slide ??new EditSlide();
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                Id = x.Id,
                IsRemoved = x.IsRemoved,
                Title = x.Title
                
            }).AsNoTracking().ToList();
        }
    }
}
