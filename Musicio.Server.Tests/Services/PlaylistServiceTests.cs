using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Musicio.Core.Domain;
using Musicio.Server.Data;
using Musicio.Server.Services.Playlist;
using Xunit;

namespace Musicio.Server.Tests.Services
{
    public class PlaylistServiceTests
    {
        private readonly Mock<Repository<Playlist>> _playlistRepositoryMock;

        public PlaylistServiceTests()
        {
            _playlistRepositoryMock = new Mock<Repository<Playlist>>();
        }

        [Fact]
        public void GetPlaylistTest()
        {
            //Arrange
            var playlist1 = new Playlist { UserId = 1, Title = "Playlist 1" };
            var playlist2 = new Playlist { UserId = 2, Title = "Playlist 2" };
            var playlist3 = new Playlist { UserId = 3, Title = "Playlist 3" };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null);

            //Act
            var actualPlaylist = playlistService.GetPlaylistSongs(3);

            //Assert
            Assert.Equal(playlist3.UserId, actualPlaylist.UserId);
            Assert.Equal(playlist3.Title, actualPlaylist.Title);
        }

        [Fact]
        public void GetOnlyPlaylistNameAndIdTest()
        {
            //Arrange
            var playlist1 = new Playlist { UserId = 1, Title = "Playlist 1", Id = 1, Image = "TestImage"};
            var playlist2 = new Playlist { UserId = 1, Title = "Playlist 2", Id = 2, Image = "TestImage" };
            var playlist3 = new Playlist { UserId = 2, Title = "Playlist 3", Id = 3, Image = "TestImage" };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null);

            //Act
            var actualPlaylists = playlistService.GetPlaylistNameAndId(1);

            //Assert
            Assert.Equal(playlist1.Id, actualPlaylists[0].Id);
            Assert.Equal(playlist1.Title, actualPlaylists[0].Title);
            Assert.Null(actualPlaylists[0].Image);
            Assert.Equal(0, actualPlaylists[0].UserId);
        }
    }
}
