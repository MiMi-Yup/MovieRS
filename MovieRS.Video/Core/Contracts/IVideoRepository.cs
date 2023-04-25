using MovieRS.Video.Dtos;

namespace MovieRS.Video.Core.Contracts
{
    public interface IVideoRepository
    {
        Task<StatusVideoDto?> GetStatus(long id);
        Task<FileInfo?> GetVideo(long id);
    }
}
