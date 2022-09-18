using System;
using System.Collections.Generic;
using System.Text;

namespace Yun.Share.Voice.BaseInterface
{
    /// <summary>
    /// 名称
    /// </summary>
    public interface ILabeNames
    {
        string LabeName { get; set; }
    }
    public interface ITitleName
    {
        string Title { get; set; }
    }

    public interface IUserId
    {
        Guid UserId { get; set; }
    }
    public interface IFileId
    {
        Guid FileId { get; set; }
    }
}
