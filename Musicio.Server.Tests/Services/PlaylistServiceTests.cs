using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Server.Data;
using Musicio.Server.Services.Playlist;
using Xunit;

namespace Musicio.Server.Tests.Services
{
    public class PlaylistServiceTests
    {
        private readonly Mock<Repository<Playlist>> _playlistRepositoryMock;
        private readonly Mock<Repository<PlaylistSong>> _playlistSongRepositoryMock;

        public PlaylistServiceTests()
        {
            _playlistRepositoryMock = new Mock<Repository<Playlist>>();
            _playlistSongRepositoryMock = new Mock<Repository<PlaylistSong>>();
        }

        [Fact]
        public void GetPlaylistByIdTest()
        {
            //Arrange
            var playlist1 = new Playlist { Id = 1, UserId = 1, Title = "Playlist 1" };
            var playlist2 = new Playlist { Id = 2, UserId = 2, Title = "Playlist 2" };
            var playlist3 = new Playlist { Id = 3, UserId = 3, Title = "Playlist 3" };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            //Act
            var actualPlaylist = playlistService.GetPlaylistById(3);

            //Assert
            Assert.Equal(playlist3.Id, actualPlaylist.Id);
            Assert.Equal(playlist3.UserId, actualPlaylist.UserId);
            Assert.Equal(playlist3.Title, actualPlaylist.Title);
        }

        [Fact]
        public void GetPlaylistWithSongsTest()
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

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

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

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            //Act
            var actualPlaylists = playlistService.GetPlaylistNameAndId(1);

            //Assert
            Assert.Equal(playlist1.Id, actualPlaylists[0].Id);
            Assert.Equal(playlist1.Title, actualPlaylists[0].Title);
            Assert.Null(actualPlaylists[0].Image);
            Assert.Equal(0, actualPlaylists[0].UserId);
        }

        [Fact]
        public void DoesPlaylistExistCheckTrue()
        {
            var playlist1 = new Playlist { UserId = 1, Title = "Playlist 1", Id = 1, Image = "TestImage" };
            var playlist2 = new Playlist { UserId = 1, Title = "Playlist 2", Id = 2, Image = "TestImage" };
            var playlist3 = new Playlist { UserId = 2, Title = "Playlist 3", Id = 3, Image = "TestImage" };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            //Act
            var actualExistence = playlistService.PlaylistExists(1);

            //Assert
            Assert.True(actualExistence);
        }

        [Fact]
        public void DoesPlaylistExistCheckFalse()
        {
            var playlist1 = new Playlist { UserId = 1, Title = "Playlist 1", Id = 1, Image = "TestImage" };
            var playlist2 = new Playlist { UserId = 1, Title = "Playlist 2", Id = 2, Image = "TestImage" };
            var playlist3 = new Playlist { UserId = 2, Title = "Playlist 3", Id = 3, Image = "TestImage" };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            //Act
            var actualExistence = playlistService.PlaylistExists(4);

            //Assert
            Assert.False(actualExistence);
        }

        [Fact]
        public void SongCountInPlaylistTest()
        {
            var playlistSong1 = new PlaylistSong { Id = 1, PlaylistId = 1, SongId = 1 };
            var playlistSong2 = new PlaylistSong { Id = 2, PlaylistId = 1, SongId = 2 };
            var playlistSong3 = new PlaylistSong { Id = 3, PlaylistId = 2, SongId = 1 };

            _playlistSongRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlistSong1);
                options.Insert(playlistSong2);
                options.Insert(playlistSong3);
            });

            var playlistService = new PlaylistService(null, null, _playlistSongRepositoryMock.Object);

            //Act
            var actualAmountPlaylist1 = playlistService.GetSongCountInPlaylist(1);
            var actualAmountPlaylist2 = playlistService.GetSongCountInPlaylist(2);

            //Assert
            Assert.Equal(2, actualAmountPlaylist1);
            Assert.Equal(1, actualAmountPlaylist2);
        }

        [Fact]
        public void AddSongToPlaylistTest()
        {
            var playlistSong1 = new PlaylistSong { Id = 1, PlaylistId = 1, SongId = 1 };
            var playlistSong2 = new PlaylistSong { Id = 2, PlaylistId = 1, SongId = 2 };
            var playlistSong3 = new PlaylistSong { Id = 3, PlaylistId = 2, SongId = 1 };

            _playlistSongRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlistSong1);
                options.Insert(playlistSong2);
                options.Insert(playlistSong3);
            });

            var playlistService = new PlaylistService(null, null, _playlistSongRepositoryMock.Object);

            //Act
            playlistService.AddSongToPlaylist(1, 4);
            var actualAmount = playlistService.GetSongCountInPlaylist(1);

            //Assert
            Assert.Equal(3, actualAmount);
        }

        [Fact]
        public void CreatePlaylistTest()
        {
            var playlist1 = new Playlist { Id = 1, UserId = 1 };
            var playlist2 = new Playlist { Id = 2, UserId = 1 };
            var playlist3 = new Playlist { Id = 3, UserId = 2 };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            //Act
            var countBefore = playlistService.GetPlaylistCount(2);
            playlistService.CreatePlaylist(new PlaylistCreationMessage("name", "desc", null, null), 2);
            var playlist = playlistService.GetPlaylistById(4);
            var countAfter = playlistService.GetPlaylistCount(2);

            //Assert
            Assert.Equal(1, countBefore);
            Assert.Equal(2, countAfter);
            Assert.NotNull(playlist);
        }

        [Fact]
        public void PlaylistCountTest()
        {
            var playlist1 = new Playlist { Id = 1, UserId = 1 };
            var playlist2 = new Playlist { Id = 2, UserId = 1 };
            var playlist3 = new Playlist { Id = 3, UserId = 2 };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            var actualCountUser1 = playlistService.GetPlaylistCount(1);
            var actualCountUser2 = playlistService.GetPlaylistCount(2);

            Assert.Equal(2, actualCountUser1);
            Assert.Equal(1, actualCountUser2);
        }

        [Fact]
        public void GetUserPlaylistsTest()
        {
            var playlist1 = new Playlist { Id = 1, UserId = 1 };
            var playlist2 = new Playlist { Id = 2, UserId = 1 };
            var playlist3 = new Playlist { Id = 3, UserId = 2 };

            var expectedPlaylists = new List<Playlist>{ playlist1, playlist2 };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            var actualPlaylists = playlistService.GetUserPlaylists(1);

            for (int i = 0; i < actualPlaylists.Count; i++)
            {
                Assert.Null(actualPlaylists[i].PlaylistSongs);
                Assert.Equal(expectedPlaylists[i].Id, actualPlaylists[i].Id);
                Assert.Equal(expectedPlaylists[i].UserId, actualPlaylists[i].UserId);
            }
        }

        [Fact]
        public void GetNonExistentUserPlaylistsTest()
        {
            var playlist1 = new Playlist { Id = 1, UserId = 1 };
            var playlist2 = new Playlist { Id = 2, UserId = 1 };
            var playlist3 = new Playlist { Id = 3, UserId = 2 };

            _playlistRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(playlist1);
                options.Insert(playlist2);
                options.Insert(playlist3);
            });

            var playlistService = new PlaylistService(_playlistRepositoryMock.Object, null, null);

            var actualPlaylists = playlistService.GetUserPlaylists(3);

            Assert.Empty(actualPlaylists);
        }
    }
}
