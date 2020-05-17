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

import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProviders;
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
public class RegisterFragment extends Fragment {

    private final String TAG = "RegisterFragment";

    private Utils mUtils;
    private APIRequest apiRequest;

    // framework components
    private NavController mNavController;
    private SharedPreferences mSharedPrefs;
    private OkHttpClient okHttpClient;
    private MediaType mMediaType;

    // layout vars
    private Context context;
    private TextView mLoginAction;
    private EditText mETDisplayName;
    private EditText mETEmail;
    private EditText mETPassword;
    private EditText mETName;
    private EditText mETSurname;
    private Button mBtnRegister;
    public RegisterFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_register, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated( view, savedInstanceState );

        prepareLayoutComponents( view );
        prepareListeners( view );
    }

    private void prepareLayoutComponents(View view){
        mNavController = Navigation.findNavController(view);
        mETDisplayName = view.findViewById( R.id.ET_register_display_name );
        mETEmail = view.findViewById( R.id.ET_register_email );
        mETPassword = view.findViewById( R.id.ET_register_password );
        mETName = view.findViewById( R.id.registerNameInput );
        mETSurname = view.findViewById( R.id.registerSurnameInput );

        mBtnRegister = view.findViewById( R.id.BTN_register );
        mMediaType = MediaType.parse(AppConf.JsonMediaTypeString);
        mUtils = new Utils();
        context = getContext();
        mSharedPrefs = mUtils.getAppSharedPreferences( getContext() );
        apiRequest = new APIRequest( getContext() );
    }

    private void prepareListeners(View view){
        mBtnRegister.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                register();
            }
        } );

    }

    private JSONObject formJsonObjectForLogin(){
        JSONObject jsonObject = new JSONObject(  );
        try {
            jsonObject.put("Surname",mETSurname.getText());
            jsonObject.put("Name",mETName.getText());
            jsonObject.put( "Email", mETEmail.getText() );
            jsonObject.put( "Password", mETPassword.getText() );
            jsonObject.put( "Username", mETDisplayName.getText() );
        }
        catch(JSONException e){
            Log.e(TAG, "LoginFragment -> formJsonObjectForLogin()" + e.toString());
        }

        return jsonObject;
    }

    private void register() {
        okHttpClient = apiRequest.generateOkHttpClient();

        AppConf apiConf = AppConf.getInstance();
        String registerApiRoute = apiConf.getACCOUNT_REGISTER_API_ROUTE();

        Request request = apiRequest.getRequestObject( registerApiRoute, false, true, formJsonObjectForLogin().toString(), mMediaType );

        okHttpClient.newCall(request).enqueue( new MainThreadOkHttpCallback() {

            @Override
            public void apiCallSuccess(String body){
                try{
                    JSONObject responseRoot = new JSONObject( body );
                    String id = responseRoot.getString( "id" );
                    mUtils.writeUserIdToSharedPreferences( getContext(), id );
                    mNavController.navigate(R.id.action_registerFragment_to_teacher_list);
                }
                catch (JSONException e){
                    Log.e("OkHttp", "Error while parsing api/users/register response data - " + e.toString());
                }
            }

            @Override
            public void apiCallFail(Exception e){
                Toast toast = Toast.makeText(context,"Username already exists", Toast.LENGTH_SHORT);
                toast.show();
                Log.e("OkHttp", "Api call http://<host>/api/users/register failed: " + e.toString());
            }

        } );
    }
}
