using Music.dto;
using Music.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.command.create;
using Music.command.delete;

using Music.command.read;
using Music.command.other;
using Music.command.update;

namespace Music.controller
{
    public sealed class CommandProvider
    {
        private static CommandProvider instance = new CommandProvider();
        private Dictionary<CommandName, ICommand> pairs = new Dictionary<CommandName, ICommand>();
        CommandProvider()
        {
            pairs.Add(CommandName.CREATE_COMMENT, new CreateComment());
           
            pairs.Add(CommandName.CREATE_MESSAGE, new CreateMessage());
            pairs.Add(CommandName.CREATE_MESSAGE_CONCLUSION_TIME, new CreateMessageConclusionTime());
            pairs.Add(CommandName.CREATE_PLAY_LIST, new CreatePlayList());
            pairs.Add(CommandName.CREATE_PLAY_LIST_SONG, new CreatePlayListSong());
            pairs.Add(CommandName.CREATE_SONG, new CreateSong());
            pairs.Add(CommandName.CREATE_USER, new CreateUser());
            pairs.Add(CommandName.CREATE_USER_DIALOG, new CreateUserDialog());
            pairs.Add(CommandName.CREATE_USER_DIALOGS, new CreateUserDialogs());
            pairs.Add(CommandName.CREATE_USER_MESSAGE, new CreateUserMessage());


        
            pairs.Add(CommandName.READ_MESSAGE_BY_ID, new ReadMessageById());
            pairs.Add(CommandName.READ_PLAY_LIST_BY_NAME, new ReadPlayListByName());
            pairs.Add(CommandName.READ_PLAY_LIST_SONG_BY_SONG_AND_PLAY_LIST_IDS, new ReadPlayListSongBySongAndPlayListIds());
            pairs.Add(CommandName.READ_USER_BY_NAME, new ReadUserByName());
            pairs.Add(CommandName.READ_USER_BY_ID, new ReadUserById());
           


            pairs.Add(CommandName.UPDATE_MESSAGE_CONCLUSION_TIME, new UpdateMessageConclusionTime());
            pairs.Add(CommandName.UPDATE_PLAY_LIST, new UpdatePlayList());
            pairs.Add(CommandName.UPDATE_SONG, new UpdateSong());



            pairs.Add(CommandName.DELETE_COMMENT, new DeleteComment());
            pairs.Add(CommandName.DELETE_MESSAGE, new DeleteMessage());
            pairs.Add(CommandName.DELETE_MESSAGE_CONCLUSION_TIME, new DeleteMessageConclusionTime());
            pairs.Add(CommandName.DELETE_PLAY_LIST, new DeletePlayList());
            pairs.Add(CommandName.DELETE_PLAY_LIST_SONG, new DeletePlayListSong());
            pairs.Add(CommandName.DELETE_SONG, new DeleteSong());
            pairs.Add(CommandName.DELETE_USER, new DeleteUser());
            pairs.Add(CommandName.DELETE_USER_DIALOG, new DeleteUserDialog());
            pairs.Add(CommandName.DELETE_USER_MESSAGE, new DeleteUserMessage());



            pairs.Add(CommandName.ERROR, new Error());
            pairs.Add(CommandName.FIND_MESSAGE_CONCLUSION_BY_USERS_IDS, new FindMessageConclusionByUsersIds());
            pairs.Add(CommandName.GET_ALL_COMMENTS_BY_SONG_ID, new GetAllCommentsBySongId());
            pairs.Add(CommandName.GET_ALL_PLAY_LISTS_BY_USER_ID, new GetAllPlayListsByUserId());
            pairs.Add(CommandName.GET_ALL_REGISTERED_USERS, new GetAllRegisteredUsers());
            pairs.Add(CommandName.GET_ALL_SONG, new GetAllSong());
            pairs.Add(CommandName.GET_ALL_SONGS_BY_PLAY_LIST_ID, new GetAllSongsByPlayListId());
            pairs.Add(CommandName.IS_USER_EXIST, new IsUserExist());
            pairs.Add(CommandName.IS_USER_REGISTERED, new IsUserRegistered());
            pairs.Add(CommandName.SEND_NOTIFICATION_FOR_USERS, new SendNotificationForUsers());







        }
        public static CommandProvider GetInstance()
        {
            return instance;
        }
        public ICommand GetCommand(string request)
        {
            CommandName commandName;
            try
            {
                commandName = (CommandName)Enum.Parse(typeof(CommandName), request.ToUpper());
                return pairs[commandName];
            }
            catch (Exception e)
            {
                return pairs[CommandName.ERROR];
            }
        }
    }
}
