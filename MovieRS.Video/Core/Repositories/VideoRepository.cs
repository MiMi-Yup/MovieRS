using MovieRS.Video.Core.Contracts;
using MovieRS.Video.Dtos;
using MovieRS.Video.Models;

namespace MovieRS.Video.Core.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DatabaseContext _context;
        private static readonly IDictionary<long, FileInfo> _redisVideo = new Dictionary<long, FileInfo>();

        public VideoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<StatusVideoDto?> GetStatus(long id)
        {
            Models.Video? video = await _context.Videos.FindAsync(id);
            if (video == null) return null;
            return new StatusVideoDto
            {
                Id = video.Id,
                Extension = string.IsNullOrEmpty(video.Path) ? string.Empty : Path.GetExtension(video.Path).ToLower(),
                Available = File.Exists(video.Path),
            };
        }

        public async Task<FileInfo?> GetVideo(long id)
        {
            FileInfo? file = null;
            if (_redisVideo.ContainsKey(id))
            {
                file = _redisVideo[id];
                if (file != null && !file.Exists)
                {
                    file = null;
                    _redisVideo.Remove(id);
                }
            }
            else
            {
                Models.Video? video = await _context.Videos.FindAsync(id);
                if (video != null && File.Exists(video.Path))
                {
                    file = new FileInfo(video.Path);
                    _redisVideo.Add(video.Id, file);
                }
            }
            return file;
        }
    }
}
