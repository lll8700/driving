using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Models.Interface
{
    public interface ModelName
    {
        public string Name { get; set; }
    }

    public interface ModelTitle
    {
        public string Title { get; set; }
    }

    public interface ModelPracticeId
    {
        public Guid PracticeId { get; set; }
    }

    public interface UserId
    {
        public Guid UserId { get; set; }
    }
}
