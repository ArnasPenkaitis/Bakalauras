package com.example.bakalauras;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.bakalauras.R;
import com.example.bakalauras.Shared.APIRequest;
import com.example.bakalauras.Shared.AppConf;
import com.example.bakalauras.Shared.CacheInterceptor;
import com.example.bakalauras.Shared.MainThreadOkHttpCallback;
import com.example.bakalauras.Shared.Utils;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import okhttp3.Cache;
import okhttp3.HttpUrl;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;


/**
 * A simple {@link Fragment} subclass.
 */
public class LoginFragment extends Fragment {

    private final String TAG = "LoginFragment";

    // application classes
    private Utils mUtils;
    private APIRequest apiRequest;

    // framework components
    private NavController mNavController;
    private SharedPreferences mSharedPrefs;
    private OkHttpClient okHttpClient;
    private MediaType mMediaType;

    // layout vars
    private View mThisFrag;
    private TextView mRegisterAction;
    private EditText mETEmail;
    private EditText mETPassword;
    private Button mBtnLogin;
    private TextView mTVErrorText;

    private Context context;
    public LoginFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_login, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated( view, savedInstanceState );

        prepareLayoutComponents( view );
        prepareListeners( view );
    }
    private void prepareListeners(View view){
        mRegisterAction.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mNavController.navigate( R.id.action_loginFragment_to_registerFragment );
            }
        } );

        mBtnLogin.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                login();
            }
        } );
    }

    private void prepareLayoutComponents(View view){
        mThisFrag = view;
        mNavController = Navigation.findNavController(view);
        mRegisterAction = view.findViewById( R.id.textView4 );
        mETEmail = view.findViewById( R.id.ET_login_email );
        mETPassword = view.findViewById( R.id.ET_login_password );
        mBtnLogin = view.findViewById( R.id.BTN_login );
        mMediaType = MediaType.parse(AppConf.JsonMediaTypeString);
        mUtils = new Utils();
        context = getContext();
        mSharedPrefs = mUtils.getAppSharedPreferences( getContext() );
        apiRequest = new APIRequest( getContext() );
        mTVErrorText = view.findViewById( R.id.TV_login_error_box );
    }

    private Boolean checkIfTokenExists(){
        return !mUtils.getAppToken( getContext() ).equals( "" );
    }
    private void login(){
        loginWithCreds();
    }

    private JSONObject formJsonObjectForLogin(){
        String token = mUtils.getAppToken( getContext() );
        JSONObject jsonObject = new JSONObject(  );
        try {
            if (mETEmail.getText().toString().equals( "" ) ) {
                jsonObject.put( "token", token );
            } else {
                jsonObject.put( "Username", mETEmail.getText() );
                jsonObject.put( "Password", mETPassword.getText() );
            }
        }
        catch(JSONException e){
            Log.e(TAG, "LoginFragment -> formJsonObjectForLogin()" + e.toString());
        }

        return jsonObject;
    }

    private void loginWithCreds() {
        okHttpClient = apiRequest.generateOkHttpClient();

        AppConf apiConf = AppConf.getInstance();
        String loginRoute = apiConf.getACCOUNT_LOGIN_API_ROUTE();

        Request request = apiRequest.getRequestObject( loginRoute, false, true, formJsonObjectForLogin().toString(), mMediaType );

        okHttpClient.newCall(request).enqueue( new MainThreadOkHttpCallback() {

            @Override
            public void apiCallSuccess(String body){
                try{
                    JSONObject responseRoot = new JSONObject( body );
                    String token = responseRoot.getString( "tokenas" );
                    String id = responseRoot.getString( "id" );
//                    mTVErrorText.setText( token );

                    if(!token.equals( "" ) && !id.equals( "0" )) {
                        mUtils.writeAppTokenToSharedPreferences( getContext(), token );
                        mUtils.writeUserIdToSharedPreferences( getContext(), id );
                        mNavController.navigate(R.id.action_loginFragment_to_teacher_list);
                        //mNavController.popBackStack( R.id.teacher_list, false );
                    }
                    else{
                        JSONObject message = responseRoot.getJSONObject( "Message" );
                        String messageText = message.getString( "Item2" );
                        mTVErrorText.setText( messageText );
                        mTVErrorText.setVisibility( View.VISIBLE );
                    }
                }
                catch (JSONException e){
                    Log.e("OkHttp", "Error while parsing api/login response data - " + e.toString());
                }
            }

            @Override
            public void apiCallFail(Exception e){
                Toast toast = Toast.makeText(context,"Login failed: bad credentials!", Toast.LENGTH_SHORT);
                toast.show();
                Log.e("OkHttp", "Api call http://<host>/api/auth/student failed: " + e.toString());
            }

        } );
    }

}
