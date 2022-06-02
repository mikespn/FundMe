using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Model
{
    public enum ProjectCategory
    {
        [Description("Art")]
        Art,
        [Description("Comics")]
        Comics,
        [Description("Crafts")]
        Crafts,
        [Description("Dance")]
        Dance,
        [Description("Design")]
        Design,
        [Description("Fashion")]
        Fashion,
        [Description("Film & Video")]
        FilmVideo,
        [Description("Food")]
        Food,
        [Description("Games")]
        Games,
        [Description("Journalism")]
        Journalism,
        [Description("Music")]
        Music,
        [Description("Photography")]
        Photography,
        [Description("Publishing")]
        Publishing,
        [Description("Technology")]
        Technology,
        [Description("Theater")]
        Theater
    }
}
