using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModel.Posts
{
    public class CreatePostViewModel
    {
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public string? YoutubeUrl { get; set; }
    }
}
