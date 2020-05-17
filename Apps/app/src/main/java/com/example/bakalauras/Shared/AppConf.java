package com.example.bakalauras.Shared;

public class AppConf {


    // API URL base for EMULATOR
    //private static final String API_SCHEME = "http://";
    //private static final String API_BASE = "10.0.2.2:8089/api/";
    //public String getLessonGetListApiRoute(String subjectId) { return "http://" + "10.0.2.2:8089/api/teacher/" + 0 + "/subject/" + subjectId +"/class"; }
    //public String getSubjectsGetListApiRoute(String teacherId) { return "http://" + "10.0.2.2:8089/api/teacher/" + teacherId + "/subject"; }
    //public String getVisualizationsGetListApiRoute(String lessonId) { return "http://" + "10.0.2.2:8089/api/visualization/class/" + lessonId; }
    //public String getTeacherGetListApiRoute() { return TEACHER_GET_LIST_API_ROUTE; }


    // NGROK
    private static final String API_SCHEME = "http://";
    private static final String API_BASE = "210491ae.ngrok.io/api/";
    public String getLessonGetListApiRoute(String subjectId) { return "http://" + "210491ae.ngrok.io/api/teacher/" + 0 + "/subject/" + subjectId +"/class"; }
    public String getSubjectsGetListApiRoute(String teacherId) { return "http://" + "210491ae.ngrok.io/api/teacher/" + teacherId + "/subject"; }
    public String getVisualizationsGetListApiRoute(String lessonId) { return "http://" + "210491ae.ngrok.io/api/visualization/class/" + lessonId; }
    public String getTeacherGetListApiRoute() { return TEACHER_GET_LIST_API_ROUTE; }


    //Maniskiai
    private final String HOST_URL = API_SCHEME + API_BASE;
    private final String ACCOUNT_LOGIN_API_ROUTE = HOST_URL + "auth/token/student";
    private final String ACCOUNT_TOKEN_LOGIN_API_ROUTE = HOST_URL + "student/token-login";
    private final String ACCOUNT_REGISTER_API_ROUTE = HOST_URL + "student";
    private final String TEACHER_GET_LIST_API_ROUTE = HOST_URL + "teacher";
    private final String LIST_GET_LIST_API_ROUTE = HOST_URL + "class/";
    private final String SUBJECTS_GET_LIST_API_ROUTE = HOST_URL + "subjects";


    // Header
    public static final String JsonMediaTypeString = "application/json";

    // App's filenames constants
    public static final String APP_SHARED_PREFERENCES_NAME = "com.example.eventlookup.access";
    public static final String TOKEN_KEY = "app_token";
    public static final String USER_ID = "user_id";

    // Singleton class
    private static AppConf appConfInstance = null;

    // Private constructor
    private AppConf(){
    }

    public static AppConf getInstance(){

        if(appConfInstance == null){
            appConfInstance = new AppConf();
        }

        return appConfInstance;
    }

    public String getACCOUNT_LOGIN_API_ROUTE() { return ACCOUNT_LOGIN_API_ROUTE; }
    public String getACCOUNT_REGISTER_API_ROUTE() { return ACCOUNT_REGISTER_API_ROUTE; }

}

