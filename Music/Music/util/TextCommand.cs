using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.util
{
    class TextCommand
    {
        public static string CREATE_COMMENT = "create_comment";
      //  public static string CREATE_GOOGLE_SONG = "create_google_song";
        public static string CREATE_MESSAGE = "create_message";
        public static string CREATE_MESSAGE_CONCLUSION_TIME = "create_message_conclusion_time";
        public static string CREATE_PLAY_LIST = "create_play_list";
        public static string CREATE_PLAY_LIST_SONG = "create_play_list_song";
        public static string CREATE_SONG = "create_song";
        public static string CREATE_USER = "create_user";
        public static string CREATE_USER_DIALOG = "create_user_dialog";
        public static string CREATE_USER_DIALOGS = "create_user_dialogs";
        public static string CREATE_USER_MESSAGE = "create_user_message";


      //  public static string READ_GOOGLE_SONG_BY_ID = "read_google_song_by_id";
        public static string READ_MESSAGE_BY_ID = "read_message_by_id";
        public static string READ_PLAY_LIST_BY_NAME = "read_play_list_by_name";
        public static string READ_PLAY_LIST_SONG_BY_SONG_AND_PLAY_LIST_IDS = "read_play_list_song_by_song_and_play_list_ids";
        public static string READ_USER_BY_NAME = "read_user_by_name";
        public static string READ_USER_BY_ID = "read_user_by_id";
      


        public static string UPDATE_MESSAGE_CONCLUSION_TIME = "update_message_conclusion_time";
        public static string UPDATE_PLAY_LIST = "update_play_list";
        public static string UPDATE_SONG = "update_song";

        public static string DELETE_COMMENT = "delete_comment";
        public static string DELETE_MESSAGE = "delete_message";
        public static string DELETE_MESSAGE_CONCLUSION_TIME = "delete_message_conclusion_time";
        public static string DELETE_PLAY_LIST = "delete_play_list";
        public static string DELETE_PLAY_LIST_SONG = "delete_play_list_song";
        public static string DELETE_SONG = "delete_song";
        public static string DELETE_USER = "delete_user";
        public static string DELETE_USER_DIALOG = "delete_user_dialog";
        public static string DELETE_USER_MESSAGE = "delete_user_message";

        
        public static string ERROR = "error";
        public static string FIND_MESSAGE_CONCLUSION_BY_USERS_IDS = "find_message_conclusion_by_users_ids";
        public static string GET_ALL_COMMENTS_BY_SONG_ID = "get_all_comments_by_song_id";
        public static string GET_ALL_PLAY_LISTS_BY_USER_ID = "get_all_play_lists_by_user_id";
        public static string GET_ALL_REGISTERED_USERS = "get_all_registered_users";
        public static string GET_ALL_SONG = "get_all_song";
        public static string GET_ALL_SONGS_BY_PLAY_LIST_ID = "get_all_songs_by_play_list_id";
        public static string IS_USER_EXIST = "is_user_exist";
        public static string IS_USER_REGISTERED = "is_user_registered";
        public static string SEND_NOTIFICATION_FOR_USERS = "send_notification_for_users";
        
    }
}
